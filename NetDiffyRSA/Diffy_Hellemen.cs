using System;
using System.Collections.Generic;
using System.Text;

namespace NetDiffyRSA
{
    public class Diffy_Hellemen
    {
        public static int b;
        public static int x;
        public static int y;
        public static int kb;
        public static int G = 35;
        public static int P = 17;
        public static int Power(int a, int b, int p)
        {
            if (b == 1)
            {
                return a;
            }
            else
            {
                return ((int)Math.Pow(a, b) % p);
            }
        }
        public static void ListenClient(string keyB)
        {
            Console.WriteLine($"Приватный ключ b: {keyB}");
            if (int.TryParse(keyB, out int bnew))
            {
                b = bnew;
                y = Power(G, b, P);
            }

            Console.WriteLine($"G = {G}, P = {P} :");
            Console.WriteLine($"Клиент Y = {y} :");

        }
        public static void ConnectClient(string serverX)
        {
            try
            {
                Console.WriteLine($"X:{serverX} ");
                if (int.TryParse(serverX, out int xnew))
                {
                    x = xnew;
                    kb = Power(x, b, P);

                }
                Console.WriteLine($"G = {G}, P = {P}");
                Console.WriteLine($"Клиент Y = {y} ");
                Console.WriteLine($"Клиент kb = {kb} ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public class ServerObject
    {
        public static Random rnd = new Random();
        public static int G = 36;
        public static int P = 46;
        public static int a, x, y, ka;
        public static int Power(int a, int b, int p)
        {
            if (b == 1)
            {
                return a;
            }
            else
            {
                return ((int)Math.Pow(a, b) % p);
            }
        }
        public static void ListenServer(string keyA)
        {
            try
            {
                Console.WriteLine($"Приватный ключ a: {keyA}");
                if (int.TryParse(keyA, out int anew))
                {
                    a = anew;
                    x = Power(G, a, P);
                }
                Console.WriteLine($"G = {G}, P = {P} :");
                Console.WriteLine($"Сервер X = {x} :");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

