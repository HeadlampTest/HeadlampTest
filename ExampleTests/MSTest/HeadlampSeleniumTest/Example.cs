// This is a sample Microsoft Test for Visual Studio using the Selenium browser test tool. 

// REQUIRED: ADD YOUR EMAIL ADDRESS - see below
// REQUIRED NuGet package "Selenium.WebDriver"
// REQUIRED You need to download the version of chromedriver that matches your version of Chrome
//              and is the correct one for your system (Mac/Linux/Windows) and place it in the same folder as this test.

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace HeadlampSeleniumTest
{
    [TestClass]
    public class Home
    {
        private static readonly string MyEmailAddress = "dgarson425@gmail.com"; // <== Put your email address here
        private static readonly string TestUrl = "https://headlamptest.com/test?headlamp-automatedby=" + MyEmailAddress;
        private static IWebDriver Driver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {          
            if (string.IsNullOrEmpty(MyEmailAddress))
            {
                Assert.Fail("Please specify your email address.");
            }

            // Set up the Chrome Driver
            ChromeOptions options = new ChromeOptions();
            options.AddExtension(@"HeadlampChromeExtension.crx"); // Include the Headlamp Chrome Extension
            Driver = new ChromeDriver(options);

            Reset(); // Reset Headlamp for a new run
        }

        private static void Reset()
        {
            // Add "reset=1" to the query string so that each run can be measured individually.
            Driver.Navigate().GoToUrl(TestUrl + "&headlamp-reset=1");

            // Wait for Reset to complete.
            Thread.Sleep(5000); // NOTE: OK, I'm not sure about this Sleep. Do we still need this with the latest Headlamp?
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Run this cleanup between tests.
            Driver.Navigate().GoToUrl(TestUrl);
            Driver.FindElement(By.Id("fname")).SendKeys("Test");
            Driver.FindElement(By.Id("lname")).SendKeys("Complete!");
        }

        [TestMethod]
        public void Test1()
        {
            // Fill in the test form

            Thread.Sleep(2000); // This is a demo, so we are using lots of Sleeps to make sure the humans can see what's happening when it runs.

            // First Name
            Driver.FindElement(By.Id("fname")).SendKeys("Watch");

            Thread.Sleep(2000); // Going VERY slowly so the humans can see what's happening.
            
            // Last Name
            Driver.FindElement(By.Id("lname")).SendKeys("this...");

            Thread.Sleep(2000); // Going VERY slowly so the humans can see what's happening.

            // Submit the form data
            Driver.FindElement(By.Id("save")).Click();
        }
    }
}
