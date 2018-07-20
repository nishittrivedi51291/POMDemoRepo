using OpenQA.Selenium;

namespace POMDemo.PO.CommonPO
{
    public class BasePage
    {
        public IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}