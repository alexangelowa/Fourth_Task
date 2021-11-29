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
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
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
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("search")));
            Assert.IsTrue(chromeDriver.Url.Contains("Child"));
            Assert.IsTrue(chromeDriver.Title.Contains(searchKeyword));
        }

        [Then(@"I verify that the first item has the title: Harry Potter and the Cursed Child - Parts One & Two")]
        public void ThenIVerifyThatTheFirstItemsHasTheTitleHarryPotterAndTheCursedChild_PartsOneTwo()
        {
            firstElement = chromeDriver.FindElement(By.CssSelector(".s-search-results:nth-of-type(2) .s-result-item:first-of-type"));
            var titleText = firstElement.FindElement(By.CssSelector(".a-size-medium"));
           
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
            var itemType = firstElement.FindElement(By.CssSelector(".s-search-results:nth-of-type(2) [data-cel-widget=\"search_result_0\"] .a-spacing-top-small .a-text-bold"));

            Assert.IsTrue(itemType.Text.Contains("Paperback"));
        }

        [Then(@"I verify the price is (.*)")]
        public void ThenIVerifyThePriceIs(string price)
        {
            this.itemPrice = price;
            var itemPrice = chromeDriver.FindElement(By.CssSelector("[data-cel-widget=\"search_result_0\"] .a-spacing-top-small [data-a-size=\"l\"] .a-offscreen"));

            Assert.IsTrue(itemPrice.GetAttribute("innerHTML").Equals(this.itemPrice));
        }

        [When(@"I click the link to the book details")]
        public void WhenIClickTheLinkToTheBookDetails()
        {
            var itemDetailsPageLink = chromeDriver.FindElement(By.CssSelector(".s-search-results:nth-of-type(2) [data-cel-widget=\"search_result_0\"] .a-size-medium.a-color-base.a-text-normal"));
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
            var itemBadge = chromeDriver.FindElement(By.CssSelector(".a-icon-addon.p13n-best-seller-badge"));

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
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("huc-v2-order-row-with-divider")));

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
            var confirmationAddedToBasket = chromeDriver.FindElement(By.CssSelector("#huc-v2-order-row-confirm-text .a-size-medium"));

            Assert.IsTrue(confirmationAddedToBasket.Text.Equals("Added to Basket"));
        }

        [Then(@"I check the quantity of the cart is one")]
        public void ThenICheckTheQuantityOfTheCartIsOne()
        {
            var quantityInBasket = chromeDriver.FindElement(By.CssSelector("#hlb-subcart"));

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
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-item-count=\"1\"] .sc-product-image")));
        }

        [Then(@"I verify that the book is shown on the list")]
        public void ThenIVerifyThatTheBookIsShownOnTheList()
        {
            var firstItemInBasket = chromeDriver.FindElement(By.CssSelector("[data-item-count=\"1\"]"));

            Assert.IsTrue(firstItemInBasket.Text.Contains(searchKeyword));
        }

        [Then(@"I verify that the title, type of print and price are the same as on the search page, quantity is (.*) and total price is same as item price")]
        public void ThenIVerifyThatTheTitleTypeOfPrintAndPriceAreTheSameAsOnTheSearchPageQuantityIsAndTotalPrice(int quantity)
        {
            var itemTitle = chromeDriver.FindElement(By.CssSelector("[data-item-count=\"1\"] .a-truncate-full"));
            var itemType = chromeDriver.FindElement(By.CssSelector("[data-item-count=\"1\"] .sc-product-binding"));
            var itemPrice = chromeDriver.FindElement(By.CssSelector("[data-item-count=\"1\"] .sc-product-price"));
            var itemQuantity = chromeDriver.FindElement(By.CssSelector("[data-item-count=\"1\"] .a-dropdown-prompt"));
            var totalPrice = chromeDriver.FindElement(By.CssSelector("[data-item-count=\"1\"] .sc-price"));

            string titleText = itemTitle.GetAttribute("innerHTML");
            string itemTypeText = itemType.GetAttribute("innerHTML");
            string itemPriceText = itemPrice.GetAttribute("innerHTML");
            string itemQuantityText = itemQuantity.GetAttribute("innerHTML");
            string totalPriceText = totalPrice.GetAttribute("innerHTML");

            Assert.IsTrue(titleText.Contains(searchKeyword));
            Assert.IsTrue(itemTypeText.Contains("Paperback"));
            Assert.IsTrue(itemPriceText.Equals(this.itemPrice));
            Assert.IsTrue(itemQuantityText.Equals(quantity.ToString()));
            Assert.IsTrue(totalPriceText.Equals(this.itemPrice));
        }
    }
}
