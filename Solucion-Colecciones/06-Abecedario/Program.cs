using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_Abecedario
{
    class Program
    {
        static void Main(string[] args)
        {
            byte letraInicial = 65;
            byte letraFinal = 91;

            Task t1 = new Task( () =>
            {
                while(letraInicial < letraFinal)
                {
                    if (!esPar(letraInicial))
                    {
                        mostrarMensaje("Tarea 1: " + (char)letraInicial);
                        letraInicial++;
                    }
                }
            });

            Task t2 = new Task(() =>
            {
                while (letraInicial < letraFinal)
                {
                    if (esPar(letraInicial))
                    {
                        mostrarMensaje("Tarea 2: " + (char)letraInicial);
                        letraInicial++;
                    }
                }
            });

            t1.Start();
            t2.Start();
            Task.WaitAll(t1, t2);
            Console.ReadKey();
        }

        static bool esPar(int n)
        {
            return n % 2 == 0;
        }

        static void mostrarMensaje(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
