using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            ViewData["a"] = calculator.A;
            ViewData["b"] = calculator.B;
            ViewData["operation"] = calculator.Command;
            switch (calculator.Command)
            {
                case ("+"):
                    ViewData["result"] = Operator.Add(calculator.A, calculator.B);
                    break;
                case ("-"):
                    ViewData["result"] = Operator.Substract(calculator.A, calculator.B);
                    break;
                case ("x"):
                    ViewData["result"] = Operator.Multiply(calculator.A, calculator.B);
                    break;
                case ("/"):
                    ViewData["result"] = Operator.Divide(calculator.A, calculator.B);
                    break;
                default:

                    break;
            }
            return View();
        }
    }
}
