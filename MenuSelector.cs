using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


/// <summary>
/// 
/// </summary>
namespace Amotels
{
    /// <summary>
    /// This class is used for creating slick ConsoleApp menus using the System.Console technology
    /// </summary>
    class MenuSelector
    {

        private const ConsoleColor foreground = ConsoleColor.White;
        private const ConsoleColor background = ConsoleColor.Black;

        private string[] options;
        private int currentIndex = 0;
        private int x;
        private int y;

        /// <summary>
        /// Constructor for Menu Selector
        /// </summary>
        /// <param name="x">x value of the display</param>
        /// <param name="y">y value of the display (y = 0 is top of screen)</param>
        /// <param name="options">all string options</param>
        public MenuSelector(int x, int y, params string[] options)
        {
            this.x = x;
            this.y = y;
            this.options = options;
            currentIndex = 0;
        }
        
        /// <summary>
        /// Displays the options to the user and lets him select a value
        /// </summary>
        /// <returns>index of selected option (starting at 0)</returns>
        public int Select()
        {
            Display();
            while(true)
            {
                
                switch(Console.ReadKey().Key)
                {

                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        Update((currentIndex - 1 + options.Length) % options.Length);
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        Update((currentIndex + 1 + options.Length) % options.Length);
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        return currentIndex;
                        break;
                }
                Update(currentIndex);
            }
        }

        /// <summary>
        /// Updates the selected index and Displays the updated selector
        /// </summary>
        /// <param name="index">index to select</param>
        private void Update(int index)
        {
            this.currentIndex = index;
            Display();
        }

        /// <summary>
        /// Prints the options to the console in a nice way
        /// </summary>
        private void Display()
        {
            for(int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(x - (options[i].Length - 2) / 2, y + i * 2);

                if (currentIndex == i)
                {
                    Console.ForegroundColor = background;
                    Console.BackgroundColor = foreground;

                    Console.WriteLine("> " + options[i] + "  ");
                }
                else
                {
                    Console.WriteLine("  " + options[i] + "  ");
                }
                Console.ResetColor();
            }
        }
    }
}
