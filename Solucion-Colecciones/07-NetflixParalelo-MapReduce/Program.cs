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
            var sw = System.Diagnostics.Stopwatch.StartNew();

            //
            // Procesar argd-line args, bienvenido msg:
            //
            string infile = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\ratings.txt";

            // 
            // Para cada registro, analice y agregue los pares <userid, num reviews>:
            //
            sw.Restart();

            Dictionary<int, int> ReviewsByUser = new Dictionary<int, int>();

            Parallel.ForEach(
                File.ReadLines(infile), // 1: Origen de datos

                () => { return new Dictionary<int, int>(); }, // 2: Inicializar almacen de datos local (tls)

                (line, loopState, tls) => // 2: Cuerpo de la tarea
                {
                    int userid = parse(line);

                    if (!tls.ContainsKey(userid))  // primera revisión:
                        tls.Add(userid, 1);
                    else  // otra revisión por el mismo usuario:
                        tls[userid]++;

                    return tls;
                },
                (tls) => //4: Finalizador - Reducir
                {
                    lock (ReviewsByUser)
                    {
                        foreach (int userid in tls.Keys)
                        {
                            int cantVotos = tls[userid];

                            if (!ReviewsByUser.ContainsKey(userid))
                                ReviewsByUser.Add(userid, cantVotos);
                            else
                                ReviewsByUser[userid] += cantVotos;
                        }
                    }
                }
            );

            //
            // Ordena los pares por número de comentarios, orden descendente, y toma el top 10:
            //
            var top10 = ReviewsByUser.OrderByDescending(x => x.Value).Take(10);

            long timems = sw.ElapsedMilliseconds;

            //
            // Escribe los resultados:
            //
            Console.WriteLine();
            Console.WriteLine("** Top 10 users reviewing movies:");

            foreach (var user in top10)
                Console.WriteLine("{0}: {1}", user.Key, user.Value);

            // 
            // Hecho:
            //
            double time = timems / 1000.0;  // convertir milisegundos a segundos

            Console.WriteLine();
            Console.WriteLine("** Done! Time: {0:0.000} secs", time);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.Write("Press a key to exit...");
            Console.ReadKey();
        }


        /// <summary>
        /// Analiza una línea del archivo de datos de netflix y devuelve el ID de usuario que revisó la película.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
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