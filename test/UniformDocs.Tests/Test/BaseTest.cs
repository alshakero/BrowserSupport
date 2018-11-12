using System;
using System.Collections.Generic;
using System.Linq;
using BrowserSupport.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Net;

namespace BrowserSupport.Tests.Test
{
    public class BaseTest
    {
        public IWebDriver Driver;
        private readonly Config.Browser _browser;
        private readonly string _browsersTc = TestContext.Parameters["Browsers"];
        private List<string> _browsersToRun = new List<string>();
        private ResultState LastOutcome = null;
        private string LastOutcomeMessage = null;

        public BaseTest(Config.Browser browser)
        {
            _browser = browser;
        }

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            if (_browsersTc != null)
            {
                _browsersToRun = _browsersTc.Split(',').ToList();
            }
            else
            {
                _browsersToRun.Add("Chrome");
                _browsersToRun.Add("Firefox");
                //_browsersToRun.Add("Edge");
            }

            Uri serverUri = Config.RemoteWebDriverUri;
            if (TestContext.Parameters["Server"] != null)
            {
                serverUri = new Uri(TestContext.Parameters["Server"]);
            }

            if (_browsersToRun.Contains(Config.BrowserDictionary[_browser]))
            {
                Driver = WebDriverManager.StartDriver(_browser, Config.Timeout, serverUri);
            }
            else
            {
                Assert.Ignore(Config.BrowserDictionary[_browser] + " is on browsers ignore list");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (LastOutcome == null || TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                //this class has single driver session for all test methods
                //we don't want to mark a test as passed if it was already marked as failed
                LastOutcome = TestContext.CurrentContext.Result.Outcome;

                //LastOutcomeMessage = TestContext.CurrentContext.Result.StackTrace;
                LastOutcomeMessage = TestContext.CurrentContext.Result.Message;
                /*
                example stack trace:
                (Marked via REST API: at OpenQA.Selenium.Support.UI.DefaultWait`1.ThrowTimeoutException(String exceptionMessage, Exception lastException) at OpenQA.Selenium.Support.UI.DefaultWait`1.Until[TResult](Func`2 condition) at BrowserSupport.Tests.Test.BaseTest.WaitForText(IWeb)

                example message:
                (Marked via REST API: OpenQA.Selenium.WebDriverTimeoutException : Timed out after 5 seconds) 
                */
            }
        }

        protected TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, string errorMessage = null, int timeToWait = 10)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeToWait)) { Message = errorMessage };
            return wait.Until(condition);
        }

        public bool WaitForText(IWebElement elementName, string text, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(ExpectedConditions.TextToBePresentInElement(elementName, text));
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            if (WebDriverManager.IsCloud)
            {
                WebDriverManager.MarkTestStatusOnBrowserStack(LastOutcome, LastOutcomeMessage);
            }
            WebDriverManager.StopDriver();
        }

        
    }
}