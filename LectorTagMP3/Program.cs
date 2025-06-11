// See https://aka.ms/new-console-template for more information
using System.Text;
using TagID3v1;
Console.WriteLine("====Lector de Informacion de MP3====");

string direccion;
bool validacion = false;

while (validacion == false)
{
    Console.Write("Ingrese la direccion del archivo MP3: ");
    direccion = Console.ReadLine();
    if (File.Exists(direccion) && Path.GetExtension(direccion).ToLower() == ".mp3")
    {
        validacion = true;
    }
    else
    {
        Console.WriteLine("Archivo no encontrado o no es un archivo MP3");
        Console.WriteLine("Intente nuevamente");
    }

    Id3v1Tag tag = LeerTagID3v1(direccion);

    if (tag != null)
    {
    // Mostrar información del tag
    Console.WriteLine("\n=== Información del Tag ID3v1 ===");
    Console.WriteLine($"Título: {tag.Titulo}");
    Console.WriteLine($"Artista: {tag.Artista}");
    Console.WriteLine($"Álbum: {tag.Album}");
    Console.WriteLine($"Año: {tag.Anio}");
    }
    else
    {
        Console.WriteLine("\nNo se encontró un tag ID3v1 válido en el archivo.");
    }
}



Id3v1Tag LeerTagID3v1(string ruta)
{
    using FileStream mp3 = new FileStream(ruta, FileMode.Open, FileAccess.Read);
    if (mp3.Length < 128)
    {
        Console.WriteLine("El archivo no tiene informacion de ID3v1");
        return null;
    }

    mp3.Seek(-128, SeekOrigin.End);
    byte[] id3v1 = new byte[128];
    mp3.Read(id3v1, 0, 128);

    string header = Encoding.Latin1.GetString(id3v1, 0, 3);
    if (header != "TAG")
    {
        return null;
    }

    return new Id3v1Tag
    {
        Titulo = Encoding.Latin1.GetString(id3v1, 3, 30).TrimEnd('\0', ' '),
        Artista = Encoding.Latin1.GetString(id3v1, 33, 30).TrimEnd('\0', ' '), 
        Album = Encoding.Latin1.GetString(id3v1, 63, 30).TrimEnd('\0', ' '),
        Anio = Encoding.Latin1.GetString(id3v1, 93, 4).TrimEnd('\0', ' ')
    };
}