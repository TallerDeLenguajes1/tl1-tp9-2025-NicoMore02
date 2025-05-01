// See https://aka.ms/new-console-template for more information
Console.WriteLine("==== Validacion de Directorio ====");

string direccionPath;
bool validacion = false;

while (validacion == false) {
    Console.WriteLine("Ingrese la direccion");
    direccionPath = Console.ReadLine();
    if (Directory.Exists(direccionPath)) {
        validacion = true;
        Console.WriteLine("La direccion existe");
        string[] Carpetas = Directory.GetDirectories(direccionPath);
        Console.WriteLine("----Carpetas----");
        foreach (var carpetas in Carpetas)
        {
            Console.WriteLine($"- {Path.GetFileName(carpetas)}");
        }

        string[] Archivos = Directory.GetFiles(direccionPath);
        Console.WriteLine("----Archivos----");
        foreach (var archivos in Archivos)
        {
            FileInfo info = new FileInfo(archivos);
            double tamaño = info.Length / 1024.0;
            Console.WriteLine($"- {Path.GetFileName(archivos)}, tamaño: {tamaño} KB");
        }

        string reporte = Path.Combine(direccionPath, "Reporte.csv");
        using (StreamWriter repor = new StreamWriter(reporte)) {
            string[] Rep = Directory.GetFiles(direccionPath);
            foreach (var rep in Rep)
            {
                FileInfo info = new FileInfo(rep);
                double tamaño = info.Length / 1024.0;
                string fechaMod = info.LastWriteTime.ToString("dd-MM-yyyy");
                repor.WriteLine($"- {Path.GetFileName(rep)}, tamaño: {tamaño} KB, fecha de Modificacion: {fechaMod}");
            }
        }

        
    } else {
        Console.WriteLine("La direccion no existe, Ingrese nuevamente");
    }
}

