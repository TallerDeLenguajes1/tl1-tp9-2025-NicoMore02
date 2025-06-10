// See https://aka.ms/new-console-template for more information
Console.WriteLine("==== Validacion de Directorio ====");

string direccionPath;
bool validacion = false;

while (validacion == false) {
    Console.WriteLine("Ingrese la direccion");
    direccionPath = Console.ReadLine();
    if (Directory.Exists(direccionPath))
    {
        validacion = true;
        Console.WriteLine("La direccion existe");
        mostrarCarpetas(direccionPath);
        mostrarArchivos(direccionPath);
        crearReporte(direccionPath);
    }
    else
    {
        Console.WriteLine("La direccion no existe, Ingrese nuevamente");
    }
}

void mostrarCarpetas(string carpeta)
{
    string[] Carpetas = Directory.GetDirectories(carpeta);
    Console.WriteLine("----Carpetas----");
    foreach (var carpetas in Carpetas)
    {
        Console.WriteLine($"- {Path.GetFileName(carpetas)}");
    }
}

void mostrarArchivos(string carpeta)
{
    string[] Archivos = Directory.GetFiles(carpeta);
    Console.WriteLine("----Archivos----");
    foreach (var archivos in Archivos)
    {
        FileInfo info = new FileInfo(archivos);
        double tamaño = info.Length / 1024.0;
        Console.WriteLine($"- {Path.GetFileName(archivos)}, tamaño: {tamaño} KB");
    }
}

void crearReporte(string carpeta)
{
    string reporte = Path.Combine(carpeta, "Reporte.csv");
    using (StreamWriter repor = new StreamWriter(reporte))
    {
        string[] Rep = Directory.GetFiles(carpeta);
        foreach (var rep in Rep)
        {
            FileInfo info = new FileInfo(rep);
            double tamaño = info.Length / 1024.0;
            string fechaMod = info.LastWriteTime.ToString("dd-MM-yyyy");
            repor.WriteLine($"- {Path.GetFileName(rep)}, tamaño: {tamaño} KB, fecha de Modificacion: {fechaMod}");
        }
    }
}