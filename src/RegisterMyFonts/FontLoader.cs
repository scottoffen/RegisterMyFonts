using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace RegisterMyFonts
{
    public class FontLoader
    {
        private readonly string _fontDirectory;
        private readonly List<string> _fontExtensions;

        private static readonly string DefaultFontDirectory = Directory.GetCurrentDirectory();
        private static readonly List<string> DefaultFontExtensions = new List<string> { ".ttf", ".otf" };

        [DllImport("gdi32.dll")]
        private static extern bool RemoveFontResource(string lpFileName);

        [DllImport("gdi32.dll")]
        private static extern int AddFontResource(string lpFilename);

        public FontLoader(FontLoaderOptions options) : this(
            string.IsNullOrEmpty(options.Directory)
                ? DefaultFontDirectory
                : options.Directory,

            options.Extensions != null && options.Extensions.Any()
                ? options.Extensions
                : DefaultFontExtensions
        )
        {
        }

        public FontLoader() : this(DefaultFontDirectory, DefaultFontExtensions)
        {
        }

        public FontLoader(string fontDirectory) : this(fontDirectory, DefaultFontExtensions)
        {
        }

        public FontLoader(List<string> fontExtensions) : this(DefaultFontDirectory, fontExtensions)
        {
        }

        public FontLoader(string fontDirectory, List<string> fontExtensions)
        {
            _fontDirectory = fontDirectory;
            _fontExtensions = fontExtensions;
        }

        public string[] GetFontList()
        {
            return GetFontList(_fontDirectory, _fontExtensions);
        }

        public string[] GetFontList(string fontDirectory)
        {
            return GetFontList(fontDirectory, _fontExtensions);
        }

        public string[] GetFontList(List<string> fontExtensions)
        {
            return GetFontList(_fontDirectory, fontExtensions);
        }

        public string[] GetFontList(string fontDirectory, List<string> fontExtensions)
        {
            if (!Directory.Exists(fontDirectory)) return new string[0];

            var files = Directory.GetFiles(fontDirectory, "*.*", SearchOption.AllDirectories)
                .Where(_ => fontExtensions.Contains(Path.GetExtension(_)?.ToLower()))
                .ToArray();

            return files;
        }

        public int AddFonts()
        {
            return AddFonts(GetFontList());
        }

        public int AddFonts(string fontDirectory)
        {
            return AddFonts(GetFontList(fontDirectory));
        }

        public int AddFonts(List<string> fontExtensions)
        {
            return AddFonts(GetFontList(fontExtensions));
        }

        public int AddFonts(string fontDirectory, List<string> fontExtensions)
        {
            return AddFonts(GetFontList(fontDirectory, fontExtensions));
        }

        public int AddFonts(string[] fontPaths)
        {
            return (from fontPath in fontPaths
                    let font = new FileInfo(fontPath)
                    where font.Exists
                    select AddFont(fontPath)).Sum();
        }

        public int AddFont(string fontPath)
        {
            return AddFontResource(fontPath);
        }
    }
}