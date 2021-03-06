# Register My Fonts
A simple way to temporarily register fonts for use on a Windows computer that you don't have admin access on.

## Default Usage ##

The default usage is recommended. Simply place the executable in the same folder as the fonts that you want to register, and run it - either by double clicking on it in Windows Explorer or running it from the command line.

All fonts in the folder will be registered and available in applications that use the font registry, until the user is logged out or the computer is restarted.

> The executeable uses the current working directory, not the directory the executeable is located in. When you double click in Windows Explorer, these values will be the same.

## Advanced Usage ##

There are several command line parameters you can use to control application behavior.

### Specify Font Directory ###

The directory value defaults to the current working directory, and all subdirectories will also be traversed for matching fonts. If you want to specify the directory to search for fonts in, you can use any of the following:

- `-d [directory]`
- `--dir [directory]`
- `--directory [directory]`

A `DirectoryNotFoundException` exception will be thrown if the specified directory does not exist.

### Specify Font Extensions ###

By default, files with the extensions `.ttf` and `.otf` are matched as fonts and will be registered. You can specify your own list of extensions using one of the following flags:

- `-x [comma separated list of extensions]`
- `--ext [comma separated list of extensions]`
- `--extensions [comma separated list of extensions]`

A leading `.` before each extension is optional. Any leading or trailing white space will be trimmed.

### Display Version Information ###

To see the verion for the executable, use one of the version flags.

- `-v`
- `--version`

### Turn On Verbose Mode ###

If things aren't working as expected, it can be helpful to turn on verbose mode.

- `-c`
- `--verbose`

## Free To Use ##

RegisterMyFont was built specifically with educational institutions and students in mind. There is not charge for it's use. Contribution and suggestions are welcome!
