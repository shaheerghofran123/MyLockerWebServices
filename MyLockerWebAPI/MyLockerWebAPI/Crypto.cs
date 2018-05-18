using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MyLockerWebAPI
{
    public class Crypto
    {
        public double p,q,n,t,e,d;
        List<double> prime = new List<double>();

        List<double> dVariable = new List<double>();
        public Crypto()
        {
            CalculateVariables();
        }

        public static string ConvertStringtoMD5(string strword)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(strword);

            byte[] hash = md5.ComputeHash(inputBytes);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public double[] CalculateVariables()
        {
            Random random = new Random();
            p = CalculatePrime(random.Next(1, 100));
            q = CalculatePrime(random.Next(1, 100));
            n = p * q;
            t = (p - 1) * (q - 1);
            e = CalculateE();
            d = CalculateD();
            return new double[] { n, e, d };
        }
        public double CalculateE()
        {
            List<double> eVariable = new List<double>();
            for (double i = 3; i < 1000; i++)
            {
                if (t % i != 0)
                {
                    eVariable.Add(i);
                }
            }
            return eVariable[29];
        }

        public double CalculateD()
        {
            for (double i = 1; i < 1000; i++)
            {
                double temp = i * e;
                if (temp % t == 1)
                {
                    dVariable.Add(temp);
                    //break;
                }
            }
            return dVariable[23];
        }
        public double CalculatePrime(int index)
        {
            List<double> nos = new List<double>();
            bool isprime = false;
            for (int i = 1000; i >= 1; i--)
            {
                for (int j = 1; j <= 1000; j++)
                {
                    if (i != j && j != 1)
                    {
                        if (i % j != 0)
                        {
                            isprime = true;
                        }
                        else
                        {
                            isprime = false;
                            break;
                        }
                    }

                }
                if (isprime == true)
                {
                    nos.Add(i);
                }
            }
            return nos[index];
        }
    }
}
