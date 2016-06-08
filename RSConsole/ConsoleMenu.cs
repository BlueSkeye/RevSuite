using System;
using System.Collections.Generic;
using System.Text;

namespace RSConsole
{
    internal class ConsoleMenu
    {
        static ConsoleMenu()
        {
            MainMenu = new ConsoleMenu()
                .AddItem(new MenuItem("Load module", Program.LoadModuleInRS));
        }

        private ConsoleMenu AddItem(MenuItem item)
        {
            if (null == item) { throw new ArgumentNullException(); }
            _items.Add(item);
            return this;
        }

        /// <summary></summary>
        /// <returns>true if menu is complete and should be popped.</returns>
        internal bool Display(out ConsoleMenu newMenu)
        {
            int menuIndex;
            while (true) {
                Console.Clear();
                for (menuIndex = 0; _items.Count > menuIndex; menuIndex++) {
                    MenuItem displayedItem = _items[menuIndex];
                    Console.WriteLine("{0,-2} : {1}", menuIndex + 1, displayedItem.Title);
                }
                Console.WriteLine("Q  : quit");
                Console.WriteLine();
                Console.Write("Choice : ");
                string answer = string.Empty;
                while (true) {
                    ConsoleKeyInfo nextKey = Console.ReadKey(true);
                    int nextCharacter = nextKey.KeyChar;

                    if (0 != (nextKey.Modifiers & ConsoleModifiers.Alt & ConsoleModifiers.Control)) {
                        continue;
                    }
                    if (8 == nextCharacter) {
                        bool erase = true;
                        switch (answer.Length) {
                            case 0:
                                erase = false;
                                break;
                            case 1:
                                answer = string.Empty;
                                break;
                            default:
                                answer = answer.Substring(0, answer.Length - 1);
                                break;
                        }
                        if (erase) {
                            Console.CursorLeft -= 1;
                            Console.Write(" ");
                            Console.CursorLeft -= 1;
                        }
                        continue;
                    }
                    if ('Q' == char.ToUpper((char)nextCharacter)) {
                        if (string.Empty == answer) {
                            answer = "Q";
                            Console.Write((char)nextCharacter);
                        }
                        continue;
                    }
                    if (char.IsDigit((char)nextCharacter)) {
                        if (2 <= answer.Length) { continue; }
                        if ("Q" == answer) { continue; }
                        answer += (char)nextCharacter;
                        Console.Write((char)nextCharacter);
                    }
                    if ((0 < answer.Length) && (0x0D == (char)nextCharacter)) {
                        Console.WriteLine();
                        break;
                    }
                    continue;
                }
                // Got an answer.
                newMenu = null;
                if ("Q" == answer) { return true; }
                int choice = int.Parse(answer) - 1;
                if ((0 > choice) || (_items.Count < choice)) {
                    continue;
                }
                MenuItem selectedItem = _items[choice];
                selectedItem.Handler(selectedItem.Tag, out newMenu);
                return false;
            }
        }

        private List<MenuItem> _items = new List<MenuItem>();
        internal static ConsoleMenu MainMenu { get; private set; }

        internal delegate void MenuItemHandler(string tag, out ConsoleMenu newMenu);

        internal class MenuItem
        {
            internal MenuItem(string title, MenuItemHandler handler, string tag = null)
            {
                if (string.IsNullOrEmpty(title)) { throw new ArgumentNullException(); }
                if (null == handler) { throw new ArgumentNullException(); }
                Title = title;
                Handler = handler;
                return;
            }

            internal MenuItemHandler Handler { get; private set; }
            internal string Tag { get; private set; }
            internal string Title { get; private set; }
        }
    }
}
