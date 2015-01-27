using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace WPSeleniumTests
{
    public class Driver
    {
        RemoteWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Create a new instance of the Firefox driver
            //driver = new FirefoxDriver();
            DesiredCapabilities capability = DesiredCapabilities.InternetExplorer();

            // Place target machine here. ( http://x.x.x.x:xxxx/wd/hub )
            driver = new RemoteWebDriver(new Uri("http://x.x.x.x:xxxx/wd/hub"), capability);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        [Test]
        public void GoogleSearch()
        {
            //Navigate to the site
            driver.Navigate().GoToUrl("http://www.google.com");
            // Find the text input element by its name
            driver.FindElement(By.Name("q")).SendKeys("Selenium");
            // Now submit the form
            driver.FindElement(By.Name("q")).Submit();
            // Google's search is rendered dynamically with JavaScript.
            // Wait for the page to load, timeout after 5 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.Title.StartsWith("Selenium"); });
            // Check that the Title is what we are expecting
            Assert.AreEqual("Selenium - Google-haku", driver.Title);

            // Take a screenshot
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile("ss.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        [Test]
        public void TheGoogleTest()
        {
            driver.Navigate().GoToUrl("http://google.com");
            driver.FindElement(By.Name("q")).SendKeys("Google");
            driver.FindElement(By.Name("q")).Submit();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.Title.StartsWith("Google"); });
        }
    }
}
