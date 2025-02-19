class Program
{
    static void Main()
    {
        Console.WriteLine("Enter which question to test? (1-2):");
        string? input = Console.ReadLine();

        try
        {
            if (input is null)
            {
                throw new Exception("Input cannot be null");
            }

            switch (Int32.Parse(input))
            {
                case 1:
                    QuestionOne();
                    break;
                case 2:
                    QuestionTwo();
                    break;
                default:
                    throw new Exception("Invalid input");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        Console.Write("Type q followed by the enter key to quit.");

        while (Console.ReadLine() != "q") {}
    }

    static void QuestionOne()
    {
        Console.WriteLine("Enter an expression (eg. 2 + 2):");
        string? input = Console.ReadLine() ?? throw new Exception("Input cannot be null");

        // Split the input string into parts
        string expression = input.Trim().Replace(" ", string.Empty);

        List<string> parts = [];
        foreach (char ch in expression)
        {
            if (char.IsDigit(ch))
            {
                if (parts.Count > 0) {
                    if (char.IsDigit(Convert.ToChar(parts[parts.Count - 1])))
                    {
                        parts[parts.Count - 1] += ch;
                    }
                    else
                    {
                        parts.Add(ch.ToString());
                    }
                }
                else
                {
                    parts.Add(ch.ToString());
                }
            }
            else
            {
                parts.Add(ch.ToString());
            }
        }

        if (parts.Count != 3)
        {
            throw new Exception("Invalid input format. Please use the format operand1 operator operand2.");
        }

        // Parse the operands and operator
        double a = double.Parse(parts[0]);
        string operation = parts[1];
        double b = double.Parse(parts[2]);

        double result;

        // Perform the calculation based on the operator
        switch (operation)
        {
            case "+":
                result = a + b;
                break;
            case "-":
                result = a - b;
                break;
            case "*":
                result = a * b;
                break;
            case "/":
                if (b != 0)
                {
                    result = a / b;
                }
                else
                {
                    throw new Exception("Error: Division by zero.");
                }
                break;
            case "^":
                result = Math.Pow(a, b);
                break;
            default:
                throw new Exception("Invalid operator. Please use one of the following: +, -, *, /, ^.");
        }

        // Print the result
        Console.WriteLine($"Result: {result}");
    }

    static void QuestionTwo()
    {
        Console.WriteLine("Enter an expression (eg. 2 + (2 / 4):");
        string? input = Console.ReadLine() ?? throw new Exception("Input cannot be null");
        string expression = input.Trim().Replace(" ", string.Empty);

        // Using a queue for the output to manage order of the numbers/using FIFO
        Queue<string> outputQueue = new();
        // Using a stack to manage operator precedence
        Stack<char> operatorStack = new();
        // Declaring operators as dictionary with their precedence
        Dictionary<char, int> operators = new() 
        {
            { '+', 1 }, { '-', 1 }, { '*', 2 }, { '/', 2 }, { '^', 3 }
        };

        for (int i = 0; i < expression.Length; i++)
        {
            char ch = expression[i];

            if (char.IsDigit(ch))
            {
                // Push the numbers to the queue
                string number = ch.ToString();
                while (i + 1 < expression.Length && char.IsDigit(expression[i + 1]))
                {
                    number += expression[++i];
                }
                outputQueue.Enqueue(number);
            }
            else if (operators.TryGetValue(ch, out int value))
            {
                // Ensure operators with higher or equal precedence are processed before the current operator
                while (operatorStack.Count > 0 && operators.ContainsKey(operatorStack.Peek()) &&
                       operators[operatorStack.Peek()] >= value)
                {
                    outputQueue.Enqueue(operatorStack.Pop().ToString());
                }
                operatorStack.Push(ch);
            }
            else if (ch == '(')
            {
                operatorStack.Push(ch);
            }
            else if (ch == ')')
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                {
                    outputQueue.Enqueue(operatorStack.Pop().ToString());
                }

                if (operatorStack.Count == 0 || operatorStack.Pop() != '(')
                {
                    throw new Exception("Mismatched parentheses");
                }
            }
            else
            {
                throw new Exception($"Invalid character: {ch}");
            }
        }

        while (operatorStack.Count > 0)
        {
            char op = operatorStack.Pop();
            if (op == '(' || op == ')')
            {
                throw new Exception("Mismatched parentheses");
            }
            outputQueue.Enqueue(op.ToString());
        }

        Stack<double> evaluationStack = new();
        
        while (outputQueue.Count > 0)
        {
            string val = outputQueue.Dequeue();
            if (double.TryParse(val, out double number))
            {
                evaluationStack.Push(number);
            }
            else
            {
                double b = evaluationStack.Pop();
                double a = evaluationStack.Pop();

                switch (val)
                {
                    case "+":
                        evaluationStack.Push(a + b);
                        break;
                    case "-":
                        evaluationStack.Push(a - b);
                        break;
                    case "*":
                        evaluationStack.Push(a * b);
                        break;
                    case "/":
                        evaluationStack.Push(a / b);
                        break;
                    case "^":
                        evaluationStack.Push(Math.Pow(a, b));
                        break;
                }
            }
        }

        double result = evaluationStack.Pop();

        Console.WriteLine($"Result: {result}");
    }
}