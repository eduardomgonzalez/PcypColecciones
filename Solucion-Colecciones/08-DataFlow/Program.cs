using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EjercicioDataFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            var TaskA = Task.Factory.StartNew<int>(() =>
            {
                Console.WriteLine("Tarea A en proceso. Retorno 10.");
                return 10;
            });

            var TaskB = Task.Factory.StartNew<int>(() =>
            {
                Console.WriteLine("Tarea B en proceso. Retorno 20.");
                return 20;
            });

            // Ejecuto una tercer tarea, pero antes espero que se complenten las anteriores
            Task TaskC = Task.Factory.ContinueWhenAll(new Task<int>[] { TaskA, TaskB },
                (tasks) =>

                Console.WriteLine("Suma total de retornos de A + C = {0}", tasks[0].Result + tasks[1].Result)

                );

            TaskC.Wait();
            Console.WriteLine("Presione una tecla...");
            Console.ReadKey();
        }

    }
}
