using static System.Console;
using static Tools;

class RoomStory
{
    //id локаций
    const int DOORLOC = 1;
    const int BOOKSHELFLOC = 2;
    const int SAFELOC = 3;
    const int MATHLOC = 4;

    //данные
    int safe_code = Random.Shared.Next(10, 99); //код от сейфа
    int codelock_pincode = Random.Shared.Next(100, 9999); //код от кодлока
    bool door_lock = true; //замок
    bool door_latch = true; //затвор двери
    bool door_codelock = true; //кодлок
    bool door_unlocked = false; //Состояние двери
    bool save_unlocked = false; //Состояние сейфа
    bool key_get = false; //ключ в кармане
    bool book_moved = false; //книга лежит на полу
    int math_rating = 0; //оценка по матану
    bool math_mystery = false; //зачёт по мат анализу
    Story story = null;

    public Story Create()
    {
         story = new StoryBuilder("Вы очнулись в комнате с дверью", "Поздравляем, вы выбрались!", DOORLOC)

            .AddLocation(DOORLOC, GetDoorDescription)
            .AddLocation(BOOKSHELFLOC, "Вы зашли в комнату и увидели большой книжный шкаф с серыми книгами \nПриглядевшись, вы заметили одну единственную зелёную книгу")
            .AddLocation(SAFELOC, GetSafeDescription)
            .AddLocation(MATHLOC, GetMathDescription)

            .AddOption(DOORLOC, SAFELOC, "перейти к сейфу")
            .AddOption(DOORLOC, BOOKSHELFLOC, "зайти в комнату с книжным шкафом")
            .AddOption(DOORLOC, MATHLOC, "зайти в загадочную комнату")
            .AddOption(DOORLOC, "ввести код", DoEnterCodeDoor, () => door_codelock)
            .AddOption(DOORLOC, "открыть замок", DoUnlockDoor, () => door_lock)
            .AddOption(DOORLOC, "уйти", DoLeave, () => !door_lock && !door_latch && !door_codelock)

            .AddOption(SAFELOC, DOORLOC,  "выйти к двери")
            .AddOption(SAFELOC, BOOKSHELFLOC, "зайти в комнату с книжным шкафом")
            .AddOption(SAFELOC, MATHLOC, "зайти в загадочную комнату")
            .AddOption(SAFELOC, "ввести код", DoEnterCode, () => save_unlocked == false)
            .AddOption(SAFELOC, "взять ключ", DoGetKey, () => save_unlocked == true && !key_get)

            .AddOption(BOOKSHELFLOC, DOORLOC, "выйти к двери")
            .AddOption(BOOKSHELFLOC, SAFELOC, "зайти в комнату с сейфом")
            .AddOption(BOOKSHELFLOC, MATHLOC, "зайти в загадочную комнату")
            .AddOption(BOOKSHELFLOC, "Потянуть за эту книгу", DoMoveBook, () => book_moved == false)

            .AddOption(MATHLOC, DOORLOC, "выйти к двери")
            .AddOption(MATHLOC, SAFELOC, "зайти в комнату с сейфом")
            .AddOption(MATHLOC, BOOKSHELFLOC, "зайти в комнату с книжным шкафом")
            .AddOption(MATHLOC, "Сесть за парту", DoQuiz, () => math_mystery == false)
            
            .Build();

        return story;
    }

    string GetDoorDescription()
    {
        string desc = "Напротив вас большая, толстая, железная, неприступная, короче нормальная такая дверь";
        if (door_lock)
            desc += "\nНа двери висит замок";
        if (door_latch)
            desc += "\nДверь закрыта электрическим затвором";
        if (door_codelock)
            desc += "\nРядом с дверью висит четырёхзначный кодлок";
        return desc;
    }

