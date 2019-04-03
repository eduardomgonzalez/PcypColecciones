using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_MultiplicacionMatrices
{
    class Program
    {
        static void Main(string[] args)
        {
            int fila1;
            int columna1;
            int fila2;
            int columna2;

            mostrarMensaje("Multiplicar dos matrices\n");

            mostrarMensaje("Ingrese la cantidad de filas para la matriz 1: ");
            fila1 = Int32.Parse(Console.ReadLine());
            mostrarMensaje("Ingrese la cantidad de columnas para la matriz 1: ");
            columna1 = Int32.Parse(Console.ReadLine());

            mostrarMensaje("Ingrese la cantidad de filas para la matriz 2: ");
            fila2 = Int32.Parse(Console.ReadLine());
            mostrarMensaje("Ingrese la cantidad de columnas para la matriz 2: ");
            columna2 = Int32.Parse(Console.ReadLine());

            mostrarMensaje("CARGA COMPLETA.\n");                       

            int[,] matriz1 = new int[fila1, columna1];
            int[,] matriz2 = new int[fila2, columna2];
            int[,] matrizResultado = new int[fila1, columna2];


            if (columna1 == fila2) //Validacion para saber si se pueden multiplicar las matrices
            {
                cargarMatriz(matriz1, fila1, columna1);
                cargarMatriz(matriz2, fila2, columna2);
                mostrarMensaje("Primera Matriz:\n");
                mostrarMatriz(matriz1, fila1, columna1);
                mostrarMensaje("Segunda Matriz:\n");
                mostrarMatriz(matriz2, fila2, columna2);

                mostrarMensaje("\nResultado de Multiplicación de ambas matrices: \n");
                multiplicarMatirces(matrizResultado, matriz1, matriz2, fila1, columna2, columna1);
                mostrarMatriz(matrizResultado, fila1, columna2);
            }
            else
            {
                mostrarMensaje("No se pueden multiplicar las matrices debido a que la columna " +
                    "de la primera no coincide con la fila de la segunda");
                Console.ReadKey();
            }

        }

        private static void multiplicarMatirces(int[,] mat, int[,] mat1, int[,] mat2, int fila, int columna, int col)
        {
            for (int i = 0; i < fila; i++)
            {
                for (int j = 0; j < columna; j++)
                {
                    
                    for (int k = 0; k < col; k++)
                    {                        
                        mat[i, j] += mat1[i, k] * mat2[k, j];
                    }
                    
                }
            }
        }

        private static void mostrarMatriz(int[,] matriz, int f, int c)
        {
            for (int i = 0; i < f; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    Console.Write(matriz[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        private static void cargarMatriz(int[,] matriz, int f, int c)
        {
            for (int i = 0; i < f; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    Console.Write("Ingrese posicion [" + (i + 1) + "," + (j + 1) + "]: ");
                    string linea;
                    linea = Console.ReadLine();
                    matriz[i, j] = int.Parse(linea);
                }
            }
            mostrarMensaje("Carga completada.\n");
        }

        private static void mostrarMensaje(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
