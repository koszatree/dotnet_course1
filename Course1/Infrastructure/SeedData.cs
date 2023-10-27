using Course1.Models;
using Microsoft.EntityFrameworkCore;

namespace Course1.Infrastructure
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();

            if (context.Products.Count() == 0 && context.Categories.Count() == 0)
            {
                Category fruits = new Category("fruits");
                Category shirts = new Category("shirts");

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Apples",
                        Price = 1.50M,
                        Category = fruits
                    },
                    new Product
                    {
                        Name = "Lemons",
                        Price = 2M,
                        Category = fruits
                    },
                    new Product
                    {
                        Name = "Watermelon",
                        Price = 0.50M,
                        Category = fruits
                    },
                    new Product
                    {
                        Name = "Grapepfruit",
                        Price = 2.50M,
                        Category = fruits
                    },
                    new Product
                    {
                        Name = "Blue shirt",
                        Price = 5.99M,
                        Category = shirts
                    },
                    new Product
                    {
                        Name = "Black shirt",
                        Price = 7.99M,
                        Category = shirts
                    },
                    new Product
                    {
                        Name = "Red shirt",
                        Price = 9.99M,
                        Category = shirts
                    },
                    new Product
                    {
                        Name = "Yellow shirt",
                        Price = 11.99M,
                        Category = shirts
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
