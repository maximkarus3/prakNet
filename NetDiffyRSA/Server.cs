using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetDiffyRSA
{
    public class Server
    {
        static int port = 8005; // порт для приема входящих запросов
        public static void SocketServer()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("192.168.0.105"), port);

            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string encryptWord, decryptWord = "";
            try
            {
                listenSocket.Bind(ipPoint);

                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                string msg = "";
                while (true)
                {
                    Socket handler = listenSocket.Accept();

                    if (handler.Connected)
                    {
                        Console.WriteLine("Обнаруженно подключение!");
                        Console.Write("Введите x сервера:");
                        string x = Console.ReadLine();
                        Diffy_Hellemen.ConnectClient(x);
                        while (handler.Connected)
                        {
                            byte[] data = new byte[256];

                            do
                            {
                                handler.Receive(data);
                                msg = Encoding.Unicode.GetString(data);
                                string[] dataSplit = msg.Split(' ');
                                encryptWord = RSACrypt.Encrypt(int.Parse(dataSplit[0]), int.Parse(dataSplit[1]), dataSplit[2]);
                                decryptWord = RSACrypt.Decrypt(encryptWord, "");
                                
                            }
                            while (handler.Available > 0);

                            Console.WriteLine("Зашифрованное сообщение: " + ": " + encryptWord);
                            Console.WriteLine("Расшифрованное сообщение: " + ": " + decryptWord);

                            data = Encoding.Unicode.GetBytes(decryptWord);
                            handler.Send(data);
                        }

                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
    