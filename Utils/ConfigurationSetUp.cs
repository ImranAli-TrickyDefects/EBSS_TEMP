using Microsoft.Extensions.Configuration;
using System;

namespace EBSS.Automated.UI.Tests.Utils
{
    public class ConfigurationSetUp
    {
        
        private static IConfiguration config = GetConfigurations();
        public static string BaseUrl = config.GetSection("Browser").GetSection("BaseUrl").Value;
        public static readonly string BrowserType = config.GetSection("Browser").GetSection("Type").Value;
        public static readonly string Headless = "false";
        public static readonly string IsDesktop = "true";
        public static readonly string DeviceName = "";
        public static readonly string AdminEmail = config.GetSection("AdminUser").GetSection("UserEmail").Value;
        public static readonly string AdminPassword = config.GetSection("AdminUser").GetSection("Password").Value;
        //    public static readonly string Access = Environment.GetEnvironmentVariable("AdminUser.AccessToken");
        //   public static readonly string RequestToken = Environment.GetEnvironmentVariable("AdminUser:RequestToken");
        public static readonly string TestEmail = config.GetSection("TestUser").GetSection("UserEmail").Value;
        public static readonly string TestPassword = config.GetSection("TestUser").GetSection("Password").Value;

        private static IConfiguration GetConfigurations()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
    }
}
