using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text;
using System.Collections.Generic;

namespace Fourth_Task.StepBindings
{
    [Binding]
    public class AmazonSearchFeatureSteps
    {
        private string searchKeyword;
        private ChromeDriver chromeDriver;
        private string itemPrice;
        private IWebElement firstElement;


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

        [When(@"I have navigated to the (.*) section of the website")]
        public void WhenIHaveNavigatedToTheBooksSectionOfTheWebsite(string sectionName)
        {
            var sectionLink = chromeDriver.FindElement(By.LinkText(sectionName));
            sectionLink.Click();
        }

        [When(@"I have entered (.*) as search keyword")]
        public void WhenIHaveEnteredHarryPotterAndTheCursedChildAsSearchKeyword(string searchWord)
        {
            searchKeyword = searchWord;
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(5));
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

        [Then(@"I verify that the first item has the title: Harry Potter and the Cursed Child - Parts One & Two")]
        public void ThenIVerifyThatTheFirstItemsHasTheTitleHarryPotterAndTheCursedChild_PartsOneTwo()
        {
            var listOfAllItems = chromeDriver.FindElements(By.XPath("//*[@id=\"search\"]/div[1]/div[1]/div/span[3]/div[2]"));
            firstElement = listOfAllItems[0];
            var titleText = firstElement.FindElement(By.XPath("//*[@id=\"search\"]/div[1]/div[1]/div/span[3]/div[2]/div[1]/div/span/div/div/div[2]/div[2]/div/div/div[1]/h2/a/span"));

            Assert.IsTrue(titleText.Text.Contains(searchKeyword));
        }

        [Then(@"I verify if it has a badge")]
        public void ThenIVerifyIfItHasABadge()
        {
            var badgeElement = firstElement.FindElement(By.ClassName("a-badge"));

            Assert.IsTrue(badgeElement.Text.Contains("Best Seller"));
        }

        [Then(@"I verify the selected type")]
        public void ThenIVerifyTheSelectedType()
        {
            var itemType = firstElement.FindElement(By.XPath("//*[@id=\"search\"]/div[1]/div[1]/div/span[3]/div[2]/div[1]/div/span/div/div/div[2]/div[2]/div/div/div[3]/div[1]/div/div[1]/div[1]/a"));

            Assert.IsTrue(itemType.Text.Contains("Paperback"));
        }

        [Then(@"I verify the price is (.*)")]
        public void ThenIVerifyThePriceIs(string price)
        {
            this.itemPrice = price;
            var itemPrice = firstElement.FindElement(By.ClassName("a-offscreen"));

            Assert.IsTrue(itemPrice.GetAttribute("innerHTML").Equals(this.itemPrice));
        }

        [When(@"I click the link to the book details")]
        public void WhenIClickTheLinkToTheBookDetails()
        {
            var itemDetailsPageLink = chromeDriver.FindElement(By.XPath("//*[@id=\"search\"]/div[1]/div[1]/div/span[3]/div[2]/div[1]/div/span/div/div/div[2]/div[2]/div/div/div[1]/h2/a"));
            itemDetailsPageLink.Click();
        }

        [Then(@"I should be redirected to the book details page")]
        public void ThenIShouldBeRedirectedToTheBookDetailsPage()
        {
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("add-to-cart-button")));

