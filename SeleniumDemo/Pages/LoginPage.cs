using OpenQA.Selenium;

namespace SeleniumDemo.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement LoginLink => driver.FindElement(By.Id("loginLink"));
        IWebElement TxtUserName => driver.FindElement(By.Id("UserName"));
        IWebElement TxtPassword => driver.FindElement(By.Id("Password"));
        IWebElement BtnLogin => driver.FindElement(By.CssSelector(".btn"));
        IWebElement LinkEmployeeDetails => driver.FindElement(By.LinkText("Employee Details"));
        IWebElement LinkManageUsers => driver.FindElement(By.LinkText("Manage Users"));
        IWebElement LinkLogoff => driver.FindElement(By.LinkText("Log off"));


        public void ClickLogin()
        {
            //LoginLink.Click();
            //SeleniumCustomMethods.Click(LoginLink);
            LoginLink.ClickElement();
        }

        public void Login(string username, string password)
        {
            //TxtUserName.SendKeys(username);
            //TxtPassword.SendKeys(password);
            //BtnLogin.Submit();
            //SeleniumCustomMethods.EnterText(TxtUserName, username);
            //SeleniumCustomMethods.EnterText(TxtPassword, password);
            //SeleniumCustomMethods.Submit(BtnLogin);

            TxtUserName.EnterText(username);
            TxtPassword.EnterText(password);
            BtnLogin.SubmitElement();
        }

        public (bool employeeDetails, bool manageUsers, bool linkLogoff) IsLoggedIn()
        {
            return (LinkEmployeeDetails.Displayed, LinkManageUsers.Displayed, LinkLogoff.Displayed);
        }
    }
}
