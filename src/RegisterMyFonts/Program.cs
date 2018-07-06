using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RegisterMyFonts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new FontLoaderOptions(args);
            var loader = options.HasOptions ? new FontLoader(options) : new FontLoader();

            if (options.Version)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                Console.WriteLine($"{fvi.ProductName} v{fvi.FileMajorPart}.{fvi.FileMinorPart}");
            }

            if (options.Verbose)
            {
                var fg = Console.ForegroundColor;
                var bg = Console.BackgroundColor;

                var counter = 0;
                Console.ForegroundColor = ConsoleColor.Cyan;
                foreach (var font in loader.GetFontList())
                {
                    Console.WriteLine($"Installing {Path.GetFileNameWithoutExtension(font)} ({Path.GetExtension(font)})");
                    counter += loader.AddFont(font);
                }

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Registered {counter} fonts");
                Console.WriteLine("Done");

                Console.ForegroundColor = fg;
                Console.BackgroundColor = bg;
                Console.ReadLine();
            }
            else
            {
                loader.AddFonts();
            }
        }

    }
}