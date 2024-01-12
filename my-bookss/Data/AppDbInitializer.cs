using my_bookss.Data.Models;

namespace my_bookss.Data
{
    public class AppDbInitializer
    {

        //use this method to add data to db if it is empty

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "first book title",
                        Description = "first description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Biography",
                        CoverUrl = "Https...",
                        DateAdded = DateTime.Now
                    },
                    new Book()
                    {
                        Title = "second book title",
                        Description = "second description",
                        IsRead = false,
                        Genre = "Biography",
                        CoverUrl = "Https...",
                        DateAdded = DateTime.Now
                    });

                    context.SaveChanges();

                }
            }
        }
    }
}  
