using System;
using System.Collections.Generic;
using System.Linq;


class Encuesta
{
    private static int contadorEncuestas = 0;

    public int NumeroEncuesta { get; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string CorreoElectronico { get; set; }
    public bool CarroPropio { get; set; }

    public Encuesta(string nombre, string apellido, DateTime fechaNacimiento, string correoElectronico, bool carroPropio)
    {
        NumeroEncuesta = ++contadorEncuestas;
        Nombre = nombre;
        Apellido = apellido;
        FechaNacimiento = fechaNacimiento;
        CorreoElectronico = correoElectronico;
        CarroPropio = carroPropio;
    }

    public int CalcularEdad()
    {
        int edad = DateTime.Today.Year - FechaNacimiento.Year;
        if (FechaNacimiento.Date > DateTime.Today.AddYears(-edad))
            edad--;
        return edad;
    }
}

class Program
{
    static List<Encuesta> encuestas = new List<Encuesta>();

    static void Main(string[] args)
    {
        int opcion;
        do
        {
            Console.WriteLine("Menú:");
            Console.WriteLine("1. Registrar encuesta");
            Console.WriteLine("2. Generar reporte");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    RegistrarEncuesta();
                    break;
                case 2:
                    GenerarReporte();
                    break;
                case 3:
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        } while (opcion != 3);
    }

    static void RegistrarEncuesta()
    {
        Console.WriteLine("\nRegistro de Encuesta:");
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();
        Console.Write("Apellido: ");
        string apellido = Console.ReadLine();
        Console.Write("Fecha de nacimiento (yyyy-MM-dd): ");
        DateTime fechaNacimiento = DateTime.Parse(Console.ReadLine());
        Console.Write("Correo Electrónico: ");
        string correoElectronico = Console.ReadLine();
        Console.Write("¿Tiene carro propio? (Sí/No): ");
        bool carroPropio = Console.ReadLine().Equals("Sí", StringComparison.OrdinalIgnoreCase);

        // Validaciones
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(correoElectronico))
        {
            Console.WriteLine("Todos los campos son obligatorios.");
            return;
        }

        int edad = DateTime.Today.Year - fechaNacimiento.Year;
        if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
            edad--;

        if (edad < 18 || edad > 50)
        {
            Console.WriteLine("La edad debe estar entre 18 y 50 años.");
            return;
        }

        // Crear y guardar la encuesta
        Encuesta encuesta = new Encuesta(nombre, apellido, fechaNacimiento, correoElectronico, carroPropio);
        encuestas.Add(encuesta);
        Console.WriteLine("Encuesta registrada exitosamente.");
    }

    static void GenerarReporte()
    {
        int totalEncuestas = encuestas.Count;
        int conCarroPropio = encuestas.Count(encuesta => encuesta.CarroPropio);
        int sinCarroPropio = totalEncuestas - conCarroPropio;

        Console.WriteLine("\nReporte:");
        Console.WriteLine($"Total de encuestas realizadas: {totalEncuestas}");
        Console.WriteLine($"Cantidad de personas con carro propio: {conCarroPropio}");
        Console.WriteLine($"Cantidad de personas sin carro propio: {sinCarroPropio}");
    }
}
