using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _03_ContadorDePalabras
{
    class Program
    {
        /*1. Contador de palabras
         * Realizar un software que solicite al usuario que ingrese un párrafo por 
         * teclado, el software debe contar la cantidad de ocurrencias de cada palabra.
         
         * Asimismo, deberá indicar la cantidad de palabras distintas que existen en el párrafo ingresado por el usuario*/

        private static Dictionary<string, int> dic = new Dictionary<string, int>();

        static void Main(string[] args)
        {

            //Capturo el parrafo en un string
            string textoIntroducido;
            mostrarMensaje("Introduzca un parrafo: ");
            textoIntroducido = Console.ReadLine();

            //Elimino caracteres especiales
            string sinCaracteres = eliminarCaracteresEspeciales(textoIntroducido);

            //Elimino espacios de mas. Trim elimina espacios adelante y atras
            string sinEspaciosDeMas = eliminarEspaciosDeMas(sinCaracteres);

            //Pongo las palabras en un array de strings utilizando Split
            string[] cadenaFinal = sinEspaciosDeMas.Split();

            //Cargo el diccionario principal
            cargarDiccionario(cadenaFinal, dic);

            //Muestro el diccionario y su cantidad de palabras
            mostrarMensaje("\nContenido del diccionario: \n");
            Console.WriteLine("Key: Value:");
            mostrarDiccionario(dic);
            Console.WriteLine("\nSize: " + dic.Count);
            Console.ReadKey();

        }

        private static void cargarDiccionario(string[] cadenaFinal, Dictionary<string, int> diccionario)
        {
            //Recorro la cadena final y voy ingresando cada posicion en el diccionario
            foreach (var palabra in cadenaFinal)
            {
                if (diccionario.ContainsKey(palabra))
                {
                    int valor;

                    if (diccionario.TryGetValue(palabra, out valor))
                    {
                        valor++;
                        diccionario.Remove(palabra);
                        diccionario.Add(palabra, valor);
                    }
                }
                else
                {
                    diccionario.Add(palabra, 1);
                }
            }

        }

        private static void mostrarDiccionario(Dictionary<string, int> diccionario)
        {
            foreach (var entrada in diccionario)
            {
                Console.WriteLine("{0} - {1}", entrada.Key, entrada.Value);
            }
        }

        private static void mostrarMensaje(string msg)
        {
            Console.WriteLine(msg);
        }

        private static string eliminarCaracteresEspeciales(string texto)
        {
            return Regex.Replace(texto, @"[^\w]", " ", RegexOptions.None, TimeSpan.FromSeconds(1.5));
        }

        private static string eliminarEspaciosDeMas(string texto)
        {
            return Regex.Replace(texto, @"\s+", " ").Trim(); ;
        }
    }
}
