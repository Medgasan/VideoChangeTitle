using VideoChangeTitle;
using File = System.IO.File;
using static VideoChangeTitle.Utils;

namespace videochangetitle
{
    class Program
    {

        private static string? myPath;


        static async Task Main(string[] args)
        {
            myPath = @".\";
            if (args.Length != 0)
            {
                myPath = Path.GetFullPath(args[0]);
            }

            Program videoChange = new();
            await videoChange.ChangeTitle2Async();
            Console.ReadKey();
        }

      
        private async Task ChangeTitle2Async()
        {
            try
            {

                IEnumerable<string> d = Directory.GetFiles(myPath, "*.*", SearchOption.AllDirectories)
                                    .Where(s => s.EndsWith(".mkv") || s.EndsWith(".avi") || s.EndsWith(".mp4"));
                foreach (string videoPath in d)
                {
                    try
                    {
                        println("");
                        println("========== Video Actual ==========");
                        println(videoPath);
                        println("------------");

                        string cleanFile = NormalizeFile(videoPath);

                        string cleanFileName = Path.GetFileName(cleanFile);

                        // Abre las meta tags del archivo
                        TagLib.File file = TagLib.File.Create(cleanFile);
                        println("Título actual en el archivo:");
                        println(file.Tag.Title); // muestra el título actual por consola
                        println("----------");
                        string old = file.Tag.Title;
                        string[] filedata = parseTitle(cleanFileName);
                        string newTitle = await TheMovieDB.getTitle(filedata[0], filedata[1], filedata[2]); // obtiene el nuevo título
                        newTitle = newTitle.Replace(".", "");
                        println($"Título nuevo: {newTitle}");
                        // Asigna el nuevo título
                        file.Tag.Title = newTitle;
                        file.Tag.Comment = $"Título Actualizado el:{DateTime.Now.Date}";
                        println("Actualizando título...");
                        file.Save();
                        println($"Nuevo título en archivo :{file.Tag.Title}");
                        println("==================================");
                    }
                    catch (Exception e)
                    {
                        println("===================================");
                        println($"Program Error: {e.Message}");
                        println("===================================");
                    }

                }

            }
            catch (Exception e)
            {
                println("===================================");
                println(e.Message);
                println("===================================");
            }

        }

        private static string NormalizeFile(string videoPath)
        {
            string ext = Path.GetExtension(videoPath);
            string cleanFile = videoPath.Replace(ext, "");

            // quita puntos en el nombre del archivo excepto el de la extensión y renombra el archivo con el nuevo nombre
            if (cleanFile.Contains("."))
            {
                cleanFile = cleanFile.Replace(".", " ");
                File.Move(videoPath, cleanFile + ext);
                println("Archivo sin puntos: " + cleanFile + ext);
            }

            // apuntamos al nuevo archivo 
            cleanFile += ext;
            return cleanFile;
        }
    }
}
