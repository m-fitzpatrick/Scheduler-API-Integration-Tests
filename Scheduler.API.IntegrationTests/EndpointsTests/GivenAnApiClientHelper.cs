using System;
using NUnit.Framework;
using Scheduler.API.IntegrationTests.ApiClient;

namespace Scheduler.API.IntegrationTests.EndpointsTests
{
    public abstract class GivenAnApiClientHelper 
    {
        protected ApiClientHelper ApiClientHelper;

        [SetUp]
        public void SetUp()
        {
            Arrange();
        }

        [TearDown]
        protected void TearDown()
        {
            CleanUp();
        }

        protected virtual void Arrange()
        {
            UriBuilder uri = new UriBuilder(StaticCache.ServiceHostScheme, StaticCache.ServiceHostStem);
            TokenHelper tokenHelper = new TokenHelper(uri.ToString());
            string token = tokenHelper.GetToken(StaticCache.ApplicationUserName, StaticCache.ApplicationPassword).Result;

            ApiClientHelper = new ApiClientHelper(uri.ToString(), token);
        }

        protected virtual void CleanUp()
        {
        }
    }
}
