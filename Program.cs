using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_lab_FP
{

    internal class Program
    {
        static string[] codigos = new string[6] { "OC-101", "OC-102", "OC-103", "OC-104", "OC-105", "OC-106" };
        static string[] proveedores = new string[6] {"TechCorp", "LogiExpress", "InsumosGlobal", "TechCorp", "AlfaSuministros", "ServiRedes" };
        static double[] precios = new double[6] { 150.0, 450.0, 80.0, 1200.0, 25.0, 600.0 };
        static double[] cantidades = new double[6] { 10, 2, 50, 1, 100, 3 };
        static double[] matriz_montos = new double[6]; //matriz que almacenará los montos de mayor a menor para ordenar las demás matrices.
        static double monto_neto;
        static double total_acumulado=0;
        static int contador_monto = 0;
        static int contador_compras_mayores = 0; // >1 000

        static bool validar_datos() 
        {
            bool alerta = true;
            for (int i = 0; i < precios.Length; i++) 
            {
                if (precios[i] <= 0 || cantidades[i]<=0) 
                {
                    Console.WriteLine("Alerta. Datos negativos o ceros. Por favor ingrese solo números positivos para ejecutar el programa.");
                    return false;
                }
            }
            Console.WriteLine("Validación de datos completada. Bienvenido.");
            return alerta;
        }
        static void Busqueda() 
        {
            while (true)
            {
                Console.Write("Ingrese un código para buscar: ");
                string buscar = Console.ReadLine().ToUpper();

                if (!String.IsNullOrEmpty(buscar))
                {
                    for (int i = 0; i < codigos.Length; i++)
                    {
                        if (buscar == codigos[i])
                        {
                            monto_neto = precios[i] * cantidades[i];

                            Console.WriteLine($"\t * Buscando '{buscar}': ¡ENCONTRADO!  Proveedor: {proveedores[i]} | Monto: ${matriz_montos[i]}");
                            contador_monto++;

                            return;
                        }
                    }
                    Console.WriteLine("No se ha encontrado el código ingresado.");
                    return;

                }
                else 
                {
                    Console.WriteLine("Por favor, ingrese un código válido.");
                }
            }
        }
        static void Ordenes() 
        {
            //Calculo de los montos y contadores:
            for (int i = 0; i < codigos.Length; i++) 
            {
                monto_neto = precios[i] * cantidades[i];
                matriz_montos[i] = monto_neto;
                contador_monto++;
                total_acumulado += monto_neto;
                if (monto_neto > 1000) 
                {
                    contador_compras_mayores++;
                }
            }
            //Ordenamiento burbuja referencial:
            for (int j = 0; j < matriz_montos.Length-1; j++) 
            {
                double aux;
                string cods_temp;
                string prov_temp;
                
                for (int k = 0; k < matriz_montos.Length - 1 - j; k++) 
                {
                    if (matriz_montos[k] < matriz_montos[k + 1]) 
                    {
                        aux = matriz_montos[k];
                        matriz_montos[k] = matriz_montos[k + 1];
                        matriz_montos[k + 1] = aux;

                        cods_temp = codigos[k];
                        codigos[k] = codigos[k + 1];
                        codigos[k + 1] = cods_temp;

                        prov_temp = proveedores[k];
                        proveedores[k] = proveedores[k + 1];
                        proveedores[k + 1] = prov_temp;
                    }
                }
            }
        }
        static void Reporte() 
        {
            Ordenes();
            for (int i = 0; i < codigos.Length; i++) 
            {
                monto_neto = precios[i] * cantidades[i];
                Console.WriteLine($"{i + 1}. Código: {codigos[i]} | Prov: {proveedores[i]} | Monto: $ {matriz_montos[i]}");
                contador_monto++;
            }
        }

        static void Main(string[] args) 
        {
            if (validar_datos())
            {
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("==================================================================");
                Console.WriteLine("\tCONTROL DE ÓRDENES DE COMPRA");
                Console.WriteLine("==================================================================");

                Console.WriteLine("--- REPORTE ORDENADO POR MONTO NETO(DECRECIENTE) ---");
                Reporte();

                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine("MÉTRICAS RESUMEN:");
                Console.WriteLine($"\t * Gran total acumulado: ${total_acumulado}");
                Console.WriteLine($"\t * Compras críticas (>$1000): {contador_compras_mayores} órdenes");

                Console.WriteLine("------------------------------------------------------------------");
                Busqueda();
                Console.WriteLine("==================================================================");
            }
        }
 

        
    }
}
