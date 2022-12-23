class Calculator
{
    //data
    string _screen;
    string _memory;
    string _op;
    CalcState _state;

    public string Screen { get { return _screen; } }

    //methods
    public Calculator()
    {
        _screen = "0";
        _memory = "";
        _op = "";
        _state = CalcState.Input1;
    }

    public void PressKey (string key)
    {
        switch (_state)
        {
            case CalcState.Input1:    _state = ProcessInput1(key);    break;
            case CalcState.Operation: _state = ProcessOperation(key); break;
            case CalcState.Input2:    _state = ProcessInput2(key);    break;
            case CalcState.Answer:    _state = ProcessAnswer(key);    break;
            case CalcState.Error:     _state = ProcessError(key);     break;
        }
    }

    //inside
    CalcState ProcessInput1(string key)
    {
        switch (GetKind(key))
        {
            case KeyKing.Digit:
                _screen = AddDigit(_screen, key);
                return CalcState.Input1;
            case KeyKing.Dot:
                _screen = AddDot(_screen);
                return CalcState.Input1;
            case KeyKing.ChangeSign:
                _screen = ChangeSign(_screen);
                return CalcState.Input1;
            case KeyKing.Operation:
                _memory = _screen;
                _op = key;
                return CalcState.Operation;
            case KeyKing.Equal:
                return CalcState.Input1;
            case KeyKing.Clear:
                Clear();
                return CalcState.Input1;
            default:
                _screen = "Error";
                return CalcState.Error;
        }
    }

    CalcState ProcessOperation(string key)
    {
        switch (GetKind(key))
        {
            case KeyKing.Digit:
                _screen = key;
                return CalcState.Input2;
            case KeyKing.Dot:
                _screen = "0.";
                return CalcState.Input2;
            case KeyKing.ChangeSign:
                return CalcState.Operation;
            case KeyKing.Operation:
                _op = key;
                return CalcState.Operation;
            case KeyKing.Equal:
                //...
                return CalcState.Input1;
            case KeyKing.Clear:
                Clear();
                return CalcState.Input1;
            default:
                _screen = "Error";
                return CalcState.Error;
        }
    }

    CalcState ProcessInput2(string key)
    {
        switch (GetKind(key))
        {
            case KeyKing.Digit:
                _screen = AddDigit(_screen, key);
                return CalcState.Input2;
            case KeyKing.Dot:
                _screen = AddDot(_screen);
                return CalcState.Input2;
            case KeyKing.ChangeSign:
                _screen = ChangeSign(_screen);
                return CalcState.Input2;
            case KeyKing.Operation:
                //...
                return CalcState.Input1;
            case KeyKing.Equal:
                //...
                return CalcState.Input1;
            case KeyKing.Clear:
                Clear();
                return CalcState.Input1;
            default:
                _screen = "Error";
                return CalcState.Error;
        }
    }

    CalcState ProcessAnswer(string key)
    {
        switch (GetKind(key))
        {
            case KeyKing.Digit:
                _screen = key;
                return CalcState.Input1;
            case KeyKing.Dot:
                _screen = "0.";
                return CalcState.Input1;
            case KeyKing.ChangeSign:
                _screen = ChangeSign(_screen);
                return CalcState.Answer;
            case KeyKing.Operation:
                _memory = _screen;
                _op = key;
                return CalcState.Operation;
            case KeyKing.Equal:
                //...
                return CalcState.Input1;
            case KeyKing.Clear:
                Clear();
                return CalcState.Input1;
            default:
                _screen = "Error";
                return CalcState.Error;
        }
    }

    CalcState ProcessError(string key)
    {
        switch (GetKind(key))
        {
            case KeyKing.Digit:
            case KeyKing.Dot:
            case KeyKing.ChangeSign:
            case KeyKing.Operation:
            case KeyKing.Equal:
                return CalcState.Error;
            case KeyKing.Clear:
                Clear();
                return CalcState.Input1;
            default:
                _screen = "Error";
                return CalcState.Error;
        }
    }

    string AddDigit(string num, string digit)
    {
        if (num == "0" && digit == "0")
            return "0";

        return num + digit;
    }

    string AddDot(string num)
    {
        if (num.Contains("."))
            return num;
        else
            return num + ".";
    }

    string ChangeSign (string num)
    {
        if (num == "0")
            return "0";

        if (num.StartsWith("-"))
            return num.Substring(1);
        else
            return "-" + num;
    }

    void Clear ()
    {
        _screen = "0";
        _memory = "";
        _op = "";
    }

    string Calc (string x, string y, string op)
    {
        double arg1 = double.Parse(x); //!!!
        double arg2 = double.Parse(y); //!!!
        double res = 0;

        switch(op)
        {
            case "+": res = arg1 + arg2; break;
            case "-": res = arg1 - arg2; break;
            case "*": res = arg1 * arg2; break;
            case "/": res = arg1 / arg2; break; //!!!
        } 

        return res.ToString(); //!!!
    }

    KeyKing GetKind(string key)
    {
        switch(key)
        {
            case "0":
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
                return KeyKing.Digit;
            case ".":
                return KeyKing.Dot;
            case "-/+":
                return KeyKing.ChangeSign;
            case "+":
            case "-":
            case "*":
            case "/":
                return KeyKing.Operation;
            case "=":
                return KeyKing.Equal;
            case "c":
            case "C":
                return KeyKing.Clear;
            default:
                return KeyKing.Undefined;
        }
    }

}

enum CalcState
{
    Input1,
    Operation,
    Input2,
    Answer,
    Error
}

enum KeyKing
{
    Digit,
    Dot,
    ChangeSign,
    Operation,
    Equal,
    Clear,
    Undefined
}