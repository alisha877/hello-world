using System;
using System.Configuration;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace Facebook_Login
{
    class Class1
    {
        IWebDriver driver;

       string userEmail = ConfigurationSettings.AppSettings["email"];
        string userPassword = ConfigurationSettings.AppSettings["pwd"];
        string invalid_pwd = ConfigurationSettings.AppSettings["invalid_pwd"];

        [OneTimeSetUp]
        public void Setup()
        {
            //  driver = new FirefoxDriver();
            var options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }
        public void Openbrowser()
        {
            driver.Navigate().GoToUrl("http://www.facebook.com");
        }


 
        public void UserLogin()
        {
            Openbrowser();
            

            //IWebElement email = driver.FindElement(By.XPath("//*[@id='identifierId']"));

            IWebElement Email = driver.FindElement(By.Name("email"));
            Email.SendKeys(userEmail);

            // var elmlogin = driver.FindElement(By.Id("identifierNext"));
            // elmlogin.Click();

            //wait

            //Thread.Sleep(8000);

            //explicit
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var password = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("pass")));

            // IWebElement password = driver.FindElement(By.Id("u_0_a"));
            password.SendKeys(userPassword);

            var next = driver.FindElement(By.Id("u_0_2"));
            next.Click();

        }

        [Test] //testcase2
        public void VerifyFriendslist()
        {
            int expectedFriendsList = 776;
            UserLogin();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a[@title='Profile']")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a[@data-tab-key='friends']")).Click();
            Thread.Sleep(2000);


            bool loop = true;
            while (loop)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,1000)");

                try
                {
                    var more = driver.FindElement(By.XPath("//h3[text()='More About You']"));
                    if (more.Text != null)
                    {
                        loop = false;
                        break;
                    }
                }
                catch (NoSuchElementException e)
                {
                    loop = true;
                    Console.WriteLine(e);
                }
            }

                 //endwhile

                 var nameElements = driver.FindElements(By.XPath("//div[@class='fsl fwb fcb']/a"));

                 foreach (var nameElement in nameElements)
                 {
                     Console.WriteLine(nameElement.Text);
                 }
                 Assert.AreEqual(expectedFriendsList, nameElements.Count);

             }
        

         /*Thread.Sleep(10);
         var Invalid = driver.FindElement(By.Id("globalContainer"));
         bool status = Invalid.Displayed;
         Assert.IsTrue(status); 
 /*
         Thread.Sleep(3000);
         var Home = driver.FindElement(By.Id("u_0_a"));
         String strHome = Home.Text;
         Assert.AreEqual(strHome, "Home"); */





                // [OneTimeTearDown]
                //public void End()
                //{
                //  driver.Quit();

            }


        }
   
