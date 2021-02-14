using System;
using System.Collections.Generic;
using System.Text;

namespace NetDiffyRSA
{
    public class RSACrypt
    {
        public static int[] Encrypted = new int[150];
        public static int[] Decrypted = new int[150];
        public static int mLenght = 0;
        private static bool isPrime(int x)
        {
            if (x < 3)
                return false;
            for (int i = x - 1; i > 1; i--)
            {
                if (x % i == 0)
                    return false;
            }
            return true;
        }
        private static int findQ_n(int first, int sec)
        {
            return (first - 1) * (sec - 1);
        }
        private static int findN(int first, int sec)
        {
            return (first) * (sec);
        }
        private static bool Coprime(int first, int sec)
        {
            int mlen = 0;
            if (first < sec)
            {
                mlen = first;
            }
            else
            {
                mlen = sec;
            }
            for (int i = 2; i < mlen; i++)
            {
                if (first % i == 0 && sec % i == 0)
                    return false;
            }
            return true;
        }
        private static int findEncKey(int Q_n)
        {
            int mKey = 2;
            while (true)
            {
                Random rnd = new Random();
                mKey = (rnd.Next(Q_n - 2)) + 2;
                if (Coprime(mKey, Q_n))
                {
                    break;
                }
                   
            }
            return mKey;
        }
        private static int findD(int EncKey, int Q_n)
        {
            for (int d = 1; true; d++)
            {
                if ((d * EncKey) % Q_n == 1)
                    return d;
            }
        }
        private static int c_dmodn(int c, int d, int n)
        {
            int value = 1;
            while (d > 0)
            {
                value *= c;
                value %= n;
                d--;
            }
            return value;
        }
        private static int Encrypte(int m, int mKey, int myN)
        {
            return c_dmodn(m, mKey, myN);
        }
        private static int Decrypte(int c, int myD, int myN)
        {
            return c_dmodn(c, myD, myN);
        }
        public static string Encrypt(int prime1, int prime2, string message)
        {
            int first = prime1;
            int sec = prime2;
            int Q_n = findQ_n(first, sec);
            int myN = findN(first, sec);
            int mKey = findEncKey(Q_n);
            int myD = findD(mKey, Q_n);
            int x, sifre, cozum;
            string res = "";
            string mWord = message;

            for (int i = 0; i < mWord.Length; i++)
            {
                x = (int)mWord[i];
                sifre = Encrypte(x, mKey, myN);
                cozum = Decrypte(sifre, myD, myN);
                Encrypted[i] = sifre;
                Decrypted[i] = cozum;

                mLenght = i + 1;
            }
            for (int i = 0; i < mLenght; i++)
            {
                res += (((char)Encrypted[i]).ToString());
            }

            return res;
        }
        public static string Decrypt(string res, string decryptWord)
        {
            for (int i = 0; i < res.Length; i++)
            {
                decryptWord += (((char)Decrypted[i]).ToString());
            }
            return decryptWord;
        }
    }
}

