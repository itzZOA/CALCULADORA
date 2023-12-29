using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;

namespace bdd.workshop.calculator.test.selenium
{
    public class CalculatorTests
    {

        private IWebDriver driver;

        public CalculatorTests()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Url = "https://calculator-ugr.azurewebsites.net/";
        }

        [Fact]
        [Trait("TestType", "FT")]
        public void BasicAdd()
        {
            // Configuración de prueba
            string op = "+";
            int a = 1;
            int b = 2;
            int expectedResult = 3;

            // Realizando la operación en la interfaz
            PerformOperation(a.ToString(), op, b.ToString());

            // Obteniendo y validando el resultado
            var result = GetResult();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        [Trait("TestType", "FT")]
        public void BasicSubtract()
        {
            string op = "-";
            int a = 5;
            int b = 3;
            int expectedResult = 2;

            PerformOperation(a.ToString(), op, b.ToString());
            var result = GetResult();
            Assert.Equal(expectedResult, result);
        }


        [Fact]
        [Trait("TestType", "FT")]
        public void BasicMultiply()
        {
            string op = "x"; 
            int a = 4;
            int b = 6;
            int expectedResult = 24;

            PerformOperation(a.ToString(), op, b.ToString());
            var result = GetResult();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        [Trait("TestType", "FT")]
        public void BasicDivide()
        {
            string op = "/"; 
            int a = 8;
            int b = 2;
            int expectedResult = 4;

            PerformOperation(a.ToString(), op, b.ToString());
            var result = GetResult();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        [Trait("TestType", "FT")]
        public void SquareRoot()
        {
            string op = "√"; 
            int a = 9; 
            int expectedResult = 3;

            PerformSquareRootOperation(op, a.ToString());
            var result = GetResult();
            Assert.Equal(expectedResult, result);
        }

        private void PerformSquareRootOperation(string operation, string operand)
        {
            ClickButtonWithValue(operation);
            ClickButtonWithValue(operand);
            ClickButtonWithValue("=");
        }

        private void PerformOperation(string operand1, string operation, string operand2)
        {
            ClickButtonWithValue(operand1);
            ClickButtonWithValue(operation);
            ClickButtonWithValue(operand2);
            ClickButtonWithValue("=");
        }

        private void ClickButtonWithValue(string value)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement button = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath($"//input[@value='{value}']")));
            button.Click();
        }

        private int GetResult()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement resultTable = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("table.table-bordered tbody")));

            var resultCells = resultTable.FindElements(By.TagName("td"));

            if (resultCells.Count >= 5)
            {
                string resultText = resultCells[4].Text;
                if (int.TryParse(resultText, out int result))
                {
                    return result;
                }
                else
                {
                    throw new InvalidOperationException("Formato de resultado inesperado: " + resultText);
                }
            }
            else
            {
                throw new InvalidOperationException("No se encontraron suficientes celdas en la fila del resultado.");
            }
        }




        public void Dispose()
        {
            driver.Quit();
        }
    }
}
