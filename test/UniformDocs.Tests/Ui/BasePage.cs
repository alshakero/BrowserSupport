using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace BrowserSupport.Tests.Ui
{
    public class BasePage
    {
        public IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.TagName, Using = "input")]
        public IWebElement Input { get; set; }

        [FindsBy(How = How.TagName, Using = "output")]
        public IWebElement Output { get; set; }

        public void SetInputValue(IWebElement elem, int value)
        {
            elem.SendKeys(value.ToString());
        }

        public BasePage GoToMainPage()
        {
            Driver.Navigate().GoToUrl(BrowserSupport.Tests.Utilities.Config.BrowserSupportUrl);
            return this;
        }
    }
}
