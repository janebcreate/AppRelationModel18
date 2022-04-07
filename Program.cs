using AppRelationModel18;
namespace HelloApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // пересоздаем базу данных
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Company microsoft = new Company { Name = "Microsoft" };
                Company google = new Company { Name = "Google" };
                db.Companies.AddRange(microsoft, google);
                User tom = new User { Name = "Tom", Age = 36, Company = microsoft };
                User bob = new User { Name = "Bob", Age = 39, Company = google };
                User alice = new User { Name = "Alice", Age = 28, Company = microsoft };
                User kate = new User { Name = "Kate", Age = 25, Company = google };
                db.Users.AddRange(tom, bob, alice, kate);
                db.SaveChanges();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = from u in db.Users
                            orderby u.Name
                            select u;
                Console.WriteLine("Сортировка медодом Linq");
                foreach (User user in users)
                    Console.WriteLine($"{user.Id}.{user.Name} ({user.Age})");
            }


        }
    }
}