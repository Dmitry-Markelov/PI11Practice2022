using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.CursorVisible = false;

        var maze = new Maze(ink: ConsoleColor.Gray, paper: ConsoleColor.DarkGray);

        while (true)
        {
            maze.ClearLine(2);

            if (!maze.Playing)
                break;

            maze.Print(3, 3);

            var ki = Console.ReadKey(true);
            switch (ki.Key)
            {
                case ConsoleKey.LeftArrow:
                    maze.Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    maze.Move(1, 0);
                    break;
                case ConsoleKey.UpArrow:
                    maze.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    maze.Move(0, 1);
                    break;
                case ConsoleKey.Escape:
                    return;
            }

            maze.ClearLine(18);
            maze.Print(3, 18, maze.message, ink: ConsoleColor.DarkGreen);
        }
        maze.Print(3, 3);
        maze.Print(3, 18, "Поздравляю, вы прошли игру!!!", ink: ConsoleColor.Yellow);
        Console.ReadKey();
    }
}