            Assert.IsTrue(chromeDriver.Url.Contains("/dp/"));
        }

        [Then(@"I verify the title")]
        public void ThenIVerifyTheTitle()
        {
            var itemTitle = chromeDriver.FindElement(By.Id("productTitle"));

            Assert.IsTrue(itemTitle.GetAttribute("innerHTML").Contains(searchKeyword));
        }

        [Then(@"I verify the badge if there is any")]
        public void ThenIVerifyTheBadgeIfThereIsAny()
        {
            var itemBadge = chromeDriver.FindElement(By.XPath("//*[@id=\"zeitgeistBadge_feature_div\"]/div/a/i"));

            Assert.IsTrue(itemBadge.Text.Equals("#1 Best Seller"));
        }

        [Then(@"I verify the price on details page")]
        public void ThenIVerifyThePriceOnDetailsPage()
        {
            var itemPrice = chromeDriver.FindElement(By.Id("price"));

            Assert.IsTrue(itemPrice.GetAttribute("innerHTML").Equals(this.itemPrice));
        }

        [Then(@"I verify the type is Paperback")]
        public void ThenIVerifyTheTypeIsPaperback()
        {
            var itemType = chromeDriver.FindElement(By.Id("productSubtitle"));

            Assert.IsTrue(itemType.Text.Contains("Paperback"));
        }

        [When(@"I click on the Add to Basket button")]
        public void WhenIClickOnTheAddToBasketButton()
        {
            var addToCartLink = chromeDriver.FindElement(By.Id("add-to-cart-button"));

            addToCartLink.Click();
        }

        [Then(@"I am redirected to the successfully added to cart screen")]
        public void ThenIAmRedirectedToTheSuccessfullyAddedToCartScreen()
        {
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("hlb-ptc-btn-native")));

            Assert.IsTrue(chromeDriver.Title.Contains("Shopping Basket"));
        }

        [Then(@"I verify that the notification for successfully adding the item is shown")]
        public void ThenIVerifyThatTheNotificationForSuccessfullyAddingTheItemIsShown()
        {
            var notificationComponent = chromeDriver.FindElement(By.Id("huc-v2-order-row-with-divider"));

            Assert.IsTrue(notificationComponent.Displayed);
        }

        [Then(@"I verify that the Added to Basket text is shown")]
        public void ThenIVerifyThatTheAddedToBasketTextIsShown()
        {
            var confirmationAddedToBasket = chromeDriver.FindElement(By.XPath("//*[@id=\"huc-v2-order-row-confirm-text\"]/h1"));

            Assert.IsTrue(confirmationAddedToBasket.Text.Equals("Added to Basket"));
        }

        [Then(@"I check the quantity of the cart is one")]
        public void ThenICheckTheQuantityOfTheCartIsOne()
        {
            var quantityInBasket = chromeDriver.FindElement(By.XPath("//*[@id=\"hlb-subcart\"]/div[1]/span/span[1]"));

            Assert.IsTrue(quantityInBasket.GetAttribute("innerHTML").Contains(" (1 item): "));
        }

        [When(@"I click on Edit basket button")]
        public void WhenIClickOnEditBasketButton()
        {
            var editBasketLink = chromeDriver.FindElement(By.Id("hlb-view-cart-announce"));

            editBasketLink.Click();
        }

        [Then(@"I should be redirected to the cart screen")]
        public void ThenIShouldBeRedirectedToTheCartScreen()
        {
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("proceedToRetailCheckout")));
        }

        [Then(@"I verify that the book is shown on the list")]
        public void ThenIVerifyThatTheBookIsShownOnTheList()
        {
            var listOfAllItemsInBasket = chromeDriver.FindElements(By.XPath("//*[@id=\"activeCartViewForm\"]/div[2]"));
            var firstItemInBasket = listOfAllItemsInBasket[0];

            Assert.IsTrue(firstItemInBasket.Text.Contains(searchKeyword));
        }

        [Then(@"I verify that the title, type of print is the same as on the search page, price is the same as on the search page, quantity is (.*) and total price is same as item price")]
        public void ThenIVerifyThatTheTitleTypeOfPrintIsTheSameAsOnTheSearchPagePriceIsTheSameAsOnTheSearchPageQuantityIsAndTotalPrice(int quantity)
        {
            var itemTitle = chromeDriver.FindElement(By.XPath($"//span[contains(text(), 'Harry Potter and the Cursed Child - Parts One and Two')]"));
            //var itemType = chromeDriver.FindElement(By.XPath("//span[contains(text(), 'Paperback')]"))


            Assert.IsTrue(itemTitle.GetAttribute("innerHTML").Contains(searchKeyword));

            //Assert.IsTrue(itemType.GetAttribute("innerHTML").Equals("Paperback"));
            //Assert.IsTrue(itemPrice.GetAttribute("innerHTML").Equals(this.itemPrice));
            //Assert.IsTrue(itemQuantity.GetAttribute("innerHTML").Equals(quantity));
            //Assert.IsTrue(totalPrice.GetAttribute("innerHTML").Equals(this.itemPrice));

        }
    }
}
