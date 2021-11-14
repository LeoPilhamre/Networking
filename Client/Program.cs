using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{

    class Program
    {
        
        public static void Main(string[] argv)
        {
            Awake();

            Connect();
        }

        /// <summary>
        /// Runs before main program executes [non-blocking]
        /// </summary>
        private static void Awake()
        {
            Settings settings = new Settings();

            Port = settings.Port;
            ipAddress = settings.ipAddress;
        }


        private static void Connect()
        {
            try
            {
                TcpClient client = new TcpClient(ipAddress , Port);

                Byte[] writeData = System.Text.Encoding.ASCII.GetBytes(connectMessage);         // encode data to write to server

                NetworkStream stream = client.GetStream();                                      // stream to read & write data

                stream.Write(writeData, 0, writeData.Length);                                   // write data to server

                Console.WriteLine("Sent: {0}", connectMessage);

                Byte[] readData = new Byte[256];                                                // 2^8 bytes => 2^11 bit => 2^8 chars

                string responseData = String.Empty;

                // Reads response & converts to string
                Int32 bytes = stream.Read(readData, 0, readData.Length);
                responseData = System.Text.Encoding.ASCII.GetString(readData, 0, bytes);

                Console.WriteLine("Received: {0}", responseData);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.Read();
        }


        #region Constants

        public static Int32 Port { get; private set; }
        public static string ipAddress { get; private set; }

        private static readonly string connectMessage = "Connection with server by client A established.";

        #endregion

    }

}
