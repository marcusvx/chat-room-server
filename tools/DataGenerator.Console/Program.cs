using Microsoft.Extensions.Configuration;

namespace DataGenerator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                             .AddEnvironmentVariables()
                             .Build();

            DbSeed dbSeed = new DbSeed(config);
            dbSeed.Generate();
        }

    }

}

