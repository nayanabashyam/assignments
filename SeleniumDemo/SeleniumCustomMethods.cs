using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumDemo
{
    public static class SeleniumCustomMethods
    {
        //public static void Click(IWebDriver driver, By locator) { 
        //    driver.FindElement(locator).Click();
        //} 

        public static void ClickElement(this IWebElement locator)
        {
            locator.Click();
        }
        public static void SubmitElement(this IWebElement locator)
        {
            locator.Submit();
        }
        //public static void EnterText(IWebDriver driver, By locator, string text) {
        //    driver.FindElement(locator).Clear();
        //    driver.FindElement(locator).SendKeys(text);
        //}
        public static void EnterText(this IWebElement locator, string text)
        {
            locator.Clear();
            locator.SendKeys(text);
        }
        //public static void SelectDropDownByText(IWebDriver driver, By locator, string value)
        //{
        //    SelectElement selectElement = new SelectElement(driver.FindElement(locator));
        //    selectElement.SelectByValue(value);
        //}

        public static void SelectDropDownByText(this IWebElement locator, string text)
        {
            SelectElement selectElement = new SelectElement(locator);
            selectElement.SelectByText(text);
        }

        public static void SelectDropDownByValue(this IWebElement locator, string value)
        {
            SelectElement selectElement = new SelectElement(locator);
            selectElement.SelectByValue(value);
        }

        //public static void MultiSelectElements(IWebDriver driver, By locator, string[] values)
        //{
        //    SelectElement selectElement = new SelectElement(driver.FindElement(locator));
        //    foreach (var value in values)
        //    {
        //        selectElement.SelectByValue(value);
        //    }

        //}
        public static void MultiSelectElements(this IWebElement locator, string[] values)
        {
            SelectElement selectElement = new SelectElement(locator);
            foreach (var value in values)
            {
                selectElement.SelectByValue(value);
            }

        }
        public static List<string> GetAllSelectedLists(this IWebDriver driver, By locator) { 
            List<string> options = new List<string>();
            SelectElement multiselect = new SelectElement(driver.FindElement(locator));

            IList<IWebElement> selectedOptions = multiselect.AllSelectedOptions;

            foreach (IWebElement option in selectedOptions) { 
                options.Add(option.Text);
            }
            return options;
        }
    }
}
