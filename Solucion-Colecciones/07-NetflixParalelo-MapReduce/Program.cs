/* Program.cs */

//
// Sequential C# Version
//
// Sequentially processes a text file of movie reviews, one per line, 
// the format of which are:
//
//   movie id, user id, rating (1..5), date (YYYY-MM-DD)
//
// The output are the top 10 users who reviewed the most movies, in 
// descending order (so the user with the most reviews is listed 
// first).
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace top10
{
    class Program
    {

        static void Main(string[] args)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            //
            // Process cmd-line args, welcome msg:
            //
            string infile = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\ratings.txt";

            // 
            // Foreach record, parse and aggregate the pairs <userid, num reviews>:
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
                (tls) => //4: Finalizer - Reduce
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
            // Sort pairs by num reviews, descending order, and take top 10:
            //
            var top10 = ReviewsByUser.OrderByDescending(x => x.Value).Take(10);

            long timems = sw.ElapsedMilliseconds;

            //
            // Write out the results:
            //
            Console.WriteLine();
            Console.WriteLine("** Top 10 users reviewing movies:");

            foreach (var user in top10)
                Console.WriteLine("{0}: {1}", user.Key, user.Value);

            // 
            // Done:
            //
            double time = timems / 1000.0;  // convert milliseconds to secs

            Console.WriteLine();
            Console.WriteLine("** Done! Time: {0:0.000} secs", time);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.Write("Press a key to exit...");
            Console.ReadKey();
        }


        /// <summary>
        /// Parses one line of the netflix data file, and returns the userid who reviewed the movie.
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

    }//class
}//namespace