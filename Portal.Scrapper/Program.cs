using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Models;
using Portal.Infrastructure;
using Portal.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Portal.Scrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<Context> b = new DbContextOptionsBuilder<Context>();
            b.UseNpgsql("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=Gym;Keepalive=600");
            Context c = new Context(b.Options);

            UserRepository x = new UserRepository();
            ClientRepository r = new ClientRepository(c);
           
        LoadDatabase(r,x);
            Console.ReadLine();
        }

        private static async void LoadDatabase(ClientRepository r, UserRepository x)
        {
            var users = await GetUsers();
            foreach(var u in users)
            {
                await x.CreatePerson(u);
                r.Save(u);
            }   
        }

        private static async Task<List<Client>> GetUsers()
        {
            var urlPerson = "https://www.uw.edu.pl/uniwersytet/wladze-i-administracja/";
            var httpClient = new HttpClient();
            var htmlPerson = await httpClient.GetStringAsync(urlPerson);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlPerson);

            var table = htmlDocument.DocumentNode.Descendants("tbody").ToList();
            List<Client> list = new List<Client>();

            var personData = table[0].Descendants("tr").ToList();
            foreach (var item in personData)
            {
                var getEmail = item.Descendants("p").Skip(1).Take(1).First();
                var email = getEmail.InnerHtml.ToString().Replace("(at)", "@");
                var getRow = item.Descendants("td").First();
                var title = getRow.InnerHtml.ToString().Split("\n").First();
                List<string> titleSplit = title.Split(" ").ToList();
                List<string> name = titleSplit.TakeLast(2).ToList();

                Client client = new Client()
                {
                    Name = name[0],
                    Surname = name[1],
                    Login = name[0],
                    Email = email,
                    Role = "Client"
                };
                client.Password = client.Login;
                list.Add(client);
            };
            return list; 
        }
    }
}
