using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumDemo.Pages;
using System.Text.Json;

namespace SeleniumDemo
{
    public class DataDrivenDemo
    {
        private IWebDriver _driver;
        public DataDrivenDemo()
        {
            //ReadJsonFile();
        }
        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);//default pool time = 1s
            _driver.Navigate().GoToUrl("http://eaapp.somee.com");
            _driver.Manage().Window.Maximize();
        }

        [Test]
        [Category("ddt")]
        [TestCaseSource(nameof(Login))]
        public void TestWithPOM(LoginModel loginModel)
        {
            LoginPage loginPage = new LoginPage(_driver);

            loginPage.ClickLogin();

            loginPage.Login(loginModel.UserName, loginModel.Password);
        }

        [Test]
        [Category("ddt")]
        [TestCaseSource(nameof(LoginJsonDataSource))]
        public void TestWithPOMWithJsonData(LoginModel loginModel)
        {
            //Arrange
            LoginPage loginPage = new LoginPage(_driver);

            //Act
            loginPage.ClickLogin();
            loginPage.Login(loginModel.UserName, loginModel.Password);

            //Assert
            var isLoggedIn = loginPage.IsLoggedIn();
            Assert.IsTrue(isLoggedIn.employeeDetails && isLoggedIn.manageUsers && isLoggedIn.linkLogoff);
        }

        public static IEnumerable<LoginModel> Login() {
            yield return new LoginModel()
            {
                UserName = "admin",
                Password = "password"
            };
            yield return new LoginModel()
            {
                UserName = "admin1",
                Password = "password"
            };
            yield return new LoginModel()
            {
                UserName = "admin2",
                Password = "password"
            };
        }

        private static IEnumerable<LoginModel> LoginJsonDataSource() {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "login.json");
            var jsonString = File.ReadAllText(jsonFilePath);
            var loginModel = JsonSerializer.Deserialize<List<LoginModel>>(jsonString, new JsonSerializerOptions { 
                PropertyNameCaseInsensitive = true
            });

            foreach (var loginData in loginModel)
            {
                yield return loginData;
            }
        }

        [TearDown]
        public void TearDown()
        {
            //_driver.Quit();
        }
    }
}
