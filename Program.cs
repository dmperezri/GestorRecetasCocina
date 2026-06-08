using System;
using System.IO;

Receta[] recetas = new Receta[50];
int cantidad = 0;
string rutaArchivo = "C:\\Users\\danni\\OneDrive\\Documents\\Mis_Proyectos\\recetas.csv";

int menu()
{
    Console.Clear();
    Console.WriteLine("Recetas Culinarias");
    Console.WriteLine("=================");
    Console.WriteLine("1. Agregar receta");
    Console.WriteLine("2. Mostrar recetas");
    Console.WriteLine("3. Buscar por ingrediente");
    Console.WriteLine("4. Filtrar por tiempo maximo");
    Console.WriteLine("5. Guardar y salir");
    Console.Write("Escriba su opcion: ");
    Console.ForegroundColor = ConsoleColor.Yellow;  
    int op;
    if (!int.TryParse(Console.ReadLine(), out op))
    {
        return 0;
    }
    Console.ResetColor();
    Console.WriteLine();
    return op;
}

void agregarReceta(int pos)
{
    if (pos >= recetas.Length)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Limite de recetas alcanzado.");
        Console.ResetColor();
        Console.ReadKey();
        return;
    }

    Console.Write("Nombre de la receta: ");
    recetas[pos].nombre = Console.ReadLine()!;

    Console.Write("Ingredientes (separados por coma): ");
    recetas[pos].ingredientes = Console.ReadLine()!;

    Console.Write("Pasos (separados por punto y coma): ");
    recetas[pos].pasos = Console.ReadLine()!;

    Console.Write("Tiempo de preparacion (minutos): ");
    if (!int.TryParse(Console.ReadLine(), out int tiempo) || tiempo <= 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Valor invalido. Se asignara 1 minuto.");
        Console.ResetColor();
        tiempo = 1;
    }
    recetas[pos].tiempoPreparacion = tiempo;

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Registro guardado satisfactoriamente");
    Console.ResetColor();

    Console.WriteLine("Presiona una tecla para continuar...");
    Console.ReadKey();
}

void mostrarRecetas(int pos)
{
    if (pos == 0)
    {
        Console.WriteLine("No hay recetas registradas.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("Lista de recetas registradas:");
    for (int i = 0; i < pos; i++)
    {
        if (!string.IsNullOrEmpty(recetas[i].nombre))
        {
            Console.WriteLine($"Receta [{i + 1}]: {recetas[i].nombre}");
            Console.WriteLine($"Ingredientes: {recetas[i].ingredientes}");
            Console.WriteLine($"Pasos: {recetas[i].pasos}");
            Console.WriteLine($"Tiempo: {recetas[i].tiempoPreparacion} min");
            Console.WriteLine();
        }
    }

    Console.WriteLine("Presiona una tecla para continuar...");
    Console.ReadKey();
}

void buscarPorIngrediente(int pos)
{
    Console.Write("Ingrediente a buscar: ");
    string ingrediente  = Console.ReadLine()!.ToLower();

    bool encontrado = false;
    Console.WriteLine("Resultados de la busqueda:");

    for (int i = 0; i < pos; i++)
    {
        if (!string.IsNullOrEmpty(recetas[i].nombre) &&
            recetas[i].ingredientes.ToLower().Contains(ingrediente))
        {
            Console.WriteLine($"- {recetas[i].nombre} ({recetas[i].tiempoPreparacion} min)");
            Console.WriteLine($"  Ingredientes: {recetas[i].ingredientes}");
            encontrado = true;
        }
    }

    if (!encontrado)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No se encontraron recetas con ese ingrediente.");
        Console.ResetColor();
    }

    Console.WriteLine("Presiona una tecla para continuar...");
    Console.ReadKey();
}

void filtrarPorTiempo(int pos)
{
    Console.Write("Tiempo maximo en minutos: ");
    if (!int.TryParse(Console.ReadLine(), out int maxTiempo) || maxTiempo <= 0)
    {
        Console.WriteLine("Valor invalido.");
        Console.ReadKey();
        return;
    }

    bool encontrado = false;
    Console.WriteLine($"Recetas con tiempo <= {maxTiempo} minutos:");

    for (int i = 0; i < pos; i++)
    {
        if (!string.IsNullOrEmpty(recetas[i].nombre) &&
            recetas[i].tiempoPreparacion <= maxTiempo)
        {
            Console.WriteLine($"- {recetas[i].nombre}: {recetas[i].tiempoPreparacion} min");
            encontrado = true;
        }
    }

    if (!encontrado)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No hay recetas dentro de ese limite de tiempo.");
        Console.ResetColor();
    }

    Console.WriteLine("Presiona una tecla para continuar...");
    Console.ReadKey();
}

void guardarDatos(int pos)
{
    StreamWriter archivo = new StreamWriter(rutaArchivo);
    for (int i = 0; i < pos; i++)
    {
        if (!string.IsNullOrEmpty(recetas[i].nombre))
        {
            archivo.WriteLine($"{recetas[i].nombre};" +
                $"{recetas[i].ingredientes};" +
                $"{recetas[i].pasos};" +
                $"{recetas[i].tiempoPreparacion}");
        }
    }
    archivo.Close();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Registros guardados satisfactoriamente");
    Console.ResetColor();
    Console.WriteLine("Presiona una tecla para continuar...");
    Console.ReadKey();
}

int main()
{
    int op = 0;

    do
    {
        op = menu();
        switch (op)
        {
            case 1:
                agregarReceta(cantidad);
                if (!string.IsNullOrEmpty(recetas[cantidad].nombre))
                    cantidad++;
                break;
            case 2:
                mostrarRecetas(cantidad);
                break;
            case 3:
                buscarPorIngrediente(cantidad);
                break;
            case 4:
                filtrarPorTiempo(cantidad);
                break;
            case 5:
                guardarDatos(cantidad);
                Console.WriteLine("Saliendo del programa...");
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opcion invalida, por favor intente nuevamente");
                Console.ResetColor();
                break;
        }
    }
    while (op != 5);

    return 0;
}

main();

struct Receta
{
    public string nombre;
    public string ingredientes;
    public string pasos;
    public int tiempoPreparacion;
}