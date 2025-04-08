using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumStuff;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void Test1()
    {
   
      IWebDriver driver = new ChromeDriver();
      driver.Navigate().GoToUrl("https://sweetshop.netlify.app/");
      driver.Manage().Window.Maximize();
     
      IWebElement button = driver.FindElement(By.CssSelector(".btn.btn-primary.btn-lg.sweets"));
      button.Click();
      
      // webElement.SendKeys to type
     
    }
}