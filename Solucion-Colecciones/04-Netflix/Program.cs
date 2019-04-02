using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace _04_Netflix
{
    class Program
    {
        /*En el archivo “ratings.txt” se encuentran las puntuaciones realizadas por los usuarios de Netflix para cada película. 
         El formato del archivo es el siguiente:
         <identificador de película>,<identificador de usuario>,<puntaje asignado>,<fecha del puntaje>
         Se solicita realizar un top 10 de los usuarios que más puntuaciones hicieron en Netflix.*/

        private static Dictionary<int, int> dic = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            StreamReader objReader = new StreamReader("c:\\ratings.txt");//Me da error cuando pongo este archivo en la solucion y lo quiero subir a GitHub
            string sLine = "";
            List<string> arrText = new List<string>();

            //Lectura
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    arrText.Add(sLine);

                    string[] cadenaFinal = sLine.Split(',');

                    int usuario = Convert.ToInt32(cadenaFinal[1]);

                    validadYCargarUsuario(usuario, cadenaFinal);
                }

            }
            objReader.Close();

            mostrarDiccionario(dic);

        }

        private static void mostrarDiccionario(Dictionary<int, int> diccionario)
        {
            foreach (var entrada in diccionario.OrderByDescending(pair => pair.Value).Take(10))//imprime los 10 primeros
            {
                Console.WriteLine("{0} - {1}", entrada.Key, entrada.Value);
            }
            Console.ReadKey();
        }
        
       private static void validadYCargarUsuario(int us, string[] cadena)
        {
            if (dic.ContainsKey(us))
            {

                int puntajeTxt = Convert.ToInt32(cadena[2]);
                int puntajeDiccionario;

                if (dic.TryGetValue(us, out puntajeDiccionario))
                {
                    puntajeDiccionario += puntajeTxt;
                    dic.Remove(us);
                    dic.Add(us, puntajeDiccionario);
                }
            }
            else
            {
                dic.Add(us, Convert.ToInt32(cadena[2]));
            }

        }
    }
}
/*
 * Otra forma de leer un txt y mostrarlo por pantalla
string text = System.IO.File.ReadAllText(@"C:\ratings.txt");
System.Console.WriteLine("Contenido del archivo = {0}", text);
Console.WriteLine("Presione cualquier tecla para salir.");
System.Console.ReadKey();
*/
