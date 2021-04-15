using NUnit.Framework;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace ParrotsNameSiteTests
{
    public class SeleniumTests
    {
        public ChromeDriver driver;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
        }

        private By emailInputLocator = By.Name("email");
        private By radioButtonBoyLocator = By.Id("boy");
        private By radioButtonGirlLocator = By.Id("girl");
        private By sendButtonLocator = By.Id("sendMe");
        private By resultTextLocator = By.ClassName("result-text");
        private By emailResultTextLocator = By.ClassName("your-email");
        private By anotherEmailLinkLocator = By.Id("anotherEmail");
        private By errorTextLocator = By.ClassName("form-error");

        private const string siteUrl = "https://qa-course.kontur.host/selenium-practice";
        private const string correctEmail = "test@email.ru";
        private const string wrongEmail = "te∞#¢#€¢∞";

        [Test]

        public void enterCorrectEmail()
        {
            var expectedEmail = "test@email.ru";
            driver.Navigate().GoToUrl(siteUrl);
            driver.FindElement(emailInputLocator).SendKeys(correctEmail);
            driver.FindElement(sendButtonLocator).Click();
            Assert.AreEqual(expectedEmail, driver.FindElement(emailResultTextLocator).Text, "Почта не совпадает");
        }

        public void selectBoysNames()
        {
            driver.Navigate().GoToUrl(siteUrl);
            driver.FindElement(radioButtonBoyLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(correctEmail);
            driver.FindElement(sendButtonLocator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLocator).Text.Contains("мальчика"), "указан другой пол");
        }

        public void selectGirlNames()
        {
            driver.Navigate().GoToUrl(siteUrl);
            driver.FindElement(radioButtonGirlLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(correctEmail);
            driver.FindElement(sendButtonLocator).Click();
            Assert.IsTrue(driver.FindElement(resultTextLocator).Text.Contains("девочки"), "указан другой пол");
        }

        public void incorrectEmail()
        {
            driver.Navigate().GoToUrl(siteUrl);
            driver.FindElement(emailInputLocator).SendKeys(wrongEmail);
            driver.FindElement(sendButtonLocator).Click();
            driver.FindElement(radioButtonGirlLocator).Click();
            Assert.IsTrue(driver.FindElements(errorTextLocator).Count == 0, "Принят некорректный email");
            Assert.AreEqual("Некорректный email", driver.FindElement(errorTextLocator).Text);
        }

        public void EnterEmptyEmail()
        {
            driver.Navigate().GoToUrl(siteUrl);
            driver.FindElement(sendButtonLocator).Click();
            Assert.IsTrue(driver.FindElements(errorTextLocator).Count == 0, "Принят пустой email");
            Assert.AreEqual("Введите email", driver.FindElement(errorTextLocator).Text);
        }

        public void ClickAnotherEmail()
        {
            driver.Navigate().GoToUrl(siteUrl);
            driver.FindElement(emailInputLocator).SendKeys(correctEmail);
            driver.FindElement(sendButtonLocator).Click();
            driver.FindElement(anotherEmailLinkLocator).Click();
            Assert.AreEqual(string.Empty, driver.FindElement(emailInputLocator).Text, "Поле почты не пустое после клика по ссылке выбрать другой email");
            Assert.IsFalse(driver.FindElement(anotherEmailLinkLocator).Displayed, "Ссылка не пропадает");
        }

        [TearDown]

        public void TearDown()
        {
            driver.Quit();
        }
    }
}
