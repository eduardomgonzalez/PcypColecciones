using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _08___Netflix_MapReduce
{
    class Program
    {
        static void Main(string[] args)
        {
            string infile = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\ratings.txt";
            //string[] readText = File.ReadAllLines(infile);
            int count = 0;
            Stopwatch tiempo = new Stopwatch();
            Dictionary<int, int> ReviewsByUser = new Dictionary<int, int>(999999);


            tiempo.Start();

            string[] archivo = File.ReadAllLines(infile);

            Parallel.ForEach(
                archivo, // Datasource

                () => 0, // Initilizer

                (linea, loopState, tls) => //Task body
                {                   
                    int userid = parse(linea);

                    if (!ReviewsByUser.ContainsKey(userid))  // primera revisión:
                        ReviewsByUser.Add(userid, 1);
                    else  // otra revisión por el mismo usuario:
                        ReviewsByUser[userid]++;           

                    return tls;
                },
                (tls) => //Finalizer
                {                    
                    Interlocked.Add(ref count, tls);
                }
            );            

            var top10 = ReviewsByUser.OrderByDescending(x => x.Value).Take(10);

            tiempo.Stop();

            // Resultados:          
            Console.WriteLine();
            Console.WriteLine("** Top 10 users reviewing movies:");

            foreach (var user in top10)
                Console.WriteLine("{0}: {1}", user.Key, user.Value);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("El tiempo que tardo es: " + tiempo.ElapsedMilliseconds / 1000.0 + " segundos");
            Console.ReadKey();
        }

        private static int parse(string line)
        {
            char[] separators = { ',' };

            string[] tokens = line.Split(separators);

            int movieid = Convert.ToInt32(tokens[0]);
            int userid = Convert.ToInt32(tokens[1]);
            int rating = Convert.ToInt32(tokens[2]);
            DateTime date = Convert.ToDateTime(tokens[3]);

            return userid;
        }
    }
}
