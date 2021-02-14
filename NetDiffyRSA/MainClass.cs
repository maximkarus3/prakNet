using System;

namespace NetDiffyRSA
{
    class MainClass
    {
        static void Main(string[] args)
        {
            switch (Console.ReadLine())
            {
                case "/server":
                    {
                        Console.Write("Введите ключ A:");
                        string keyA = Console.ReadLine(); 
                        ServerObject.ListenServer(keyA);
                        Server.SocketServer();
                        break;
                    }
                case "/client":
                    {
                        Console.Write("Введите ключ B:");
                        string keyB = Console.ReadLine();
                        Diffy_Hellemen.ListenClient(keyB);  
                        Client.SocketClient();
                        break;
                    }
            }
        }
    }
}
