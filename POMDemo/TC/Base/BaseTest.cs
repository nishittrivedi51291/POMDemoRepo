using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RelevantCodes.ExtentReports;
using System;
using System.IO;

namespace POMDemo.TC.BaseTest
{
    [TestFixture]
    public class BaseTest
    {
        public ExtentReports extent;
        public ExtentTest test;
        public IWebDriver driver = null;

        public BaseTest()
        {

        }

        [OneTimeSetUp]
        public void BeforeClass()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\ExtentScreenshot.html";
            extent = new ExtentReports(reportPath, true);

            driver = new ChromeDriver(@"C:\Users\P1059\Downloads\chromedriver_win32\");
            this.driver.Manage().Window.Maximize();
            this.driver.Navigate().GoToUrl("https://www.swtestacademy.com");
        }

        [SetUp]
        public void BeforeMethod()
        {
            this.driver.Navigate().Refresh();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [TearDown]
        public void AfterMethod()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                string screenShotPath = GetScreenShot.Capture(driver, "ScreenShotName");
                test.Log(LogStatus.Fail, stackTrace + errorMessage);
                test.Log(LogStatus.Fail, "Snapshot below: " + test.AddScreenCapture(screenShotPath));
            }
            extent.EndTest(test);
        }

        [OneTimeTearDown]
        public void Endreport()
        {
            extent.Flush();
            extent.Close();
            driver.Quit();
        }
    }

    public class GetScreenShot
    {
        public static string Capture(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();

            var pathSample = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string currentDirectory = Path.Combine(pathSample.Substring(0, pathSample.IndexOf("bin")), "Reports");
            string finalpth = Path.Combine(currentDirectory, "ErrorScreenshots");                //pth.Substring(0, pth.LastIndexOf("bin")) + "ErrorScreenshots\\" + screenShotName + ".png";
            string fileName = screenShotName + "_" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "_") + "_" + DateTime.Now.Millisecond + ".png";           //string localpath = new Uri(finalpth).LocalPath;
            if (!Directory.Exists(finalpth))
            {
                Directory.CreateDirectory(finalpth);
            }
            Directory.SetCurrentDirectory(finalpth);
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
            return Path.Combine(finalpth, fileName);
        }
    }
}
