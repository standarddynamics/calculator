using System;

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
            default:
                Console.WriteLine("Invalid input.");
                return;
        }
    }

    static void QuestionOne()
    {
        Console.WriteLine("Enter an expression (e.g., 2 + 2):");
        string? input = Console.ReadLine();

        if (input is null)
        {
            Console.WriteLine("Input cannot be null");
            return;
        }
        
        // Split the input string into parts
        string[] parts = input.Split(' ');

        if (parts.Length != 3)
        {
            Console.WriteLine("Invalid input format. Please use the format operand1 operator operand2.");
            return;
        }

        try
        {
            // Parse the operands and operator
            Int32 operand1 = Int32.Parse(parts[0]);
            string operation = parts[1];
            Int32 operand2 = Int32.Parse(parts[2]);

            float result = 0;

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
                default:
                    Console.WriteLine("Invalid operator. Please use one of the following: +, -, *, /.");
                    return;
            }

            // Print the result
            Console.WriteLine($"Result: {result}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid operand format. Please enter numeric operands.");
        }
    }
}
