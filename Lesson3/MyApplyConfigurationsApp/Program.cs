using System;
using System.Linq;

namespace MyApplyConfigurationsApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var contex = new MessageContextDB())
            {
                var userFrom1 = new User() { Name = "Ann" };
                var userFrom2 = new User() { Name = "Tom" };

                var toUser1 = new User() { Name = "Kate" };
                var toUser2 = new User() { Name = "Johne" };
                var toUser3 = new User() { Name = "Ron" };
                var toUser4 = new User() { Name = "Jessika" };

                userFrom1.UserFromMessages.AddRange(new Message[]
                {
                    new Message { ToUser = toUser1, Text = "Hi Kate!" },
                    new Message { ToUser = toUser3, Text = "Hi Ron!" }
                });

                userFrom2.UserFromMessages.AddRange(new Message[]
                {
                    new Message { ToUser = toUser2, Text = "Hi Johne!" },
                    new Message { ToUser = toUser4, Text = "Hi Jessika!" },
                    new Message { ToUser = toUser1, Text = "Hi Kate!" } }
                );

                contex.Users.AddRange(userFrom1, userFrom2);
                contex.SaveChanges();

                // Выведем всех отправителей и их получателей
                var userFromResult = contex.Users.Where(u => u.UserFromMessages.Any()).ToList();

                foreach (var u in userFromResult)
                {
                    Console.WriteLine($"\nFROM {u.Name}: ");

                    var toUsers = u.UserFromMessages.Select(m => m.ToUser).ToList();

                    foreach (var toUser in toUsers)
                    {
                        Console.WriteLine(new string(' ', 5) + $"TO {toUser.Name}");
                    }
                }

                Console.WriteLine();

                // Выведем всех получателей и их отправителей
                var toUserResult = contex.Users.Where(u => u.ToUserMessages.Any()).ToList();

                foreach (var u in toUserResult)
                {
                    Console.WriteLine($"\nTO {u.Name}: ");

                    var usersFrom = u.ToUserMessages.Select(m => m.UserFrom).ToList();

                    foreach (var userFrom in usersFrom)
                    {
                        Console.WriteLine(new string(' ', 5) + $"FROM {userFrom.Name}");
                    }
                }
            }
        }
    }
}
