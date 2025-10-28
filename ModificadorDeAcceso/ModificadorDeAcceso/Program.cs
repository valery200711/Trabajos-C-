using modificadoresAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ModificadorDeAcceso.Libro;
using static modificadoresAcceso.Empleado;

namespace ModificadorDeAcceso
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //--------CLASE VEHICULO--------
            //Vehiculo vehiculo1 = new Vehiculo("Toyota", "Rojo");
            //vehiculo1.MostrarInformacion();

            //Vehiculo.Camion vehiculo2 = new Vehiculo.Camion("Toyota");
            //vehiculo2.MostrarMetodo();

            //--------CLASE EMPLEADO--------
            //Empleado empleado1 = new Empleado("alexis", "Desarrolladora", 3500000);
            //Empleado empleado2 = new Empleado("valery", "Diseñador UX", 2800000);
            //empleado1.MostrarInformacion();
            //empleado2.MostrarInformacion();
            //empleado1.ActualizarSalarioInterno();

            //Gerente gerente1 = new Gerente("steven", "Gerente de Proyecto", 5000000);
            //gerente1.MostrarConBono();

            //--------CLASE LIBRO--------
            //Libro libro1 = new Libro("Cien años de soledad", "Gabriel García Márquez", 496);
            //libro1.MostrarInformacion();
            //libro1.MetodoPrivado();

            //LibroDigital libroD1 = new LibroDigital("1984", "George Orwell", 328, 2.5f);
            //libroD1.MetodoProtegido();

        }
    }
}
