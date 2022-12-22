using static System.Console;
//данные
Random rnd = new Random();
bool agressive_exit = false;

//локации
const int MAINLOC = 1; //главная комната с дверью
const int DOORLOC = 2; //рядом с дверью
const int BOOKSHELFLOC = 3; // комната с книжным шкафом
const int SAFELOC = 4; // комната с сейфом
const int MATHLOC = 5; // комната с мат анализом
int loc = MAINLOC; //начальная локация

//предметы
int safe_code = rnd.Next(10, 99); //код от сейфа
int codelock_pincode = rnd.Next(100, 9999); //код от кодлока
bool door_lock = true; //замок
bool door_latch = true; //щеколда
bool door_codelock = true; //кодлок
bool door_unlocked = false; //Состояние двери
bool save_unlocked = false; //Состояние сейфа
bool key_get = false; //ключ в кармане
bool book_moved = false; //книга лежит на полу
// string[] inv = new string[]; //инвентарь

//начало игры
Console.Clear();
WriteLine("Вы очнулись в комнате с дверью");
WriteLine("Подумав пару секунд, вы вспомнили как уснули на мат анализе");
WriteLine("Пробежав глазами по комнате вы замечаете три двери, ведущие в другие помещения");
WriteLine("Придется отсюда выбираться!");
WriteLine(safe_code);
WriteLine(codelock_pincode);
// WriteLine(inv);

//основной код//
while(true)
{
    if (loc == 1)
    {
        //о локации
        WriteLine("Вы стоите посередине основной комнаты с дверью");
        WriteLine("1) Зайти в комнату с книжным шкафом");
        WriteLine("2) Зайти в комнату с сейфом");
        WriteLine("3) Зайти в комнату с мат анализом");
        WriteLine("4) Подойти к двери");

        //выбор действия
        int choose = GetInt("Ваши действия:", 1, 4);
        if (choose == 1) //в комнату с книжным шкафом
            loc = BOOKSHELFLOC;
        else if (choose == 2) //в комнату с сейфом
            loc = BOOKSHELFLOC;
        else if (choose == 3) //в комнату с мат анализом
            loc = MATHLOC;
        else if (choose == 4) //подойти к двери
            loc = DOORLOC;
    }

    else if (loc == 2)
    {
        //о локации
        WriteLine("Напротив вас большая, толстая, железная, неприступная, короче нормальная такая дверь");
        if (door_lock)
            WriteLine("На двери весит замок");
        if (door_latch)
            WriteLine("Дверь закрыта электрической щеколдой");
        if (door_codelock)
            WriteLine("Рядом с дверью висит четырёхзначный кодлок");
        
        if (!door_lock && !door_latch && !door_codelock) {
            WriteLine("Вы полностью разблокировали дверь!");
            WriteLine("1) Культурно открыть дверь и выйти из этого странного места");
            WriteLine("2) Выбить дверь с ноги");
            WriteLine("3) Погулять еще");
            int choose = GetInt("Ваши действия:", 1, 3);
            if(choose == 1){
                break;
            } else if(choose == 2){
                agressive_exit = true;
                break;
            } else if(choose == 3){
                loc = MAINLOC;
            }
        } else {
            WriteLine("1) Отойти от двери");
            WriteLine("2) Ввести пин-код");
            if(door_lock && key_get){
                WriteLine("3) Открыть замок");
            }
            int choose = GetInt("Ваши действия:", 1, 3);
            if(choose == 1){
                loc = MAINLOC;
            }
            if(choose == 2){
                int x = GetInt("Введите пин-код (4 цифры)", 1000, 9999);

                        if (x == codelock_pincode)
                        {
                            door_codelock = false;
                            Console.WriteLine("После ввода комбинации вы услышали как один из замков в двери разблокировался");
                        }
                        else {
                            Console.WriteLine("ERROR!!!");
                        }
            }
            if(choose == 3 && key_get){
                WriteLine("Вы достаете ключ из кармана и скрестив пальцы открываете замок");
                door_lock = false;
            }
            
        }
    }

    else if (loc == 3)
    {
        //...
    }

    else if (loc == 4)
    {
        //...
    }

    else if (loc== 5)
    {
        //...
    }
}   

static int GetInt(string s, int min, int max)
{
    int result = min;
    bool valid = false;
    do
    {
        Write(s);
        valid = int.TryParse(ReadLine(), out result);
    } while (!valid || result < min || result > max);
    return result;
}
