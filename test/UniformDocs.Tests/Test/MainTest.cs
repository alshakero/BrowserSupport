using BrowserSupport.Tests.Ui;
using BrowserSupport.Tests.Utilities;
using NUnit.Framework;
using System;

namespace BrowserSupport.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    class MainTest : BaseTest
    {
        BasePage _page;
        public MainTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _page = new BasePage(Driver).GoToMainPage();
        }


        [Test]
        public void TestChangeValue()
        {
            WaitUntil(x => _page.Input.Displayed);
            Random rnd = new Random();
            int value = rnd.Next(1, 200);
            _page.SetInputValue(_page.Input, value);
            WaitUntil(x => _page.Input.Displayed);
            _page.Output.Click();
            Assert.IsTrue(WaitForText(_page.Output, $"Mirrored value is: {value.ToString()}", 5));

        }
    }
}