    string GetSafeDescription()
    {
        string desc = "Вы зашли в комнату c сейфом \nНа сейфе был установлен двузначный код, который можно было подобрать вручную, ну или найти где-то код";
        if (save_unlocked)
        {
            desc = "Сейф открыт";
            if (!key_get)
                desc += $"\nВ сейфе лежит ключ с запиской, на которой написан код от двери {codelock_pincode}";
            else
                desc += " и пуст.";
        }
        return desc;
    }

    string GetMathDescription()
    {
        string desc = "Вы зашли в загадочную комнату \nВозможно здесь вы и узнаете код от сейфа! \nВ комнате за партой сидел загадочный человек в чёрной мантии \nЧеловек сказал вам: \n-Садись!";
        if (math_rating == 2)
        {
            desc = "Экзамен провален!";
        } else if(math_rating == 5) {
            desc = "Экзамен уже сдан!";
        }
        return desc;
    }

    void DoQuiz()
    {
        Alert("После того, как вы сели, человек в чёрном протянул вам билет \nТут вы поняли, что попали на экзамен по мат анализу \nСо страхом в глазах вы посмотрели на билет, где было одно единственное задание: \n2 + 2 = ? \nСпустя долгие раздумия у вас вышло три варианта ответа \n1) 5 \n2) 4 \n3) Я наверное на пересдачу приду");
        var choose = GetInt("Выберите вариант ответа: ", 1, 3);
        if (choose == 1){
            Alert("Вытирая пот с лица вы решили дать ответ на задачу:");
            Alert("5!");
            Alert("После вашего ответа человек в мантии разозлился и выгнал вас из аудитории");
            Alert("Придется подбирать код вручную!");
            math_rating = 2;
        }
        if(choose == 2){
            Alert("Вытирая пот с лица вы решили дать ответ на задачу:");
            Alert("4!");
            Alert($"После ответа человек в мантии поставил зачет и дал вам записку с кодом от сейфа: {safe_code}");
            math_rating = 5;
            math_mystery = true;
        }
        if(choose == 3){
            Alert("Вы встали и ушли из комнаты");
            Alert("Придется подбирать код вручную!");
            math_rating = 2;
        }
    }

    void DoUnlockDoor()
    {
        if (key_get)
        {
            door_lock = false;
            Alert("Вы достаете ключ из кармана и скрестив пальцы открываете замок");
        }
        else
        {
            Alert("У вас нет ключа!");
        }       
    }

    void DoLeave ()
    {
        Alert("1) Культурно открыть дверь и выйти из этого странного места");
        Alert("2) Выбить дверь с ноги");
        var choose = GetInt("Введите действие: ", 1, 2);
        if (choose == 2)
            Alert("Нанеся критический урон ногой по двери она вылетела как пробка и вы вышли из заточения!");
        else
        {
            Alert("Поздравляю! Вы вышли из заточения!");
        }
        story.End = true;
    }

    void DoMoveBook()
    {
        Alert("После того, как вы потянули за книгу, из комнаты с дверью донесся щелчок, напоминающий открытие затвора двери");
        door_latch = false;
        book_moved = true;
    }

    // void DoRepairPicture ()
    // {
    //     Alert("Вы поправляете картину, но внезапно она срывается, а за ней...");
    //     Alert("Обнаруживается скрытый сейф!");
    //     picture_state = PICTURE_SAFE_LOCKED;
    // }

    void DoEnterCode ()
    {
        var choose = GetInt("Введите код (2 цифры): ", 10, 99);
        if (choose != safe_code)
            Alert("ERROR");
        else
        {
            Alert("После ввода комбинации сейф открылся");
            save_unlocked = true;
        }
    }

    void DoEnterCodeDoor ()
    {
        var choose = GetInt("Введите код (4 цифры): ", 100, 9999);
        if (choose != codelock_pincode)
            Alert("ERROR");
        else
        {
            Alert("После ввода комбинации вы услышали как один из замков в двери разблокировался");
            door_codelock = false;
        }
    }

    void DoGetKey ()
    {
        Alert("Вы взяли ключ");
        key_get = true;
    }

}
