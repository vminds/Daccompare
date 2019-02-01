using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daccompare
{
    class ConsoleLogger : ILogger
    {
        public void WriteErrorLine(string message)
        {
            WriteLine(message, ConsoleColor.Red);
        }

        public void WriteError(string message)
        {
            Write(message, ConsoleColor.Red);
        }

        public void WriteError(Exception ex)
        {
            WriteLine(ex.Message, ConsoleColor.Red);
        }

        public void WriteErrorLine(Exception ex)
        {
            WriteLine(ex.Message, ConsoleColor.Red);
        }

        public void WriteInfoLine(string message)
        {
            WriteLine(message, ConsoleColor.White);
        }

        public void WriteInfo(string message)
        {
            Write(message, ConsoleColor.White);
        }

        public void WriteSuccessLine(string message)
        {
            WriteLine(message, ConsoleColor.Green);
        }

        public void WriteSuccess(string message)
        {
            Write(message, ConsoleColor.Green);
        }

        public void WriteWarningLine(string message)
        {
            WriteLine(message, ConsoleColor.Cyan);
        }

        public void WriteWarning(string message)
        {
            Write(message, ConsoleColor.DarkMagenta);
        }

        public void ClearLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public void WriteEmptyLine()
        {
            Console.WriteLine();
        }

        public void WriteSeparator()
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        }

        private void WriteLine(string message, ConsoleColor color)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{DateTime.Now.ToString()} { message }");
            }
            finally
            {
                Console.ResetColor();
            }
        }

        private void Write(string message, ConsoleColor color)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.Write(message);
            }
            finally
            {
                Console.ResetColor();
            }
        }
    }
}
