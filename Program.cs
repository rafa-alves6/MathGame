using System.Diagnostics;

Random random = new();
List<String> history = [];
Stopwatch gameTimer = new();

StartGame();

void StartGame()
{
    gameTimer.Start();

    Console.Clear();
    Console.WriteLine("\t#####\t Welcome to the Math Game!\t#####");

    bool shouldLeave = false;
    do
    {
        ShowOptions();
        Console.Write("Choose an option: ");
        string? option = Console.ReadLine();
        Console.WriteLine("");

        switch (option)
        {
            case "0":
                Console.WriteLine("Leaving...");
                shouldLeave = true;
                gameTimer.Stop();
                TimeSpan timePlayed = gameTimer.Elapsed;
                Console.WriteLine($"You played for {timePlayed.Minutes} minutes and {timePlayed.Seconds} seconds");
                break;
            case "1":
                AskQuestion("add");
                break;
            case "2":
                AskQuestion("sub");
                break;
            case "3":
                AskQuestion("mult");
                break;
            case "4":
                AskQuestion("div");
                break;
            case "5":
                ShowHistory(history);
                break;
            case "6":
                RandomChoice();
                break;
            default:
                Console.WriteLine("Pick a number from 0-6");
                break;
        }
    } while (!shouldLeave);
}

void ShowOptions()
{
    Console.WriteLine("");
    Console.WriteLine("1. Addition");
    Console.WriteLine("2. Subtraction");
    Console.WriteLine("3. Multiplication");
    Console.WriteLine("4. Division");
    Console.WriteLine("5. Show game history");
    Console.WriteLine("6. Random game");
    Console.WriteLine("0. Leave game");
    Console.WriteLine("");
}

int[] GenerateTwoRandomIntegers()
{
    int x = random.Next(0, 101), y = random.Next(0, 101);
    return [x, y];
}

int[] GenerateValidDivision()
{
    int x, y;
    do
    {
        x = random.Next(0, 101);
        y = random.Next(1, 101);
    } while (!(x % y == 0)); // Generate x and y until they are divisible (so it always generates an integer)
    return [x, y];
}

int[] CalculateResult(string operation)
{
    int[] question = new int[3]; // question[0] is the first operand, question[1] is the second operand and question [2] is the result 
    int result = 0;

    int[] xAndY = GenerateTwoRandomIntegers();
    int x = xAndY[0];
    int y = xAndY[1];

    switch (operation)
    {
        case "add":
            result = x + y;
            question[0] = x;
            question[1] = y;
            question[2] = result;
            break;
        case "sub":
            result = x - y;
            question[0] = x;
            question[1] = y;
            question[2] = result;
            break;
        case "mult":
            result = x * y;
            question[0] = x;
            question[1] = y;
            question[2] = result;
            break;
        case "div":
            int[] valid = GenerateValidDivision();
            int validX = valid[0],
                validY = valid[1];

            int validResult = validX / validY;

            question[0] = validX;
            question[1] = validY;
            question[2] = validResult;
            break;
    }
    return question;
}

int GetAnswer()
{
    while (true)
    {
        string? answer = Console.ReadLine();

        if (int.TryParse(answer, out int number))
        {
            return number;
        }

        Console.WriteLine("Invalid answer. Please enter a valid integer.");
    }
}

bool IsCorrectAnswer(int answer, int result)
{
    return answer == result;
}

const int numberOperations = 3; // Number of operations the user will be prompted to answer
void AskQuestion(string operation)
{
    string operationCharacter = "";

    for (int i = 0; i < numberOperations; i++)
    {
        int[] question = CalculateResult(operation);
        int x = question[0],
            y = question[1],
       result = question[2];

        switch (operation)
        {
            case "add":
                operationCharacter = "+";
                break;
            case "sub":
                operationCharacter = "-";
                break;
            case "mult":
                operationCharacter = "*";
                break;
            case "div":
                operationCharacter = "/";
                break;
        }

        string playerQuestion = $"{x} {operationCharacter} {y} =";
        Console.Write($"{playerQuestion} ");

        int playerAnswer = GetAnswer();
        bool isCorrect = IsCorrectAnswer(playerAnswer, result);

        if (isCorrect)
        {
            AddToHistory(playerQuestion, result, isCorrect);
        }
        else
        {
            AddToHistory(playerQuestion, result, isCorrect, playerAnswer);
        }
    }
}

void ShowHistory(List<String> hist)
{
    if (hist.Count == 0)
    {
        Console.WriteLine("There are no recorded games.");
    }
    else
    {
        for (int i = 0; i < hist.Count; i++)
        {
            if (i != 0 && i % numberOperations == 0) // Improve readability of result history
            {
                Console.WriteLine("");
            }

            Console.WriteLine($"{hist[i]}");

        }
    }
}

void AddToHistory(string playerQuestion, int result, bool playerWon, int playerAnswer = 0)
{
    string defaultHistory = $"{history.Count + 1}. {playerQuestion} {result}";

    if (playerWon)
    {
        defaultHistory += ". Result: Player got it right";
    }
    else
    {
        defaultHistory += $". Result: Player got it wrong. Player answer was {playerAnswer}";
    }
    history.Add(defaultHistory);
}

void RandomChoice()
{
    string[] operations = ["add", "sub", "mult", "div"];
    int index = random.Next(0, operations.Length);
    AskQuestion(operations[index]);
}