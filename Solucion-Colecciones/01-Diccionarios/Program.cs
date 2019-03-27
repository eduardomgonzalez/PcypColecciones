using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Diccionarios
{
    class Program
    {
        static void Main(string[] args)
        {
            //implementacion del diccionario con sus valores respectivos
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Juan", "55423412");
            dic.Add("Ernesto", "56985623");
            dic.Add("Mariana", "54787451");

            //a)
            if (dic.ContainsKey("Juan"))
            {
                Console.WriteLine(dic["Juan"]); //accedo al valor
                Console.ReadKey();
            }

            //b)
            string nombre;
            if (dic.TryGetValue("Pedro", out nombre))
            {
                Console.WriteLine(nombre);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No contiene la llave");
                Console.ReadKey();
            }

            //c)
            // Mostrar todas las entradas en el diccionario
            // KeyValuePair<string, string> se puede reemplazar por var.
            foreach (KeyValuePair<string, string> entrada in dic)
            {
                Console.WriteLine("{0}, {1}", entrada.Key, entrada.Value);
            }
            Console.ReadKey();

            //d)
            // cambiar el valor de una llave
            dic["Mariana"] = "58251425";
            Console.WriteLine("Nuevo teléfono de Mariana: {0}", dic["Mariana"]);
            Console.ReadKey();
        }
    }
}