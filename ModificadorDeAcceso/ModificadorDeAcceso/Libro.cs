using modificadoresAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModificadorDeAcceso
{
    internal class Libro
    {
        private string _titulo { get; set; }
        private string _autor { get; set; }
        private int _Npaginas { get; set; }

        public Libro(string titulo, string autor, int npaginas)
        {
            _titulo = titulo;
            _autor = autor;
            _Npaginas = npaginas;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("INFORMACIÓN DEL LIBRO (LIBRO FISICO)");
            Console.WriteLine($"Titulo: {_titulo}");
            Console.WriteLine($"Autor: {_autor}");
            Console.WriteLine($"Numero de paginas: {_Npaginas}");
        }

        private void ModificarPaginas()
        {
            Console.WriteLine($"MODIFICADOR DE PAGINAS");
            if (_Npaginas <= 0)
            {
                Console.WriteLine($"No se encuentran paginas de este libro");
            }
            else
            {

                Console.WriteLine($"Numero de paginas actuales: {_Npaginas}");
                Console.Write("Ingrese el nuevo numero de paginas: ");
                int totalPaginas = int.Parse(Console.ReadLine());
                _Npaginas = totalPaginas;
                Console.WriteLine($"Numero de paginas actualizadas: {_Npaginas}");

            }
        }

        public void MetodoPrivado()
        {
            Console.WriteLine("UTILIZACION DEL METODO PRIVADO");
            ModificarPaginas();
        }

        protected void GenerarResumen()
        {
            Console.WriteLine("RESUMEN");
            Console.WriteLine($"Titulo: {_titulo}");
            Console.WriteLine($"Autor: {_autor}");
            Console.WriteLine($"Numero de paginas: {_Npaginas}");
        }

        public class LibroDigital : Libro
        {
            private float _tamanioArchivo;
            public LibroDigital(string titulo, string autor, int npaginas, float tamanIoArchivo) : base(titulo, autor, npaginas)
            {
                _tamanioArchivo = tamanIoArchivo;

            }

            public void MetodoProtegido()
            {
                Console.WriteLine("UTILIZACION DEL METODO PROTEGIDO CON EL TAMAÑO DE ARCHIVO AGREGADO (LIBRO VIRTUAL)");
                GenerarResumen();
                Console.WriteLine($"Tamaño del archivo digital: {_tamanioArchivo} MB");
            }
        }
    }
}
