using System.Threading.Tasks;
using Flux.Client;
using Flux.Flux.Options;
using NUnit.Framework;

namespace Flux.Tests.Flux
{
    public abstract class AbstractItFluxClientTest : AbstractTest
    {
        protected const string DatabaseName = "flux_database";

        protected FluxClient FluxClient;
        
        [SetUp]
        public new void SetUp()
        {
            SetUpAsync().Wait();            
        }

        async Task SetUpAsync()
        {
            string influxUrl = GetInfluxDbUrl();
            
            var options = new FluxConnectionOptions(influxUrl);
            
            FluxClient = FluxClientFactory.Create(options);
            
            await InfluxDbQuery("CREATE DATABASE " + DatabaseName, DatabaseName);
            
            await PrepareDara();            
        }

        [TearDown]
        protected void After() 
        {
            InfluxDbQuery("DROP DATABASE " + DatabaseName, DatabaseName).GetAwaiter().GetResult();
        }

        public abstract Task PrepareDara();
    }
}