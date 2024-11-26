using Microsoft.Extensions.Configuration;

namespace TodoMVC.Utilities
{
    public static class ConfigUtil
    {
        public static IConfiguration Configuration { get; set; }

        static ConfigUtil()
        {
            string appSettingPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "TodoMVCTestAutomation");
            
            // Build configuration from appsettings.json
            Configuration = new ConfigurationBuilder()
                .SetBasePath(appSettingPath)  // Get the current directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load the config file
                .Build();          
        }

        public static string GetBaseUrl()
        {
            return Configuration["BaseUrl"];  
        }

        public static string GetBrowser()
        {
            return Configuration["Browser"];  
        }

        public static string GetPageLoadTimeout()
        {
            return Configuration["PageLoadTimeout"];  
        }
    }

}
