
//for (int i = 0; i < 10; i++)
//{
//    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}, i {i}");
//    Thread.Sleep( 500 );
//}

//Console.WriteLine("---------------------");

//Parallel.For(0, 10, i => {
//    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}, i {i}");
//    Thread.Sleep(500);
//});


CalculateDirSize();

void CalculateDirSize()
{
    var directory = "C:\\Users\\nrodr\\Documents\\2024Q3_ConcurrenciaYSistemasDistribuidos\\src\\Python";

    if (!Directory.Exists(directory))
    {
        Console.WriteLine($"Directorio {directory} no existe.");
        return;
    }

    long totalSize = 0;
    var files = Directory.GetFiles(directory);
    Parallel.For(0, files.Length, i => { 
        var file = new FileInfo(files[i]);
        var size = file.Length;
        Interlocked.Add(ref totalSize, size);
        //totalSize += size;
    });
    Console.WriteLine($"Directorio: {directory}, cantidad de archivos: {files.Length}, tamanio de directorio: {totalSize}");
}