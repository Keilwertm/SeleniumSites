using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;



namespace SeleniumStuff
{
    public class SweetStuff
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

            Console.WriteLine(price1 + " " + price2 + " " + price3 + " " + price4 + " " + price5 + " " + price6);

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
                Console.WriteLine(total == basketGbp ? "Total Values match!" : "Total Values do not match!!");

            }
            driver.Quit();
        }
        
        [Test]
        public void Test2()
        {
            // Validate wrong login information 
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://sweetshop.netlify.app/");
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            IWebElement loginMainPage = driver.FindElement(By.LinkText("Login"));
            loginMainPage.Click();
            IWebElement emailLogIn = driver.FindElement(By.Id("exampleInputEmail"));
            emailLogIn.Click();
            emailLogIn.SendKeys(("incorrect email"));
            IWebElement password = driver.FindElement(By.Id("exampleInputPassword"));
            password.Click();
            password.SendKeys(("" + Keys.Enter));
            IWebElement invalidEmail = driver.FindElement(By.CssSelector(".invalid-feedback.invalid-email"));
            IWebElement invalidPassword = driver.FindElement(By.CssSelector(".invalid-feedback.invalid-password"));

            if (invalidEmail.Displayed && invalidPassword.Displayed)
            {
                Console.WriteLine("Invalid Login text is displayed!");
            }

            driver.Quit();
        }
        
        [Test]
        public void Test3()
        {
            // Catch missing image
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://sweetshop.netlify.app/");
            IWebElement browseSweets = driver.FindElement(By.LinkText("Sweets"));
            browseSweets.Click();

            var productCards = driver.FindElements(By.CssSelector("div.col-lg-3.col-md-6.mb-4"));

            foreach (var productCard in productCards)
            {
                // Find the image inside each product card
                IWebElement imageElement = productCard.FindElement(By.CssSelector("img.card-img-top"));
                string imageUrl = imageElement.GetAttribute("src"); // Getting the image source URL
                var jsExecutor = (IJavaScriptExecutor)driver; // Using JavaScript to access natural width and height
                var imageWidth = (long)jsExecutor.ExecuteScript("return arguments[0].naturalWidth;", imageElement);
                var imageHeight = (long)jsExecutor.ExecuteScript("return arguments[0].naturalHeight;", imageElement);

                if (string.IsNullOrEmpty(imageUrl) || imageUrl.Contains("missing") || imageUrl.Contains("404"))
                {
                    Console.WriteLine("There is a missing Image source - invalid or missing.");
                }
                else if (imageWidth != 500 || imageHeight != 300)
                {
                    Console.WriteLine(
                        $"Image has an invalid size. Expected 500px width and 300px height. Actual: {imageWidth}x{imageHeight}");
                }
                else
                {
                    Console.WriteLine("Image is valid and size is correct (500px width and 300px height).");
                }
            }

            driver.Quit();
        }

        [Test]
        public void Test4()
        {
            // deleting and adding things to the cart updates the total, along with shipping
            
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
            
            // There is probably a way better way to do this, because I use these in Test1, a more compact way. A UI Map or something.

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

            Random random = new Random();
            int loopCandyToCart = random.Next(3, 7);

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

            if (total == basketGbp)
            {
                Console.WriteLine(total == basketGbp ? "Total Values match!" : "Total Values do not match!!");
            }

            for (int i = 0;
                 i < loopCandyToCart;
                 i++) 
            {
                IWebElement quantity1 = driver.FindElement(By.XPath("(//small[@class='text-muted'])[1]"));
                quantity1.Click();

                IWebElement deleteItem1 = driver.FindElement(By.XPath("//a[contains(@href, 'removeItem') and contains(@class, 'small')]"));
                deleteItem1.Click();
                
                Thread.Sleep(1000);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            
            IWebElement sweetHome = driver.FindElement(By.CssSelector("a.navbar-brand[href='/']"));
            sweetHome.Click();
            IWebElement browseSweetsButton = driver.FindElement(By.CssSelector("a.btn.btn-primary.btn-lg.sweets[href='/sweets']"));
            
            browseSweetsButton.Click();
            
            IWebElement item8 = driver.FindElement(By.CssSelector("a.addItem[data-id='8']"));
            IWebElement item9 = driver.FindElement(By.CssSelector("a.addItem[data-id='8']"));
            
            for (int i = 0; i < loopCandyToCart; i++)
            {
                item8.Click();
                item9.Click();
            }
            
            driver.Navigate().GoToUrl("https://sweetshop.netlify.app/basket"); 
            // I know this isn't ideal but EVERYTHING gives stale element after used once and I already got around three others
            IWebElement emptyBasketButton = driver.FindElement(By.XPath("//a[contains(@onclick, 'emptyBasket')]"));
                emptyBasketButton.Click();
                IAlert alert2 = driver.SwitchTo().Alert();
                alert2.Accept();
                
                if (basketGbp == 0.00m)
                {
                    Console.WriteLine("Empty Basket!");
                }
                else if (basketGbp > 0)
                {
                    Console.WriteLine("Error emptying basket!"); // This always displays Error emptying basket, even though the basket is empty. 
                }
                Thread.Sleep(2000);
            driver.Quit();
        }
    public void Test5()
            {
                // check correct payment and billing information or incorrect billing information
            }
        }
    }