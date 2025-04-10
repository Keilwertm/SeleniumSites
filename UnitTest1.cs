using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumStuff
{
    public class Tests
    {
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
            password.SendKeys(("wq@#as#$!~AAA!" + Keys.Enter));
            IWebElement sweetShop = driver.FindElement(By.ClassName("navbar-brand"));
            sweetShop.Click();
            driver.Quit();
        }
        
        [Test]
            public void Test1()
            {
                // Adding things to cart, validating total. 

                decimal total = 0;
                
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://sweetshop.netlify.app/");
                IWebElement browseSweets = driver.FindElement(By.LinkText("Sweets"));
                browseSweets.Click();
                
                IWebElement basketLink = driver.FindElement(By.CssSelector("a.nav-link[href='/basket']"));
                
                IWebElement item1 = driver.FindElement(By.CssSelector("a.addItem[data-id='1']"));
                IWebElement firstPrice = driver.FindElement(By.XPath("(//small[@class='text-muted'])[1]"));
                string price1 = firstPrice.Text;
                decimal priceN1 = decimal.Parse(price1.Replace("\u00a3", "").Trim());
                
                IWebElement item2 = driver.FindElement(By.CssSelector("a.addItem[data-id='2']"));
                IWebElement secondPrice = driver.FindElement(By.XPath("(//small[@class='text-muted'])[2]"));
                string price2 = secondPrice.Text;
                decimal priceN2 = decimal.Parse(price2.Replace("\u00a3", "").Trim());
                
                IWebElement item3 = driver.FindElement(By.CssSelector("a.addItem[data-id='3']"));
                IWebElement thirdPrice = driver.FindElement(By.XPath("(//small[@class='text-muted'])[3]"));
                string price3 = thirdPrice.Text;
                decimal priceN3 = decimal.Parse(price3.Replace("\u00a3", "").Trim());
                
                IWebElement item4 = driver.FindElement(By.CssSelector("a.addItem[data-id='4']"));
                IWebElement fourthPrice = driver.FindElement(By.XPath("(//small[@class='text-muted'])[4]"));
                string price4 = fourthPrice.Text;
                decimal priceN4 = decimal.Parse(price4.Replace("\u00a3", "").Trim());
                
                IWebElement item5 = driver.FindElement(By.CssSelector("a.addItem[data-id='5']"));
                IWebElement fifthPrice = driver.FindElement(By.XPath("(//small[@class='text-muted'])[5]"));
                string price5 = fifthPrice.Text;
                decimal priceN5 = decimal.Parse(price5.Replace("\u00a3", "").Trim());
                
                IWebElement item6 = driver.FindElement(By.CssSelector("a.addItem[data-id='6']"));
                IWebElement sixthPrice = driver.FindElement(By.XPath("(//small[@class='text-muted'])[6]"));
                string price6 = sixthPrice.Text;
                decimal priceN6 = decimal.Parse(price6.Replace("\u00a3", "").Trim());
                
                Console.WriteLine(price1 + " " + price2 + " " + price3 + " " + price4 + " " + price5  + " " + price6);
                
                Random random = new Random();
                int loopCandyToCart = random.Next(3, 14);
                
                for (int i = 0; i < loopCandyToCart; i++)
                {
                    item1.Click();
                    total += priceN1;
                    item2.Click();
                    total += priceN2;
                    item3.Click();
                    total += priceN3;
                    item4.Click();
                    total += priceN4;
                    item5.Click();
                    total += priceN5;
                    item6.Click();
                    total += priceN6;
                }
               
                basketLink.Click();
                IWebElement basketHere = driver.FindElement(By.CssSelector("strong"));
                string basketTotal = basketHere.Text;
                decimal basketGbp = decimal.Parse(basketTotal.Replace("\u00a3", "").Trim());
                Console.WriteLine(total);
                
                if (total == basketGbp)
                {
                    Console.WriteLine("Total Values match!");
                }
                else
                {
                    Console.WriteLine("Total Values differ!");
                }
                driver.Quit();
            }

            public void Test2()
            {
                // Validate wrong login information 
            }

            public void Test3()
            {
                // catch missing image
            }

            public void Test4()
            {
                // check correct payment and billing information 
            }

            public void Test5()
            {
                // deleting and adding things to the cart updates the total, along with shipping
            }

            public void Test6()
            {
                // promo code works properly and discounts the total 
            }

            
            /*
            [TearDown]
            public void Teardown()
            {
                    driver.Quit();
            }
            // Add in Screenshots on fail? 
            // There has to be a better way to have a continuous test instead of opening the web driver for every test. 
            */
            
        }
    }