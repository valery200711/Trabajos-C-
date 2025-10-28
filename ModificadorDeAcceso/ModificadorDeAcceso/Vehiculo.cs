using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modificadoresAcceso
{
    internal class Vehiculo
    {
        private string _marca { get; set; }
        private string _modelo { get; set; }

        private float _kilometraje = 1500;

        public Vehiculo(string marca, string modelo)
        {
            _marca = marca;
            _modelo = modelo;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"INFORMACION DEL VEHICULO");
            Console.WriteLine($" Marca {_marca} " +
                $" Modelo {_modelo} " +
                $" Kilometraje {_kilometraje} ");
        }

        protected void CostoMantenimiento()
        {
            Console.WriteLine($"COSTO DEL MANTENIMIENTO");
            float manoObra = 70.000F;
            float costoMateriales = 100.000F;
            float costoEnergia = 90.000F;
            float servicioExterno = 10.000F;

            float costoVariable = manoObra + costoMateriales + costoEnergia + servicioExterno;

            float isd = 50000;
            float garaje = 5000;

            float costosFijo = isd + garaje;

            float costoTotal = costoVariable + costosFijo;
            float total = costoTotal / _kilometraje;

            Console.WriteLine($"Mano de obra: {manoObra}" +
                $" Costo del material {costoMateriales}" +
                $" Costo de la energia {costoEnergia}" +
                $" Servicios externos {servicioExterno}" +
                $" Impuestos, seguros, y depreciación del vehículo {isd}" +
                $" Garaje {garaje}" +
                $" Total {total}");
        }

        public class Camion : Vehiculo
        {
            public Camion(string marca) : base(marca, "camion")
            {

            }

            public void MostrarMetodo()
            {
                CostoMantenimiento();
            }
        }
    }
}

