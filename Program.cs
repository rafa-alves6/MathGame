Random random = new();
List<String> history = [];
StartGame();

void StartGame()
{
        Console.Clear();
        Console.WriteLine("\t#####\t Welcome to the Math Game!\t#####");

        bool shouldLeave = false;
        do
        {
                ShowOptions();
                Console.Write("Choose an option: ");
                string? option = Console.ReadLine();
                Console.WriteLine("");

                bool isNumber = int.TryParse(option, out int number);

                if (isNumber)
                {
                        switch (number)
                        {
                                case 0:
                                        Console.WriteLine("Leaving...");
                                        shouldLeave = true;
                                        break;
                                case 1:
                                        askQuestion("add");
                                        break;
                                case 2:
                                        askQuestion("sub");
                                        break;
                                case 3:
                                        askQuestion("mult");
                                        break;
                                case 4:
                                        askQuestion("div");
                                        break;
                                case 5:
                                        showHistory(history);
                                        break;
                                default:
                                        Console.WriteLine("Pick a number from 0-5");
                                        break;
                        }

                }
                else
                {
                        Console.WriteLine("Option must be a number.");
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
        Console.WriteLine("0. Leave game");
        Console.WriteLine("");
}

int[] generateTwoRandomIntegers()
{
        int x = random.Next(0, 101), y = random.Next(0, 101);
        return [x, y];
}

int[] generateValidDivision()
{
        int x, y;
        do
        {
                x = random.Next(0, 101);
                y = random.Next(1, 101);
        } while (!(x % y == 0)); // Generate x and y until they are divisible (so it always generates an integer)
        return [x, y];
}

int[] calculateResult(string operation)
{
        int[] question = new int[3]; // question[0] is the first operand, question[1] is the second operand and question [2] is the result 
        int result = 0;

        int[] xAndY = generateTwoRandomIntegers();
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
                        int[] valid = generateValidDivision();
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

int getAnswer()
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

bool isCorrectAnswer(int answer, int result)
{
        return answer == result;
}


void askQuestion(string operation)
{
        string operationCharacter = "";

        int[] question = calculateResult(operation);
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

        int playerAnswer = getAnswer();
        bool isCorrect = isCorrectAnswer(playerAnswer, result);
        
        if (isCorrect)
        {
                addToHistory(playerQuestion, result, isCorrect);
        }
        else
        {
                addToHistory(playerQuestion, result, isCorrect, playerAnswer);
        }
}

void showHistory(List<String> hist)
{
        if (hist.Count < 0)
        {
                Console.WriteLine("There are no recorded games.");
        }
        else
        {
                Console.WriteLine("Game history:");
                foreach (String s in hist)
                {
                        Console.WriteLine(s);
                }
        }
}

void addToHistory(string playerQuestion, int result, bool playerWon, int playerAnswer = 0)
{
        string defaultHistory = $"{playerQuestion} {result}";

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