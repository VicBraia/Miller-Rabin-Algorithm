using System;
using System.Collections.Generic;

namespace Miller_Rabin
{
    class Program
    {
        public static int n, k, m, a, max = 1000;
        public static List<int> b = new List<int>();

        public static int Mod (int a, int b)
        {
            int resp = ((a % b) + b) % b;
            return resp;
        }

        public static void CalculaKeM() //calcula k e m tal que n - 1 = 2^(k) * m
        {
            int quociente = n - 1;
            int divisor = n - 1;
            int dividendo = 0;
            int potencia = 1;
            while (quociente % 2 == 0)
            {
                dividendo = (int)(Math.Pow(2, potencia));
                quociente = divisor/dividendo;
                potencia ++;
            }
            k = potencia - 1;
            m = quociente;
        }

        public static void EscolheA() //aleatoriamente escolha a
        {
            Random random = new Random();
            a = random.Next(2, n);
        }

        public static void CalculaB0() // bo=a^(m) mod n
        {
            int aElevadoM = ((int)(Math.Pow(a, m)));
            int b0 = Mod(aElevadoM, n);
            b.Add(b0);
        }

        public static bool CheckB0() //retorna n é primo se b0 = 1 ou b0 = -1
        {
            if(b[0] == 1 || b[0] == -1)
            {
                return true;
            }
            return false;
        }

        public static bool CalculaBn(int i) //bn = b[n-1]^(2) mod n
        {
            double bElevado2 = Math.Pow(b[i - 1], 2);
            int bi = Mod((int)(bElevado2), n);
            b.Add(bi);

            if (b[i] == 1)
            {
                return false;
            }
            else if (b[i] == n - 1)
            {
                return true;
            }

            return false;
        }

        public static bool MillerRabin()
        {
            CalculaKeM(); //calcular k e m tal que n - 1 = 2^(k) * m
            //EscolheA();
            a = 2;
            CalculaB0();
            bool CheckB0_resp = CheckB0();
            if (!CheckB0_resp)
            {
                int i = 1;
                bool resp = false;
                while (i < max && resp == false)
                {
                    resp = CalculaBn(i); //calcula b[n] até que encontre-se uma solução
                    i++;
                }
                return resp;
            }
            return CheckB0_resp; //se b[0] for conclusivo, não é preciso calcular b[n]
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Valor de n: ");
            n = int.Parse(Console.ReadLine());

            if (n < 3)
            {
                Console.WriteLine(n + " não é primo");
            }
            else if (n % 2 == 0)
            {
                Console.WriteLine(n + " não é primo");
            }
            else if (MillerRabin())
            {
                Console.WriteLine(n + " é primo");
            }
            else
            {
                Console.WriteLine(n + " não é primo");
            }
        }
    }
}
