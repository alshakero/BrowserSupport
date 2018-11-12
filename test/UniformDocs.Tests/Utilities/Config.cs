using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BrowserSupport.Tests.Utilities
{
    public class Config
    {
        public enum Browser
        {
            Chrome,
            Edge,
            Firefox
        }
        
        public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(60);
        public static readonly string LocalIP = Dns.GetHostEntry(Dns.GetHostName())
   .AddressList.First(
       f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
   .ToString();

        public static readonly Uri BrowserSupportUrl = new Uri($"http://{LocalIP}:8080/Main");
        public static readonly Uri RemoteWebDriverUri = new Uri("http://hub-cloud.browserstack.com/wd/hub/");
        public static readonly string BrowserstackUsername = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
        public static readonly string BrowserstackAccessKey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
        public static readonly string BrowserstackLocalIdentifier = Environment.GetEnvironmentVariable("COMPUTERNAME");
    }
}