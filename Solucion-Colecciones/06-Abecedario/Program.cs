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

            //List<string> lista = new List<string>();

            Task t1 = new Task( () =>
            {
                for (byte a = 65; a < 91; a++)
                {
                    if (!esPar(a))
                    {
                        Console.WriteLine("Tarea 1: " + (char)a);
                        //lista.Add("Tarea 1: " + (char)a);
                    }
                    
                }
            });

            Task t2 = new Task(() =>
            {
                for (byte a = 65; a < 91; a++)
                {
                    if (esPar(a))
                    {
                        Console.WriteLine("Tarea 2: " + (char)a);
                        //lista.Add("Tarea 2: " + (char)a);
                    }
                    
                }
            });

            /*
            foreach (var element in lista)
            {

                Console.Write("{0} \n", element);
            }
            Console.ReadKey();
            */

            t1.Start();
            t2.Start();

            Console.ReadKey();

        }

        static bool esPar(int n)
        {
            return n % 2 == 0;
        }
    }
}
