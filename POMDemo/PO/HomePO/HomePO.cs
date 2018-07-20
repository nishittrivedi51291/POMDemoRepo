using OpenQA.Selenium;
using POMDemo.PO.CommonPO;

namespace POMDemo.PO.HomePO
{
    public class HomePO : BasePage
    {        
        public HomePO(IWebDriver driver) : base(driver)
        {
            base.driver = driver;
        }
               
        public By about_xPath = By.XPath("//li[@role='menuitem']//span[text()='About']");
        

        public bool goToAboutPage()
        {
            IWebElement about_WebL = driver.FindElement(about_xPath);
            about_WebL.Click();

            string currentURL = driver.Url.ToString();
            if (currentURL.Contains("about"))
            {
                return true;
            }
            else return false;
        }
    }
}
