using static System.Console;
//данные
Random rnd = new Random();

//локации
const int DOORLOC = 1; //комната с дверью
const int BOOKSHELFLOC = 2; // комната с книжным шкафом
const int SAFELOC = 3; // комната с сейфом
const int MATHLOC = 4; // косната с мат анализом
int loc = DOORLOC; //начальная локация

//предметы
int pincode = rnd.Next(10, 100);;
bool door_unlocked = false; //Состояние двери
bool key_get = false; //ключ в кармане
bool book_moved = false; //книга лежит на полу

//начало игры
Console.Clear();
WriteLine("Вы очнулись в комнате с дверью");
WriteLine("Подумав пару секунд, вы вспомнили как уснули на мат анализе");
WriteLine("Придется отсюда выбираться!");
WriteLine(pincode);

//основной код//
