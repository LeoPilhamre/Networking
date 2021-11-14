using System;

namespace Client
{
    interface NetworkData
    {
        Int32 Port { get; set; }
        string ipAddress { get; set; }
    }

    public class Settings : NetworkData
    {
        public Settings()
        {
            Port = 13000;
            ipAddress = "127.0.0.1";
        }

        public Int32 Port { get; set; }
        public string ipAddress { get; set; }
    }
}
