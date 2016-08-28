namespace Scheduler.API.IntegrationTests
{
    public class StaticCache : StaticCacheBase
    {
        public static string ServiceHostStem = GetApplicationSetting<string>("ServiceHostStem");

        public static string ServiceHostScheme = GetApplicationSetting<string>("ServiceHostScheme");

        public static string ApplicationUserName = GetApplicationSetting<string>("ApplicationUserName");

        public static string ApplicationPassword = GetApplicationSetting<string>("ApplicationPassword");
    }
}
