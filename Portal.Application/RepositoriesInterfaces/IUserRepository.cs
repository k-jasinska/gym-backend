using Portal.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application
{
    public interface IUserRepository
    {
        Task CreatePerson(Person p);
        void DeleteClients(Guid id);
        Task SetUserPassword(string id, string password);
    }
}
