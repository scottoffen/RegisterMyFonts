using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RegisterMyFonts
{
    public class FontLoaderOptions
    {
        public string Directory { get; set; }

        public List<string> Extensions { get; set; }

        public bool Verbose { get; set; }

        public bool Version { get; set; }

        public bool HasOptions { get; set; }

        private readonly List<Exception> _exceptions = new List<Exception>();

        public FontLoaderOptions(string[] args)
        {
            HasOptions = args.Any();
            ParseArguments(args);
            if (_exceptions.Count > 0)
            {
                throw new AggregateException(_exceptions);
            }
        }

        private void ParseArguments(IEnumerable<string> args)
        {
            var currentArg = ArgumentType.Undefined;

            foreach (var arg in args)
            {
                if (arg.StartsWith("-"))
                {
                    currentArg = ArgumentType.Undefined;

                    if (arg.EqualsIgnoreCase("-d", "--dir", "--directory"))
                    {
                        currentArg = ArgumentType.Directory;
                    }
                    else if (arg.EqualsIgnoreCase("-x", "--ext", "--extensions"))
                    {
                        currentArg = ArgumentType.Extension;
                    }
                    else if (arg.EqualsIgnoreCase("-v", "--version"))
                    {
                        Version = true;
                    }
                    else if (arg.EqualsIgnoreCase("-c", "--verbose"))
                    {
                        Verbose = true;
                    }
                    else
                    {
                        _exceptions.Add(new ArgumentException($"Argument {arg} is not a valid flag."));
                    }
                }
                else if (currentArg != ArgumentType.Undefined)
                {
                    switch (currentArg)
                    {
                        case ArgumentType.Directory:
                            Directory = ParseDirectory(arg);
                            break;
                        case ArgumentType.Extension:
                            Extensions = ParseExtensions(arg);
                            break;
                    }

                    currentArg = ArgumentType.Undefined;
                }
                else if (arg.EqualsIgnoreCase("verbose"))
                {
                    Verbose = true;
                }
                else
                {
                    _exceptions.Add(new ArgumentException($"Argument {arg} is not understood."));
                }
            }
        }

        private string ParseDirectory(string directory)
        {
            try
            {
                var path = Path.GetFullPath(directory);
                if (System.IO.Directory.Exists(path)) return path;
                throw new DirectoryNotFoundException($"Unable to locate directory {directory}");
            }
            catch (Exception e)
            {
                _exceptions.Add(e);
                return string.Empty;
            }
        }

        private List<string> ParseExtensions(string extensionsArg)
        {
            try
            {
                var extensions = extensionsArg.Split(',').ToList();
                return extensions.ConvertAll(FormatExtension);
            }
            catch (Exception e)
            {
                _exceptions.Add(e);
                return new List<string>();
            }
        }

        private static string FormatExtension(string ext)
        {
            return ext.StartsWith(".") ? ext.Trim() : $".{ext.Trim()}";
        }

        private enum ArgumentType
        {
            Undefined,
            Directory,
            Extension
        }
    }
}