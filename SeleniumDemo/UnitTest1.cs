using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumDemo.Pages;
using Polly;
using Polly.Retry;

namespace SeleniumDemo
{
    public class Tests
    {
        [SetUp]

        public void Setup()
        {
        }

        //Method to define Retry Policy
        private static RetryPolicy RetryPolicy() 
        {
            var retryPolicy = Policy.Handle<NoSuchElementException>()
                .Or<StaleElementReferenceException>()
                .WaitAndRetry(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(3),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine($"Retry {retryCount} due to {exception.Message}. Waiting {timeSpan} before next retry.");
                    });
            return retryPolicy;

        }
        [Test]
        public void Test1()
        {
            //Working with the Driver
            //Setup Selenium
            //1.Create a new instance of the Selenium Web Driver
            IWebDriver driver = new ChromeDriver();

            //Navigate to the URL
            driver.Navigate().GoToUrl("https://www.google.com");

            //Maximize the browser
            driver.Manage().Window.Maximize();

            //WebDriverWait webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //webDriverWait.Until(_ => driver.FindElement(By.Name("qq")).Displayed);

            //Explicit Wait
            WebDriverWait webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)) {
                PollingInterval = TimeSpan.FromMilliseconds(2000),
                Message = "Textbox not found."
            };
            //var txtUserName = webDriverWait.Until(d =>
            //{
            //    var element = driver.FindElement(By.Name("qq"));
            //    return (element != null && element.Displayed) ? element : null;
            //});

            //Retry Policy by the method
            var txtUserName = RetryPolicy().Execute(() => driver.FindElement(By.Name("qq")));
           


            //Find the element
            //IWebElement webElement = driver.FindElement(By.Name("qq"));

            //Enter text in the element
            //webElement.SendKeys("Selenium");
            txtUserName.SendKeys("Selenium");

            //Press Enter
            txtUserName.SendKeys(Keys.Return);

            //driver.Quit();
        }
        //Locators - identify the UI elements(ID, Name, TagName, ClassName, LinkText, XPath)
        //[Test]
        //public void EAAPPSiteTest()
        //{
        //    //1.Create a new instance of the Selenium Web Driver
        //    IWebDriver driver = new ChromeDriver();
        //    //Navigate to the URL
        //    driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        //    driver.Manage().Window.Maximize();

        //    //driver.FindElement(By.Id("loginLink")).Click();
        //    SeleniumCustomMethods.Click(driver, By.Id("loginLink"));
        //    #region
        //    //driver.FindElement(By.Name("UserName")).SendKeys("admin");
        //    //driver.FindElement(By.Name("Password")).SendKeys("admin"); 
        //    #endregion
        //    SeleniumCustomMethods.EnterText(driver, By.Name("UserName"), "admin");
        //    SeleniumCustomMethods.EnterText(driver, By.Name("Password"), "admin");
        //    #region 
        //    //IWebElement bthSubmit = driver.FindElement(By.ClassName("btn"));
        //    //bthSubmit.Submit(); 
        //    #endregion
        //    driver.FindElement(By.CssSelector(".btn")).Submit();

        //    //driver.Quit();
        //}
        //[Test]
        //public void DropDownTest()
        //{
        //    //1.Create a new instance of the Selenium Web Driver
        //    IWebDriver driver = new ChromeDriver();

        //    //Navigate to the URL
        //    driver.Navigate().GoToUrl("file:///C:/Users/partha.bora/source" +
        //        "/repos/SpecflowDemo/SeleniumDemo/TestPage.html");
        //    driver.Manage().Window.Maximize();

        //    #region 
        //    //SelectElement selectElement = new SelectElement(driver.FindElement(By.Id("country")));
        //    //selectElement.SelectByText("Germany"); 
        //    #endregion
        //    SeleniumCustomMethods.SelectDropDownByText(driver, By.Id("country"), "DE");
        //    #region 
        //    //SelectElement selectMultiElement = new SelectElement(driver.FindElement(By.Id("skills")));
        //    //selectMultiElement.SelectByText("CSS");
        //    //selectMultiElement.SelectByText("React"); 
        //    #endregion
        //    SeleniumCustomMethods.MultiSelectElements(driver, By.Id("skills"), ["css", "react"]);
        //    #region 
        //    //IList<IWebElement> selectedOptions = selectMultiElement.AllSelectedOptions;
        //    //foreach (IWebElement item in selectedOptions)
        //    //{
        //    //    Console.WriteLine(item.Text);
        //    //}
        //    //driver.Quit(); 
        //    #endregion
        //    List<string> getSelectedOptions = SeleniumCustomMethods.GetAllSelectedLists(driver, By.Id("skills"));
        //    getSelectedOptions.ForEach(Console.WriteLine);


        //}

        [Test]
        public void TestWithPOM()
        {
            //1.Create a new instance of the Selenium Web Driver
            IWebDriver driver = new ChromeDriver();

            //Navigate to the URL
            driver.Navigate().GoToUrl("http://eaapp.somee.com");
            LoginPage loginPage = new LoginPage(driver);

            loginPage.ClickLogin();

            loginPage.Login("admin","password");
        }
    }
}