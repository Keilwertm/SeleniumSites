using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumStuff;

public class BookStore
{
    // https://bookcart.azurewebsites.net/  
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Starting test: {TestContext.CurrentContext.Test.Name}");
        driver = new ChromeDriver(); // <-- removed `IWebDriver`
        driver.Navigate().GoToUrl("https://bookcart.azurewebsites.net/");
        driver.Manage().Window.Maximize();
        driver.Manage().Cookies.DeleteAllCookies();
    }

    [Test]
    public void Login_Register()
    {
        // Register and login as a user
    }

    [Test]
    public void Book_Search()
    {
        // Searching for specific books and validating search bar functions
    }
    
    [Test]
    public void Cart_Total()
    {
        // Adding books to cart and checking the total, along with removing books and checking the total, Clear Cart 
    }
    
    [Test]
    public void Price_Filter()
    {
        // Filtering the price of books, and validating that the filter slider works 
    }
    
    [Test]
    public void Category_Filter()
    {
        // Verify filters work as intended for Categories
    }

    [TearDown]
    public void Teardown()
    {
        driver.Quit();
        driver.Dispose();
    }
    
    // I can also double-check that "Similar Books" updates properly 
}