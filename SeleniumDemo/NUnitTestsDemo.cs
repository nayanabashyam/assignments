using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumDemo.Pages;

namespace SeleniumDemo
{
    [TestFixture("admin", "password")]
    [TestFixture("admin", "password2")]
    public class NUnitTestsDemo
    {
        private IWebDriver _driver;
        private string userName;
        private string password;
        public NUnitTestsDemo(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        [SetUp]
        public void SetUp() {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://eaapp.somee.com");
            _driver.Manage().Window.Maximize();
        }

        [Test]
        [TestCase("chrome", "40")]
        public void TestBrowserVersion(string browser, string version) {
            Console.WriteLine(browser+" "+version);
        }

        [Test]
        [Category("smoke")]
        public void TestWithPOM()
        {
            LoginPage loginPage = new LoginPage(_driver);

            loginPage.ClickLogin();

            loginPage.Login(userName, password);
        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
