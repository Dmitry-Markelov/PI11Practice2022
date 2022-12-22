class Maze
{
    //данные
    int playerX = 7; //начальная координата по x
    int playerY = 7; //начальная координата по y
    double visibleDistance = 20; //прорисовка
    bool GreenKey = false; //зелёный ключ в кармане
    bool BlueKey = false; //синий ключ в кармане
    public bool Playing = true;
    int s = 0; // счётчик золота
    public string message = "";

    int width;
    int height;
    int[,] maze = new int[,]
    {
        //0  1  2  3  4  5  6  7  8  9  10 11 12 13 14
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // 0
        { 1, 2, 1, 6, 0, 0, 1, 0, 1, 0, 0, 2, 1, 0, 3}, // 1
        { 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1}, // 2
        { 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1}, // 3
        { 1, 1, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1}, // 4
        { 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1}, // 5
        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1}, // 6
        { 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1}, // 7
        { 1, 0, 1, 2, 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 1}, // 8
        { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1}, // 9
        { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1}, // 10
        { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 4, 1}, // 11
        { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1}, // 12
        { 1, 2, 1, 1, 1, 0, 1, 0, 1, 0, 5, 2, 0, 0, 1}, // 13
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}  // 14
    };
    ConsoleColor ink;
    ConsoleColor paper;

    public Maze(ConsoleColor ink, ConsoleColor paper, int width = 15, int height = 15)
    {
        this.height = height;
        this.width = width;
        this.ink = ink;
        this.paper = paper;
    }

    //методы
    public void Move(int dx, int dy)
    {
        ClearMessage();

        if (playerX == 14 && playerY == 1)
        {
            Playing = false;
        }
        else
        {
            int nx = playerX + dx;
            int ny = playerY + dy;
            if (maze[ny, nx] % 2 == 0)
            {
                playerX = nx;
                playerY = ny;

                switch (maze[ny, nx])
                {
                    case 2:
                        PickUpGold(ny, nx);
                        break;
                    case 4:
                        PickUpGreenKey(ny, nx);
                        break;
                    case 6:
                        PickUpBlueKey(ny, nx);
                        break;
                }
            } 
            else
            {
                switch (maze[ny, nx])
                {
                    case 5:
                        OpenRoomDoor(ny,nx);
                        break;
                    case 3:
                        OpenMainDoor(ny, nx);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void Print(int shiftX, int shiftY)
    {
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                double relativeDistance = Math.Sqrt(
                    (playerX - x) * (playerX - x) + (playerY - y) * (playerY - y)
                );
                if (relativeDistance > visibleDistance)
                {
                    Print(shiftX + x, shiftY + y, " ", paper: ConsoleColor.Black);
                }
                else
                {
                    switch (maze[y, x])
                    {
                        case 0: //empty space
                            Print(shiftX + x, shiftY + y, " ");
                            break;
                        case 1: //wall
                            Print(shiftX + x, shiftY + y, " ", ink, paper);
                            break;
                        case 2: //gold
                            Print(shiftX + x, shiftY + y, "g", ink: ConsoleColor.Yellow);
                            break;
                        case 3: //exit
                            Print(shiftX + x, shiftY + y, "#", ink, ConsoleColor.Green);
                            break;
                        case 4: //greenKey
                            Print(shiftX + x, shiftY + y, "k", ink: ConsoleColor.Green);
                            break;
                        case 5: //room
                            Print(shiftX + x, shiftY + y, "#", ink, ConsoleColor.Blue);
                            break;
                        case 6: //blueKey
                            Print(shiftX + x, shiftY + y, "k", ink: ConsoleColor.Blue);
                            break;
                    }
                }
            }

        Print(shiftX + playerX, shiftY + playerY, "&");
    }

    public void Print(
        int x,
        int y,
        string s,
        ConsoleColor ink = ConsoleColor.White,
        ConsoleColor paper = ConsoleColor.Black
    )
    {
        Console.ForegroundColor = ink;
        Console.BackgroundColor = paper;
        Console.CursorLeft = x;
        Console.CursorTop = y;
        Console.Write(s);
    }

    void PickUpGold(int ny, int nx)
    {
        maze[ny, nx] = 0;
        message = "Вы взяли слиток золота";
    }

    void PickUpGreenKey(int ny, int nx)
    {
        maze[ny, nx] = 0;
        GreenKey = true;
        message = "Вы подобрали зеленый ключ";
    }

    void PickUpBlueKey(int ny, int nx)
    {
        maze[ny, nx] = 0;
        BlueKey = true;
        message = "Вы подобрали синий ключ";
    }

    void OpenRoomDoor(int ny, int nx)
    {
        if (BlueKey)
        {
            maze[ny, nx] = 0;
            message = "Дверь в комнату открыта";
        }
        else
        {
            message = "Дверь в комнату закрыта";
        }
    }

    void OpenMainDoor(int ny, int nx)
    {
        if (GreenKey && BlueKey && GreenKey)
        {
            maze[ny, nx] = 0;
            message = "Главная дверь открыта";
        }
        else
        {
            message = "Главная дверь закрыта";
        }
    }

    void ClearMessage()
    {
        message = "";
    }

    public void ClearLine(int index)
    {
        var x = Console.CursorLeft;
        var y = Console.CursorTop;
        Console.SetCursorPosition(0, index);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(x, y);
    }
}
