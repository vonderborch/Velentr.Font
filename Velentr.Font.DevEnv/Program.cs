using System;

#if FNA
using System.IO;
using System.Runtime.InteropServices;
#endif

namespace Velentr.Font.DevEnv
{
    public static class Program
    {
#if FNA
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetDefaultDllDirectories(int directoryFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern void AddDllDirectory(string lpPathName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetDllDirectory(string lpPathName);

        const int LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000;
#endif

        [STAThread]
        static void Main()
        {
#if FNA
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                try
                {
                    SetDefaultDllDirectories(LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
                    AddDllDirectory(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        Environment.Is64BitProcess ? "x64" : "x86"
                    ));
                }
                catch
                {
                    // Pre-Windows 7, KB2533623 
                    SetDllDirectory(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        Environment.Is64BitProcess ? "x64" : "x86"
                    ));
                }
            }
#endif
            using (var game = new Game1())
                game.Run();
        }
    }
}
