// This is a sample Microsoft Test for Visual Studio using the Selenium browser test tool. 
// REQUIRED: ADD YOUR EMAIL ADDRESS

// Requires NuGet package "Selenium.WebDriver"
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace HeadlampSeleniumTest
{
    [TestClass]
    public class Home
    {
        // [REQUIRED] Add "automatedby" with your email address to the query string
        private static readonly string MyEmailAddress = ""; // <== Put your email address here
        private static readonly string TestUrl = "https://headlamptest.com/TestPage.html?headlamp-automatedby=" + MyEmailAddress;
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
            Thread.Sleep(5000);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Run this cleanup between tests.
            Driver.Navigate().GoToUrl(TestUrl);
            Driver.FindElement(By.Id("fname")).SendKeys("Test Complete!");
        }

        [TestMethod]
        public void Test1()
        {
            // Fill in the test form

            // First Name
            Driver.FindElement(By.Id("fname")).SendKeys("Headlamp Test");

            // Last Name
            Driver.FindElement(By.Id("lname")).SendKeys("Automation Gap Analysis");

            // Submit the form data
            Driver.FindElement(By.XPath("/html/body/form/input[3]")).Click();
        }
    }
}
