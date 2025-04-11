using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumStuff
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]

    public class SweetStuff
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine(
                $"[{Thread.CurrentThread.ManagedThreadId}] Starting test: {TestContext.CurrentContext.Test.Name}");
            driver = new ChromeDriver(); 
            driver.Navigate().GoToUrl("https://sweetshop.netlify.app/");
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();

            IWebElement sweetShop = driver.FindElement(By.ClassName("navbar-brand"));
            sweetShop.Click();
        }

        [Test]
        public void Cart_Total()
        {
            // Adding things to cart, validating total. 

            decimal total = 0;
            
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
        }

        [Test]
        public void Improper_Login()
        {
            // Validate wrong login information 
            
            driver.Navigate().GoToUrl("https://sweetshop.netlify.app/");
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
        }

        [Test]
        public void Missing_Image()
        {
            // Catch missing image
            
            driver.Navigate().GoToUrl("https://sweetshop.netlify.app/");
            IWebElement browseSweets = driver.FindElement(By.LinkText("Sweets"));
            browseSweets.Click();

            var productCards = driver.FindElements(By.CssSelector("div.col-lg-3.col-md-6.mb-4"));

            foreach (var productCard in productCards)
            {
                // Find the image inside each product card
                IWebElement imageElement = productCard.FindElement(By.CssSelector("img.card-img-top"));
                string imageUrl = imageElement.GetAttribute("src");
                var jsExecutor =
                    (IJavaScriptExecutor)driver; // Using JavaScript to access natural width and height as supposedly C# cannot
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
        }

        [Test]
        public void Empty_Cart()
        {
            // deleting and adding things to the cart updates the total, along with shipping

            decimal total = 0;
            
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

            Assert.That(total, Is.EqualTo(basketGbp), "Basket total does not match expected total.");

            for (int i = 0;
                 i < loopCandyToCart;
                 i++)
            {
                IWebElement quantity1 = driver.FindElement(By.XPath("(//small[@class='text-muted'])[1]"));
                quantity1.Click();

                IWebElement deleteItem1 =
                    driver.FindElement(By.XPath("//a[contains(@href, 'removeItem') and contains(@class, 'small')]"));
                deleteItem1.Click();

                Thread.Sleep(1000);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }

            IWebElement sweetHome = driver.FindElement(By.CssSelector("a.navbar-brand[href='/']"));
            sweetHome.Click();
            IWebElement browseSweetsButton =
                driver.FindElement(By.CssSelector("a.btn.btn-primary.btn-lg.sweets[href='/sweets']"));

            browseSweetsButton.Click();

            IWebElement item8 = driver.FindElement(By.CssSelector("a.addItem[data-id='8']"));
            IWebElement item9 = driver.FindElement(By.CssSelector("a.addItem[data-id='8']"));

            for (int i = 0; i < loopCandyToCart; i++)
            {
                item8.Click();
                item9.Click();
            }

            driver.Navigate().GoToUrl("https://sweetshop.netlify.app/basket");
            // I know this isn't ideal but EVERYTHING gives stale element after used once and I already got around two other instances of this
            
            IWebElement emptyBasketButton = driver.FindElement(By.XPath("//a[contains(@onclick, 'emptyBasket')]"));
            emptyBasketButton.Click();
            IAlert alert2 = driver.SwitchTo().Alert();
            alert2.Accept();
            
            Thread.Sleep(2000);
         //   basketGbp.ShouldBe(0.00m, 0.01m);  check this later shouldly stuff to validate the cart = 0. It's currently taking the value before the cart is cleared for some reason.
            Thread.Sleep(2000);
        }

        [Test]
        public void Correct_Billing()
        {
            // check correct payment and billing information or incorrect billing information
            
            driver.Navigate().GoToUrl("https://sweetshop.netlify.app/");

            Random random = new Random();
            int loopCandyToCart = random.Next(3, 7);

            IWebElement browseSweets = driver.FindElement(By.LinkText("Sweets"));
            browseSweets.Click();

            IWebElement item8 = driver.FindElement(By.CssSelector("a.addItem[data-id='8']"));
            IWebElement item9 = driver.FindElement(By.CssSelector("a.addItem[data-id='8']"));

            for (int i = 0; i < loopCandyToCart; i++)
            {
                item8.Click();
                item9.Click();
            }

            // checking billing information is correct, and when incorrect displays the error message 

            IWebElement basketLink = driver.FindElement(By.CssSelector("a.nav-link[href='/basket']"));
            basketLink.Click();

            IWebElement firstName = driver.FindElement(By.CssSelector("input#name"));
            firstName.SendKeys("mel");
            IWebElement lastNameInput = driver.FindElement(By.XPath("(//input[@id='name'])[2]"));
            lastNameInput.SendKeys("Smith");
            IWebElement emailInput = driver.FindElement(By.CssSelector("input#email"));
            emailInput.SendKeys("mel@gmail.com");
            IWebElement address2Input = driver.FindElement(By.CssSelector("input#address2"));
            address2Input.SendKeys("stonewall drive");
            IWebElement zipInput = driver.FindElement(By.CssSelector("input#zip"));
            zipInput.SendKeys("12345");
            IWebElement ccNameInput = driver.FindElement(By.CssSelector("input#cc-name"));
            ccNameInput.SendKeys("mel k");
            IWebElement ccNumberInput = driver.FindElement(By.CssSelector("input#cc-number"));
            ccNumberInput.SendKeys("12345");
            IWebElement ccExpirationInput = driver.FindElement(By.CssSelector("input#cc-expiration"));
            ccExpirationInput.SendKeys("05/25");
            IWebElement ccCvvInput = driver.FindElement(By.CssSelector("input#cc-cvv"));
            ccCvvInput.SendKeys("778");

            IWebElement countryDropdown = driver.FindElement(By.Id("country"));
            SelectElement selectCountry = new SelectElement(countryDropdown);
            selectCountry.SelectByText("United Kingdom");

            IWebElement cityDropdown = driver.FindElement(By.Id("city"));
            SelectElement selectCity = new SelectElement(cityDropdown);
            selectCity.SelectByText("Bristol");

            IWebElement continueButton = driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-lg.btn-block[type='submit']"));
            continueButton.Click();
            
            firstName.SendKeys("#!@#$!!");
            lastNameInput.SendKeys("!@#$%SFE!!!!!#@F");
            emailInput.SendKeys("wrong wrong");
            address2Input.SendKeys("321321321!!!!!");
            ccNameInput.SendKeys("32142132!!!!");
            ccNumberInput.SendKeys("abc");
            ccExpirationInput.SendKeys("idk");
            ccCvvInput.SendKeys("771546568");
            continueButton.Click();

            try
            {
                bool emailErrorVisible = driver.FindElement(By.XPath("//div[contains(text(), 'Please enter a valid email address')]")).Displayed;
                bool lastNameErrorVisible = driver.FindElement(By.XPath("//div[contains(text(), 'Valid last name is required.')]")).Displayed;
                bool cvvErrorVisible = driver.FindElement(By.XPath("//div[contains(text(), 'Security code required')]")).Displayed;

                if (emailErrorVisible && lastNameErrorVisible && cvvErrorVisible)
                {
                    Console.WriteLine("All errors are showing properly.");      // I'll have to check this later
                }
                else
                {
                    Console.WriteLine("Some errors are not visible, validation failed.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Some errors are not visible, validation failed.");
            }
        }

            [Test]
            public void Table_Sort()
            {
                // Validate table is sorting correctly in the login page
                
                driver.Navigate().GoToUrl("https://sweetshop.netlify.app/");
                
                IWebElement loginMainPage = driver.FindElement(By.LinkText("Login"));
                loginMainPage.Click();

                IWebElement emailLogIn = driver.FindElement(By.Id("exampleInputEmail"));
                emailLogIn.SendKeys("testemail@gmail.com");

                IWebElement password = driver.FindElement(By.Id("exampleInputPassword"));
                password.SendKeys("wq@#as#$!~AAA!" + Keys.Enter);
                
                // let us grab the default value of the first in the table (1) double click to sort the table and validate that the new top number is MORE THAN the first stored value. 
                
                IWebElement firstValue = driver.FindElement(By.CssSelector("th[scope='row']"));
                string text = firstValue.Text; // e.g. "#1"
                int number = int.Parse(text.Replace("#", ""));
                
                IWebElement orderNumber = driver.FindElement(By.CssSelector("a.order-number[href*='SortTable(0']"));
                Actions actions = new Actions(driver);
                actions.DoubleClick(orderNumber).Perform();

                Thread.Sleep(1000); 

                firstValue = driver.FindElement(By.CssSelector("th[scope='row']"));
                string text2 = firstValue.Text; // e.g. "#3"
                int number2 = int.Parse(text2.Replace("#", "")); 

                if (number2 > number)
                {
                    Console.WriteLine("Sorting table working as intended.");
                }
                else if (number2 < number)
                {
                    Console.WriteLine("Sorting table is not working as intended.");
                }
                else
                {
                    Console.WriteLine("Table didn't change or sorting had no effect.");
                }
            }
            
            [TearDown]
            public void Teardown()
            {
                    driver.Quit();
                    driver.Dispose();
            }
         }
    }
    
    // Improvements I can make - Introduce a page object model? Add in more asserts? Refactor repeated code? Get parallel tests working?