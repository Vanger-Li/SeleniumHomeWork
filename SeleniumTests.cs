using NUnit.Framework;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SeleniumTests
{
    public class SeleniumTests
    {
        private ChromeDriver driver;
        private By emailInputLocator = By.Name("email");
        private By radioButtonBoyLocator = By.Id("boy");
        private By radioButtonGirlLocator = By.Id("girl");
        private By sendButtonLocator = By.Id("sendMe");
        private By emailResultTextLocator = By.Name("result-text");
        private By anotherEmailLinkLocator = By.LinkText("указать другой e-mail");

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [Test]

        public void getBoysNames()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys("test@email.ru");
            driver.FindElement(sendButtonLocator).Click();
            driver.FindElement(radioButtonGirlLocator).Click();
            Assert.AreEqual("test@email.ru", driver.FindElement(emailResultTextLocator).Text);
        }

        public void getGirlNames()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys("test@email.ru");
            driver.FindElement(sendButtonLocator).Click();
            driver.FindElement(radioButtonGirlLocator).Click();
            Assert.AreEqual("test@email.ru", driver.FindElement(emailResultTextLocator).Text);
        }

        public void uncorrectEmail()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys("te∞#¢#€¢∞");
            driver.FindElement(sendButtonLocator).Click();
            driver.FindElement(radioButtonGirlLocator).Click();
            Assert.AreEqual("te∞#¢#€¢∞", driver.FindElement(emailResultTextLocator).Text);
        }

        public void EnterEmail()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(sendButtonLocator).Click();
            driver.FindElement(radioButtonGirlLocator).Click();
            Assert.AreEqual("", driver.FindElement(emailResultTextLocator).Text);
        }

        public void EnterVeryLoongEmail()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(sendButtonLocator).Click();
            driver.FindElement(radioButtonGirlLocator).Click();
            Assert.AreEqual("te∞#¢#€¢∞", driver.FindElement(emailResultTextLocator).Text);
        }

        [TearDown]

        public void TearDown()
        {
        }
    }
}
