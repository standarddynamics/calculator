class Program
{
    static void Main()
    {
        Console.WriteLine("Enter which question to test? (1-3):");
        string? input = Console.ReadLine();

        if (input is null)
        {
            Console.Write("Input cannot be null");
            return;
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
                Console.WriteLine("Invalid input.");
                return;
        }
    }

    static void QuestionOne()
    {
        Console.WriteLine("Enter an expression (eg. 2 + 2):");
        string? input = Console.ReadLine();

        if (input is null)
        {
            Console.WriteLine("Input cannot be null");
            return;
        }
        
        // Split the input string into parts
        string[] parts = ConvertStringToArray(input.Trim());

        if (parts.Length != 3)
        {
            Console.WriteLine("Invalid input format. Please use the format operand1 operator operand2.");
            return;
        }

        // Parse the operands and operator
        double operand1 = double.Parse(parts[0]);
        string operation = parts[1];
        double operand2 = double.Parse(parts[2]);

        double result = 0;

        // Perform the calculation based on the operator
        switch (operation)
        {
            case "+":
                result = operand1 + operand2;
                break;
            case "-":
                result = operand1 - operand2;
                break;
            case "*":
                result = operand1 * operand2;
                break;
            case "/":
                if (operand2 != 0)
                {
                    result = operand1 / operand2;
                }
                else
                {
                    Console.WriteLine("Error: Division by zero.");
                    return;
                }
                break;
            case "^":
                result = Math.Pow(operand1, operand2);
                break;
            default:
                Console.WriteLine("Invalid operator. Please use one of the following: +, -, *, /, ^.");
                return;
        }

        // Print the result
        Console.WriteLine($"Result: {result}");
    }

    static void QuestionTwo()
    {
        Console.WriteLine("Enter an expression (eg. 2 + (2 / 4):");
        string? input = Console.ReadLine();

        if (input is null)
        {
            Console.WriteLine("Input cannot be null");
            return;
        }
        
        // Split the input string into parts
        string[] parts = ConvertStringToArray(input.Trim());
    }

    static string[] ConvertStringToArray(string str)
    {
        List<string> conversion = [];

        foreach (char value in str)
        {
            if (value.ToString() != " ")
            {
                conversion.Add(value.ToString());
            }
        }

        return [.. conversion];
    }
}