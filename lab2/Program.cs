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
bool door_latch = true; //затвор двери
bool door_codelock = true; //кодлок
bool door_unlocked = false; //Состояние двери
bool save_unlocked = false; //Состояние сейфа
bool key_get = false; //ключ в кармане
bool book_moved = false; //книга лежит на полу
int math_rating = 0; //оценка по матану
bool math_mystery = false; //зачёт по мат анализу
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
            loc = SAFELOC;
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
            WriteLine("На двери висит замок");
        if (door_latch)
            WriteLine("Дверь закрыта электрическим затвором");
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
        //о локации
        WriteLine("Вы зашли в комнату и увидели большой книжный шкаф с серыми книгами");
        WriteLine("Приглядевшись, вы заметили одну единственную зелёную книгу");
        WriteLine("1) Вернуться в главную комнату");
        if(!book_moved){
            WriteLine("2) Потянуть за эту книгу");
        }

        //выбор действия
        int choose = GetInt("Ваши действия:", 1, 2);
        if (choose == 1){ //в главную комнату
            loc = MAINLOC;
        }
        else if (choose == 2 && !book_moved){ //потянуть за книгу
            WriteLine("После того, как вы потянули за книгу, из комнаты с дверью донесся щелчок, напоминающий открытие затвора двери");
            door_latch = false;
        } 
        
    }

    else if (loc == 4)
    {
        //о локации
        WriteLine("Вы зашли в комнату c сейфом");
        WriteLine("На сейфе был установлен двузначный код, который можно было подобрать вручную, ну или найти где-то код");
        WriteLine("1) Вернуться в главную комнату");
        WriteLine("2) Ввести код");

        //выбор действия
        int choose = GetInt("Ваши действия:", 1, 2);
        if(choose == 1){
                loc = MAINLOC;
        }
        if(choose == 2 && !save_unlocked){
            int x = GetInt("Введите пин-код (2 цифры)", 10, 99);
                if (x == safe_code)
                {
                    Console.WriteLine("После ввода комбинации сейф открылся");
                    Console.WriteLine("Внутри лежал ключ с запиской");
                    Console.WriteLine($"На записке вероятно был написан код от главной двери {codelock_pincode}");
                    save_unlocked = true;
                    key_get = true;
                }
                else {
                    Console.WriteLine("ERROR!!!");
                }
        }
    }

    else if (loc== 5)
    {
        if(!math_mystery){
            //о локации
            WriteLine("Вы зашли в загадочную комнату");
            WriteLine("Возможно здесь вы и узнаете код от сейфа!");
            WriteLine("В комнате за партой сидел загадочный человек в чёрной мантии");
            WriteLine("Человек сказал вам:");
            WriteLine("-Садись!");
            WriteLine("После того, как вы сели, человек в чёрном протянул вам билет");
            WriteLine("Тут вы поняли, что попали на экзамен по мат анализу");
            WriteLine("Со страхом в глазах вы посмотрели на билет, где было одно единственное задание:");
            WriteLine("2 + 2 = ?");
            WriteLine("Спустя долгие раздумия у вас вышло три варианта ответа");
            WriteLine("1) 5");
            WriteLine("2) 4");
            WriteLine("3) Я наверное на пересдачу приду");

            //выбор действия
            int choose = GetInt("Ваши действия:", 1, 3);
            if(choose == 1){
                WriteLine("Вытирая пот с лица вы решили дать ответ на задавчу:");
                WriteLine("5!");
                WriteLine("После вашего ответа человек в мантии разозлился и выгнал вас из аудитории");
                WriteLine("Придется подбирать код вручную!");
                math_rating = 2;
            }
            if(choose == 2){
                WriteLine("Вытирая пот с лица вы решили дать ответ на задачу:");
                WriteLine("4!");
                WriteLine($"После ответа человек в мантии поставил зачет и дал вам записку с кодом от сейфа: {safe_code}");
                math_rating = 5;
                math_mystery = true;
            }
            if(choose == 3){
                WriteLine("Вы встали и ушли из комнаты");
                WriteLine("Придется подбирать код вручную!");
                math_rating = 2;
            }
            WriteLine("Вы вернулись в главную комнату");
            loc = MAINLOC;
        } else {
            if(math_rating == 2){
                WriteLine("Экзамен провален!");
            } else {
                WriteLine("Экзамен уже сдан!");
            }
            WriteLine("1) Вернуться в главную комнату");

            //выбор действия
            int choose = GetInt("Ваши действия:", 1, 1);
            if(choose == 1){
                    loc = MAINLOC;
            }
        }
    }
}   

if(agressive_exit){
    WriteLine("Нанеся критический урон ногой по двери она вылетела как пробка и вы вышли из заточения!");
} else {
    WriteLine("Поздравляю! Вы вышли из заточения!");
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