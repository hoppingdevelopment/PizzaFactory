using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace PizzaFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("factoryConfig.json").Build();

            var section = config.GetSection(nameof(PizzaFactoryConfig));
            var factoryConfig = section.Get<PizzaFactoryConfig>();
            Console.WriteLine(DateTime.Now + " Pizza Factory Starting.....");
            
            for(var i = 1; i < factoryConfig.NumberOfPizzas; i++)
            {
                Pizza pizza = PizzaFactory.RandomPizza(factoryConfig);
                int cookTime = PizzaFactory.CalculatedCookingTime(factoryConfig.BaseCookingTime, pizza.BaseCookingTimeMultiplier, pizza.Topping);

                Thread.Sleep(cookTime);
                Console.WriteLine(DateTime.Now + " Pizza cooked: " + pizza.Base + " with " + pizza.Topping + "(" + " - Cooking Time (seconds): " + (cookTime / 1000).ToString("#.##") + ")");
            }

            Console.WriteLine(DateTime.Now + " Pizza Factory Finishing.....");

        }
    }
}
