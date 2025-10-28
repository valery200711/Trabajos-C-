using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TALLER1
{
    public class Persona
    {
        public string _nombre { get; set; }
        public int _edad { get; set; }
        public string _genero { get; set; }

        public Persona(string nombre, int edad, string genero)
        {
            _nombre = nombre;
            _edad = edad;
            _genero = genero;
        }

        public List<Persona> personas = new List<Persona>();
        public void agregarUsuario()
        {
            Console.WriteLine("CREAR PERSONA:");
            string nombre = Console.ReadLine();
            int edad = int.Parse(Console.ReadLine());
            string genero = Console.ReadLine();
            if (genero == "F" || genero == "f" || genero == "M" || genero == "m")
            {
                personas.Add(new Persona(nombre, edad, genero));
                Console.WriteLine($"Persona agregada correctamente ");
            }
            else
            {
                Console.WriteLine($"Tienes que ingresar una de las dos opcines ");

            }
        }

        public void mostrarPersonas()
        {
            Console.WriteLine("MOSTRAR INFORMACION:");
            Console.WriteLine("Lista de personas registradas:");

            if (personas.Count == 0)
            {
                Console.WriteLine("No hay personas registradas ");
            }

            foreach (var persona in personas)
            {
                Console.WriteLine($"Nombre: {persona._nombre}, Edad: {persona._edad}, Genero: {persona._genero} ");
            }
        }

        public void editarPersona()
        {
            Console.WriteLine("EDITAR DATO:");
            if (personas.Count > 0)
            {
                Console.WriteLine("Ingrese el numero de usuario que quiere editar: ");
                int numero = int.Parse(Console.ReadLine());

                if (numero < 0 || numero > personas.Count)
                {
                    Console.WriteLine("El numero no existe");
                    return;
                }

                Console.WriteLine("Ingrese el dato que desea editar:" +
                "1 Nombre" +
                "2 Edad" +
                "3 Genero ");
                int opcion = int.Parse(Console.ReadLine());

                Persona personaSeleccionada = personas[numero - 1];

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese el nuevo nombre");
                        string nuevoNombre = Console.ReadLine();
                        personaSeleccionada._nombre = nuevoNombre;
                        Console.WriteLine("Nombre actualizado correctamente");
                        break;
                    case 2:
                        Console.WriteLine("Ingrese la nueva edad");
                        int nuevaEdad =int.Parse(Console.ReadLine());
                        personaSeleccionada._edad = nuevaEdad;
                        Console.WriteLine("Edad actualizado correctamente");
                        break;
                    case 3:
                        Console.WriteLine("Ingrese el nuevo genero (F-M)");
                        string nuevoGenero = Console.ReadLine();
                        if (nuevoGenero == "F" || nuevoGenero == "f" || nuevoGenero == "M" || nuevoGenero == "m")
                        {
                            personaSeleccionada._genero = nuevoGenero;
                            Console.WriteLine("Genero actualizado correctamente");
                        }
                        break;
                    default:
                        Console.WriteLine("Opcion incorrecta");
                        break;
                }

                personas[numero - 1] = personaSeleccionada;

            }
            else
            {
                Console.WriteLine("No hay personas registradas");
            }
        }

        public void calcularEdadEnDias()
        {
            Console.WriteLine("EDAD EN DIAS:");
            if (personas.Count > 0)
            {
                Console.WriteLine("Ingrese el numero de usuario: ");
                int numero = int.Parse(Console.ReadLine());

                if (numero < 0 || numero > personas.Count)
                {
                    Console.WriteLine("El numero no existe");
                    return;
                }

                Persona personaSeleccionada = personas[numero - 1];
                int edadDias = personaSeleccionada._edad * 365;
                Console.WriteLine($"Tu edad en dias es aproximadamente: {edadDias}");
            }
            else
            {
                Console.WriteLine("No hay personnas registradas");
            }
        }

    }
}
