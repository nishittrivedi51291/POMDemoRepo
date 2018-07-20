using NUnit.Framework;
using OpenQA.Selenium;
using POMDemo.PO.HomePO;
using RelevantCodes.ExtentReports;

namespace POMDemo.TC.HomeTC
{
    [TestFixture]
    public class HomeTC1 : BaseTest.BaseTest
    {
        public int flag = 1;

        public HomeTC1()
        {

        }

        public HomeTC1(IWebDriver driver, int Flag)
        {
            base.driver = driver;
            flag = Flag;
        }

        [Test]
        public void openAboutPage()
        {
            if(flag > 0)
            {
                test = extent.StartTest("Test 1: Open About Page");
            }

            HomePO homePO = new HomePO(driver);

            Assert.IsTrue(homePO.goToAboutPage(), "Unable to go to about page");
            test.Log(LogStatus.Pass, "Go to About Page.", "User should redirect to About Page.");
        }
    }
}