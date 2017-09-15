using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using CallerID_Cloud_Relay;

namespace cid_cm.Classes
{
    public class UdpReceiverClass
    {
        // Declare variables
        public static Boolean Done;
        public static string ReceivedMessage;
        public static byte[] ReceviedMessageByte;
        public static int[] NListenPorts = new int[] { 3520, 3521, 3522, 3523, 3524, 3525, 3526, 3527, 3528, 3529, 3530 };
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
            }

            if (!bound)
            {
                var erString = "Binding to ports 3520 - 3530 failed. Closing...";
                MessageBox.Show(erString);
                Program.FUrlSend.Close();
                return;
            }
            else
            {
                Program.FUrlSend.SetMyTitle("Caller ID Cloud Relay - " + Application.ProductVersion.ToString() + " - Listening on Port: " + UdpReceiverClass.BoundTo);
            }

            // Wait for broadcast
            try
            {
                while (!Done)
                {
                    // Receive broadcast
                    ReceviedMessageByte = listener.Receive(ref groupEp);
                    ReceivedMessage = Encoding.UTF7.GetString(ReceviedMessageByte, 0, ReceviedMessageByte.Length);

                    // Filter smaller messages););
                    if (ReceviedMessageByte.Length > filterMessageSmallerThan)
                    {
                        DataReceived(this, EventArgs.Empty);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            listener.Close();
        }

    }
}

