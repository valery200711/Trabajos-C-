using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace modificadoresAcceso
{
    internal class Empleado
    {
        private string _nombre { get; set; }
        private string _cargo { get; set; }
        private float _salario { get; set; }

        public Empleado(string nombre, string cargo, float salario)
        {
            _nombre = nombre;
            _cargo = cargo;
            _salario = salario;
        }
        public void MostrarInformacion()
        {
            Console.WriteLine("INFORMACIÓN DEL EMPLEADO");
            Console.WriteLine($"Nombre: {_nombre}");
            Console.WriteLine($"Cargo: {_cargo}");
            Console.WriteLine($"Salario: {_salario}");
        }

        private void ModificarSalario()
        {
            Console.WriteLine($"MODIFICADOR DE SALARIO");
            if (_salario <= 0)
            {
                Console.WriteLine($"No hay salario disponible");
            }
            else
            {

                Console.WriteLine($"Salario actual: {_salario}");
                Console.Write("Ingrese el nuevo salario: ");
                float nuevoSalario = float.Parse(Console.ReadLine());
                _salario = nuevoSalario;
                Console.WriteLine($"Salario actualizado: {_salario}");

            }
        }
        public void ActualizarSalarioInterno()
        {
            ModificarSalario();
        }
        protected float ObtenerSalario()
        {
            return _salario;
        }

        public class Gerente : Empleado
        {
            public Gerente(string nombre, string cargo, float salario) : base(nombre, cargo, salario)
            {

            }

            public void MostrarConBono()
            {
                float salario = ObtenerSalario();
                float bono = salario * 0.10f; 
                float salarioConBono = salario + bono;

                Console.WriteLine("INFORMACIÓN DEL GERENTE");
                Console.WriteLine($"Salario base: {salario}");
                Console.WriteLine($"Bono del 10%: {bono}");
                Console.WriteLine($"Salario total con bono: {salarioConBono}");
            }
        }

    }
}