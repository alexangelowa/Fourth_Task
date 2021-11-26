using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Fourth_Task.StepBindings
{
    [Binding]
    public class AmazonSearchFeatureSteps : IDisposable
    {
        private string searchKeyword;
        private ChromeDriver chromeDriver;

        public AmazonSearchFeatureSteps()
        {
            chromeDriver = new ChromeDriver();
        }


        [Given(@"I have navigated to Amazon website")]
        public void GivenIHaveNavigatedToAmazonWebsite()
        {
            chromeDriver.Navigate().GoToUrl("https://www.amazon.co.uk");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("amazon"));
        }

        [Given(@"I have accepted the Cookie Policy")]
        public void GivenIHaveAcceptedTheCookiePolicy()
        {
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sp-cc-accept")));
            var agreeToCookiePolicyButton = chromeDriver.FindElement(By.Id("sp-cc-accept"));
            agreeToCookiePolicyButton.Click();
        }

        [Given(@"I have entered (.*) as search keyword")]
        public void GivenIHaveEnteredAdeleAsSearchKeyword(string searchWord)
        {
            searchKeyword = searchWord;
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("twotabsearchtextbox")));
            var searchBox = chromeDriver.FindElement(By.Id("twotabsearchtextbox"));
            searchBox.SendKeys(searchKeyword);
        }

        [When(@"I press the search button")]
        public void WhenIPressTheSearchButton()
        {
            var searchButton = chromeDriver.FindElement(By.Id("nav-search-submit-button"));
            searchButton.Click();
        }

        [Then(@"I should be redirected to the search results page")]
        public void ThenIShouldBeRedirectedToTheSearchResultsPage()
        {
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("reviewsRefinements")));
            Assert.IsTrue(chromeDriver.Url.Contains("Child"));
            Assert.IsTrue(chromeDriver.Title.Contains(searchKeyword));
        }

        public void Dispose()
        {
            if (chromeDriver != null)
            {
                chromeDriver.Dispose();
                chromeDriver = null;
            }
        }
    }
}
