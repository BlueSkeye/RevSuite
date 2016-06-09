using System;
using System.IO;

namespace RSConsole
{
    internal delegate string AutoCompleterDelegate(string prefix);

    internal static class Helpers
    {
        internal static string AnswerQuestionOrEscape(AutoCompleterDelegate autoCompleter =null)
        {
            string answer = "";
            while (true) {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.KeyChar) {
                    case (char)0x0D:
                        if(string.Empty == answer) { continue; }
                        return answer;
                    case (char)0x08:
                        int answerLength = answer.Length;
                        if (0 < answerLength) {
                            Console.CursorLeft -= 1;
                            Console.Write(" ");
                            Console.CursorLeft -= 1;
                            answer = (1 == answerLength) ? "" : answer.Substring(1);
                        }
                        continue;
                    case (char)0x09:
                        if (null == autoCompleter) { goto default; }
                        string autoCompleted = autoCompleter(answer);
                        if (autoCompleted.Length > answer.Length) {
                            string suffix = autoCompleted.Substring(answer.Length);
                            Console.Write(suffix);
                            answer += suffix;
                        }
                        continue;
                    default:
                        Console.Write(key.KeyChar);
                        answer += key.KeyChar;
                        continue;
                }
            }
        }

        internal class FileNameAutoCompleter
        {
            internal FileNameAutoCompleter(DirectoryInfo baseDirectory)
            {
                if (null == baseDirectory) { throw new ArgumentNullException(); }
                throw new NotImplementedException();
                _directoryInfo = baseDirectory;
                return;
            }

            // TODO
            internal string AutoComplete(string prefix)
            {
                if (!_directoryInfo.Exists) { return prefix; }
                FileInfo searchPath = new FileInfo(
                    Path.Combine((prefix.StartsWith("/")
                        ? _directoryInfo.Root.Name
                        : _directoryInfo.FullName), prefix));
                string fullSearchPathName = searchPath.FullName;
                DirectoryInfo searchDirectory = fullSearchPathName.EndsWith("/")
                    ? new DirectoryInfo(fullSearchPathName)
                    : searchPath.Directory;
                throw new NotImplementedException();
            }

            private DirectoryInfo _directoryInfo;
        }
    }
}
