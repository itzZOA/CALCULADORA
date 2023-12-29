using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace bdd.workshop.calculator.web.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Operate(Models.Calculator calculator)
        {
            char operation = calculator.Input.FirstOrDefault(c => "+-x/√".Contains(c));
            var parts = calculator.Input.Split(operation);

            if (operation == '√')
            {
                if (parts.Length == 2 && double.TryParse(parts[1], out double radicand))
                {
                    ViewData["a"] = radicand;
                    ViewData["operation"] = "√";
                    ViewData["result"] = Math.Sqrt(radicand); 
                }
                else
                {
                    ViewData["result"] = "Entrada inválida para la raíz cuadrada";
                }
            }
            else if (parts.Length == 2 && int.TryParse(parts[0], out int a) && int.TryParse(parts[1], out int b))
            {
                calculator.A = a;
                calculator.B = b;
                calculator.Command = operation.ToString();

                ViewData["a"] = a;
                ViewData["b"] = b;
                ViewData["operation"] = operation;

                switch (operation)
                {
                    case '+':
                    ViewData["result"] = Operator.Add(a, b);
                    break;
                    case '-':
                    ViewData["result"] = Operator.Substract(a, b);
                    break;
                    case 'x':
                    ViewData["result"] = Operator.Multiply(a, b);
                    break;
                    case '/':
                    if (b != 0)
                    {
                        ViewData["result"] = Operator.Divide(a, b);
                    }
                    else
                    {
                        ViewData["result"] = "Error: División por cero";
                    }
                    break;
                    default:
                    ViewData["result"] = "Operación no válida";
                    break;
                }
            }
            else
            {
                ViewData["result"] = "Entrada inválida";
            }

            return View();
        }



    }
}
