using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Client;

namespace Server
{

    class Program
    {

        public static void Main(string[] args)
        {
            Awake();

            new Listener();
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


        /// <summary>
        /// Listens for clients & write to client
        /// </summary>
        private class Listener
        {
            private TcpListener server { get; set; }

            public Listener()
            {
                Listen();
            }

            /// <summary>
            /// Listens for clients' data [blocking]
            /// </summary>
            private void Listen()
            {
                try
                {
                    Int32 port = Port;
                    IPAddress localAddr = IPAddress.Parse(ipAddress);

                    server = new TcpListener(localAddr, port);

                    server.Start();                                         // starts listening to clients' requests

                    // Buffer for reading data
                    Byte[] bytes = new Byte[256];                           // 2^8 bytes => 2^11 bit => 2^8 chars
                    String data;

                    while (true)
                    {
                        Console.WriteLine("Listening for clients' requests...");

                        TcpClient client = server.AcceptTcpClient();                    // await singular client connection

                        Console.WriteLine("Client connected. Proceeding.");

                        NetworkStream stream = client.GetStream();                      // data stream of client

                        data = null;                                                    // default

                        // All data from client looped
                        int i;
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);        // decode [max 2^8 characters]

                            Console.WriteLine("Received: {0}", data);

                            //TODO: Process data (encode: System.Text.Encoding.ASCII.GetBytes(data);)
                        }

                        client.Close();
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
                finally
                {
                    server.Stop();
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.Read();
            }

        }


        #region Constants

        public static Int32 Port { get; private set; }
        public static string ipAddress { get; private set; }

        #endregion

    }

}
