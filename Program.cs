using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections;

namespace SeleniumVideoDownloader
{
    class Program
    {
        static void Main(string[] args)
        {

            
           
            Console.WriteLine("Hello World!");
            List<string> urls = getLinkns();
            List<string> names = getFileNames();
            string[] filesInFolder = readFolder();
            Console.WriteLine(urls.Count);
            foreach (string url in urls)
            {
                Download(url);
            }

            foreach (string name in names)
            {

                string newName = name.Replace(" ", "");

                foreach (string file in filesInFolder)
                {
                    if (!file.Contains(name))
                    {
                        System.IO.File.WriteAllText(@"C:\Users\Mario\Downloads\filed.txt", "failed to download :" + name);
                    }

                }

            }




            Console.Read();

        }



        public static List<string> getLinkns()
        {

            List<string> urls = new List<string>();

            String url = "http://www.gryllus.net/Blender/VideoTutorials/AllVideoTutorials.html";
            IWebDriver driver;
            driver = new ChromeDriver(@"C:\Users\Mario\Documents\JavaLib");
            driver.Navigate().GoToUrl(url);
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("(//a[contains(@href,'vimeo')])"));

            // Xpath att @


            foreach (IWebElement element in elements)
            {
                var ur = element.GetAttribute("href");
                string newUrl = ur.Replace("http:", "https:");



                Console.WriteLine(newUrl);
                urls.Add(newUrl);

            }


            return urls;
        }


        public static string[] readFolder()
        {
            string[] files = System.IO.Directory.GetFiles(@"C:\Users\Mario\Downloads", "*.mp4");


            foreach (string file in files)
            {

                Console.WriteLine(file);


            }

            return files;

        }


        public static void Download(string url)
        {
            FirefoxProfile firefoxProfile = new FirefoxProfile();

            firefoxProfile.SetPreference("browser.download.dir", "c:\\downloads");
            firefoxProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "video/mp4");

            IWebDriver driver;
            string baseURL;
            // driver = new FirefoxDriver(firefoxProfile);
            driver = new ChromeDriver(@"C:\Users\Mario\Documents\JavaLib");
            baseURL = url;

            Console.WriteLine("current url :" + baseURL);

            driver.Navigate().GoToUrl(baseURL);


            Console.WriteLine("started");


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            Thread.Sleep(20000);
            wait.Until(ExpectedConditions.UrlToBe(baseURL));//breaks
            IWebElement myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(By.XPath("(//span[text()='Download'])")));

            if (myDynamicElement != null)
            {
                var newElement = wait.Until(ExpectedConditions.ElementToBeClickable(myDynamicElement));
                if (newElement != null)
                {
                    myDynamicElement.Click();
                    Console.WriteLine("Choose download click");
                }
            }

            Thread.Sleep(10000);
            Console.WriteLine("log in found");


            myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(By.XPath("(//a[text()='Download'])[2]")));

            if (myDynamicElement != null)
            {
                var newElement = wait.Until(ExpectedConditions.ElementToBeClickable(myDynamicElement));
                if (newElement != null)
                {
                    myDynamicElement.Click();
                    Console.WriteLine("click download");
                }
            }



            myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(By.CssSelector("button.iris_modal-btn--close")));


            if (myDynamicElement != null)
            {
                var newElement = wait.Until(ExpectedConditions.ElementToBeClickable(myDynamicElement));
                if (newElement != null)
                {
                    myDynamicElement.Click();
                    Console.WriteLine("passed closed the log in");
                }
            }



            Console.WriteLine("after the actual download");
            try
            {
                Thread.Sleep(40000);                 //1000 milliseconds is one second.
            }
            catch (ThreadInterruptedException ex)
            {
                Console.WriteLine("error found");
            }


            driver.Quit();
            Console.WriteLine("done");

        }

        public static List<string> getFileNames()
        {

            List<string> names = new List<string>();

            String url = "http://www.gryllus.net/Blender/VideoTutorials/AllVideoTutorials.html";
            IWebDriver driver;
            driver = new ChromeDriver(@"C:\Users\Mario\Documents\JavaLib");
            driver.Navigate().GoToUrl(url);
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("(//a[contains(@href,'vimeo')])"));

            // Xpath att @


            foreach (IWebElement element in elements)
            {
                var name = element.Text;
                



                Console.WriteLine(name);
                names.Add(name);

            }


            return names;
        }

    }






}
