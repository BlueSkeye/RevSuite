using System;
using System.Collections.Generic;
using System.IO;

using RSCoreInterface;

namespace RSConsole
{
    public class Program
    {
        private static bool Initialize()
        {
            return true;
        }

        /// <summary>Handle the 'Load module' item from main menu.</summary>
        /// <param name="tag"></param>
        /// <param name="newMenu"></param>
        internal static void LoadModuleInRS(string tag, out ConsoleMenu newMenu)
        {
            newMenu = null;
            Console.Write("Hit ENTER");
            Console.ReadLine();
            return;
        }

        public static int Main(string[] args)
        {
            if (!Initialize()) {
                return (int)ReturnCode.InitializationFailure;
            }
            if (!ParseArguments(args)) {
                return (int)ReturnCode.ArgumentParsingFailure;
            }
            _rsCore = RSCoreProvider.RSCore;
            _menus.Push(ConsoleMenu.MainMenu);
            Run();
            return (int)ReturnCode.Success;
        }

        private static bool ParseArguments(string[] args)
        {
            bool result = true;
            int argumentsCount = args.Length;
            for(int index = 0; index < argumentsCount; index++) {
                string scannedArgument = args[index];
                char initial = scannedArgument[0];
                if (('-' != initial) && ('/' != initial)) {
                    if (argumentsCount != (index + 1)) {
                        Console.WriteLine(Messages.AnalyzedFileNameMustBeLastArgument);
                        result = false;
                        continue;
                    }
                    _analyzedFile = new FileInfo(scannedArgument);
                    if (!_analyzedFile.Exists) {
                        Console.WriteLine(Messages.AnalyzedFileDoesntExist, _analyzedFile.FullName);
                        result = false;
                        continue;
                    }
                    try {
                        using (FileStream unused = File.Open(_analyzedFile.FullName, FileMode.Open, FileAccess.Read)) { }
                    }
                    catch (Exception e) {
                        Console.WriteLine(Messages.UnableToOpenAnalyzedFile, _analyzedFile.FullName, e.Message);
                        result = false;
                        continue;
                    }
                    continue;
                }
                // Handle switches.
                if (1 == scannedArgument.Length) {
                    result = false;
                    Console.WriteLine(Messages.EmptySwitchArgument);
                    continue;
                }
                scannedArgument = (1 == scannedArgument.Length) ? string.Empty : scannedArgument.Substring(1);
            }
            //if (null == _analyzedFile) {
            //    Console.WriteLine(Messages.MissingAnalyzedFileNameMandatoryArgument);
            //    result = false;
            //}
            return result;
        }

        private static void Run()
        {
            while (true) {
                if (0 >= _menus.Count) { return; }
                ConsoleMenu currentMenu = _menus.Peek();
                ConsoleMenu newMenu;
                if (currentMenu.Display(out newMenu)) {
                    if (null != newMenu) { throw new AssertionException(); }
                    _menus.Pop();
                }
            }
        }

        private static FileInfo _analyzedFile;
        private static Stack<ConsoleMenu> _menus = new Stack<ConsoleMenu>();
        private static IRSCore _rsCore;
    }
}
