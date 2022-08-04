using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CallerID_Cloud_Relay;
using CallerID_Cloud_Relay.Classes;

namespace cid_cm.Classes
{
    public class UdpReceiverClass
    {
        // Declare variables
        public static Boolean Done;
        public static string ReceivedMessage;
        public static byte[] ReceviedMessageByte;
        public static int[] NListenPorts = new int[] { 3520 };//, 3521, 3522, 3523, 3524, 3525, 3526, 3527, 3528, 3529, 3530 };
        public static string BoundTo;

        // Define event
        public delegate void UdpEventHandler(UdpReceiverClass u, EventArgs e);
        public event UdpEventHandler DataReceived;

        // Idle listening
        public void UdpIdleReceive()
        {

            // Set done to false so loop will begin
            Done = false;

            // Setup filter for too small messages
            const int filterMessageSmallerThan = 4;

            // Setup socket for listening
            UdpClient listener = null;
            IPEndPoint groupEp = null;

            bool bound = false;
            while (!bound)
            {
                foreach (int port in NListenPorts)
                {
                    try
                    {
                        listener = new UdpClient(port);
                        groupEp = new IPEndPoint(IPAddress.Any, port);
                        bound = true;
                        BoundTo = port.ToString();
                        break;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                break;
            }

            if (!bound)
            {
                List<Port> ports = GetNetStatPorts();
                string programs = "";
                foreach(Port p in ports)
                {
                    if (p.port_number == "3520")
                    {
                        programs += p.process_name + ", ";
                    }
                }
                if (!string.IsNullOrEmpty(programs))
                    programs = programs.Substring(0, programs.Length - 2);
                else
                    programs = "Unknown";

                var erString = "Needed port ( UDP 3520 ) is in use. Please close the following programs:\n\n" + programs;
                MessageBox.Show(erString);
                Application.Exit();
                return;
            }
            else
            {
                if (Program.FUrlSend != null)
                {
                    Common.WriteToLog("Bound to: " + BoundTo);
                    Program.FUrlSend.SetMyTitle("Caller ID Cloud Relay - " + Application.ProductVersion.ToString() + " - Listening on Port: " + UdpReceiverClass.BoundTo);
                }
            }

            // Wait for broadcast
            while (!Done)
            {
                // Receive broadcast
                try
                {
                    ReceviedMessageByte = listener.Receive(ref groupEp);
                }
                catch(Exception e)
                {
                    Common.WriteToLog("Failed to receive data: skipping. Exception: " + e.ToString());
                    continue;
                }

                try
                {
                    ReceivedMessage = Encoding.UTF7.GetString(ReceviedMessageByte, 0, ReceviedMessageByte.Length);
                }
                catch
                {
                    Common.WriteToLog("Unable to decode byte array from reception on bound port. Ignoring...");
                    continue;
                }

                // Filter smaller messages);
                try
                {
                    if (ReceviedMessageByte.Length > filterMessageSmallerThan)
                    {
                        DataReceived(this, EventArgs.Empty);
                    }
                }
                catch(Exception ex)
                {
                    Common.WriteToLog("Unable to raise call event. Exception: " + ex.ToString());
                    continue;
                }

            }

            listener.Close();
            Common.WriteToLog("[DONE= " + Done + "] :: LOST BOUND (" + DateTime.Now.ToString() + ")");
        }

        // ===============================================
        // The Method That Parses The NetStat Output
        // And Returns A List Of Port Objects
        // ===============================================
        public static List<Port> GetNetStatPorts()
        {
            var Ports = new List<Port>();

            try
            {
                using (Process p = new Process())
                {

                    ProcessStartInfo ps = new ProcessStartInfo();
                    ps.Arguments = "-a -n -o";
                    ps.FileName = "netstat.exe";
                    ps.UseShellExecute = false;
                    ps.WindowStyle = ProcessWindowStyle.Hidden;
                    ps.RedirectStandardInput = true;
                    ps.RedirectStandardOutput = true;
                    ps.RedirectStandardError = true;

                    p.StartInfo = ps;
                    p.Start();

                    StreamReader stdOutput = p.StandardOutput;
                    StreamReader stdError = p.StandardError;

                    string content = stdOutput.ReadToEnd() + stdError.ReadToEnd();
                    string exitStatus = p.ExitCode.ToString();

                    if (exitStatus != "0")
                    {
                        // Command Errored. Handle Here If Need Be
                    }

                    //Get The Rows
                    string[] rows = Regex.Split(content, "\r\n");
                    foreach (string row in rows)
                    {
                        //Split it baby
                        string[] tokens = Regex.Split(row, "\\s+");
                        if (tokens.Length > 4 && (tokens[1].Equals("UDP") || tokens[1].Equals("TCP")))
                        {
                            string localAddress = Regex.Replace(tokens[2], @"\[(.*?)\]", "1.1.1.1");
                            Ports.Add(new Port
                            {
                                protocol = localAddress.Contains("1.1.1.1") ? String.Format("{0}v6", tokens[1]) : String.Format("{0}v4", tokens[1]),
                                port_number = localAddress.Split(':')[1],
                                process_name = tokens[1] == "UDP" ? LookupProcess(Convert.ToInt16(tokens[4])) : LookupProcess(Convert.ToInt16(tokens[5]))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ports;
        }

        public static string LookupProcess(int pid)
        {
            string procName;
            try { procName = Process.GetProcessById(pid).ProcessName; }
            catch (Exception) { procName = "-"; }
            return procName;
        }

        // ===============================================
        // The Port Class We're Going To Create A List Of
        // ===============================================
        public class Port
        {
            public string name
            {
                get
                {
                    return string.Format("{0} ({1} port {2})", this.process_name, this.protocol, this.port_number);
                }
                set { }
            }
            public string port_number { get; set; }
            public string process_name { get; set; }
            public string protocol { get; set; }
        }

    }
}



