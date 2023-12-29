using System;

namespace bdd.workshop.calculator.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter operation: separate number and operator by blank space, example: 3 + 4");
            var command = Console.ReadLine();
            Operator.CommandManager(command, out int a, out int b, out string operation);
            switch (operation)
            {
                case ("+"):
                    Console.WriteLine(Operator.Add(a, b));
                    break;
                case ("-"):
                    Console.WriteLine(Operator.Substract(a, b));
                    break;
                case ("x"):
                    Console.WriteLine(Operator.Multiply(a, b));
                    break;
                case ("/"):
                    Console.WriteLine(Operator.Divide(a, b));
                    break;
                default:
                    Console.WriteLine("Possible operators: +,-,x,/: i.e.: 3 + 4");
                    break;
            }
        }
    }
}
