using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using ShoppingBasket.Utilities;
using ShoppingBasketTestApp.Utilities;
using System;

namespace ShoppingBasketTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var shoppingBasket = serviceProvider.GetService<ShoppingBasketLib.ShoppingBasket>();
            shoppingBasket.Initialize(GenerateProducts.GetProducts(), GenerateDiscounts.GetDiscounts());

            //first check
            shoppingBasket.AddToBasket(1, 1);
            shoppingBasket.AddToBasket(2, 1);
            shoppingBasket.AddToBasket(3, 1);

            //secondCheck
            shoppingBasket.AddToBasket(2, 2);
            shoppingBasket.AddToBasket(2, 2);
            shoppingBasket.AddToBasket(3, 2);
            shoppingBasket.AddToBasket(3, 2);

            //thirdCheck
            shoppingBasket.AddToBasket(1, 3);
            shoppingBasket.AddToBasket(1, 3);
            shoppingBasket.AddToBasket(1, 3);
            shoppingBasket.AddToBasket(1, 3);

            //forthCheck
            shoppingBasket.AddToBasket(2, 4);
            shoppingBasket.AddToBasket(2, 4);
            shoppingBasket.AddToBasket(3, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);
            shoppingBasket.AddToBasket(1, 4);


            var afterAddBasket = shoppingBasket.GetCurrentUserBasket(1);
            var afterAddBasketSecondUser = shoppingBasket.GetCurrentUserBasket(2);
            var afterAddBasketThirdUser = shoppingBasket.GetCurrentUserBasket(3);
            var afterAddBasketTForthUser = shoppingBasket.GetCurrentUserBasket(4);

            Console.WriteLine(shoppingBasket.GetBasketStatusTxtForUser(4));
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
            }).Configure<LoggerFilterOptions>(options =>
            {
                options.AddFilter<DebugLoggerProvider>(null, LogLevel.Information);
                options.AddFilter<ConsoleLoggerProvider>(null, LogLevel.Error);
            }).AddTransient<ShoppingBasketLib.ShoppingBasket>();
        }
    }
}
