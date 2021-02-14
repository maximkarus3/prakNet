using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace NetDiffyRSA
{
    public class Client
    {
        static string symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static Random rnd = new Random();
        static char GetRandomChar()
        {
            var index = rnd.Next(symbols.Length);
            return symbols[index];
        }
        public static void SocketClient()
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(Globals.address), Globals.port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                while (true)
                {
                    Console.Write("Введите ключи и сообщение:");
                    string message = Console.ReadLine();
                    if (message != "exit")
                    {
                        byte[] data = Encoding.Unicode.GetBytes(message);
                        socket.Send(data);
                        data = new byte[256];
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;
                        do
                        {
                            bytes = socket.Receive(data, data.Length, 0);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (socket.Available > 0);
                        Console.WriteLine("Расшифрованное сообщение: " + builder.ToString());
                        builder.Clear();
                    }
                    else
                    {
                        break;
                    }
                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                Environment.Exit(1);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

    }
}
