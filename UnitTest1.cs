using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumStuff
{
    public class Tests
    {
        private IWebDriver driver;
        
        [SetUp]
        public void Setup()
        {
            // Set up, logging in, clearing cookies, etc. 
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://sweetshop.netlify.app/");
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            IWebElement loginMainPage = driver.FindElement(By.LinkText("Login"));
            loginMainPage.Click();
            IWebElement emailLogIn = driver.FindElement(By.Id("exampleInputEmail"));
            emailLogIn.Click();
            emailLogIn.SendKeys(("testemail@gmail.com"));
            IWebElement password = driver.FindElement(By.Id("exampleInputPassword"));
            password.Click();
            password.SendKeys(("password123" + Keys.Enter));
        }

        [Test]
        public void Test1()
        {
            IWebElement browseSweets = driver.FindElement(By.CssSelector(".btn.btn-primary.btn-lg.sweets"));
            browseSweets.Click();

            // webElement.SendKeys to type
        }

        [TearDown]
            public void Cleanup()
            {
                driver.Quit();
            }
    }
}