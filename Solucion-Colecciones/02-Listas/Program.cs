using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Listas
{

    class Program
    {
        private static readonly string[] colors = { "MAGENTA", "RED", "WHITE", "BLUE", "CYAN" };
        private static readonly string[] removeColors = { "RED", "WHITE", "BLUE" };

        static void Main(string[] args)
        {

            List<string> list = new List<string>();

            // Añade elementos de colors a la Lista (uno por uno)
            foreach (var color in colors)
            {
                list.Add(color);
            }

            // Añade elementos del arreglo desde el constructor
            List<string> removeList = new List<string>(removeColors);

            Console.WriteLine("List: ");
            MostrarLista(list);

            // remueve colores
            RemueveColores(list, removeList);

            Console.WriteLine("\nLista con colores removidos: ");
            MostrarLista(list);

        }

        private static void MostrarLista(List<string> list)
        {
            // muestra el contenido con foreach
            foreach (var element in list)
            {
                Console.Write("{0} ", element);
            }
            Console.ReadKey();
        }

        private static void RemueveColores(List<string> firstList, List<string> secondList)
        {
            for (int count = 0; count < secondList.Count; ++count)
            {
                firstList.Remove(secondList[count]);
            }
        }

    }
}

