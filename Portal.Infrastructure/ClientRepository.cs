using Microsoft.EntityFrameworkCore;
using Portal.Application;
using Portal.Application.Helpers;
using Portal.Application.Models;
using Portal.Application.Models.Dto;
using Portal.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Infrastructure
{
    public class ClientRepository : IClientQueryRepository, IClientRepository
    {
        private readonly Context _context;

        public ClientRepository(Context context)
        {
            _context = context;
        }

        private int TotalCount()
        {
            var result = _context.Clients.Count();
            return result;
        }

        public PagedResultDto<ClientWithStatus> Get(SearchCriteria criteria)
        {
            var list = _context.Clients.
                OrderBy(x => x.ProfileComplete)
                .Skip(criteria.RowsPerPage * (criteria.Page - 1))
                .Take(criteria.RowsPerPage)
                .ToList();

            List<ClientWithStatus> resultList = new List<ClientWithStatus>();

            foreach (var item in list)
            {
                bool exist = false;
                bool active = false;
                var carnet = GetCarnet(item.PersonID);
                if (carnet != null)
                {
                    exist = true;
                    active = carnet.IsActive;
                }
                ClientWithStatus el = new ClientWithStatus
                {
                    PersonID = item.PersonID,
                    Name = item.Name,
                    Surname = item.Surname,
                    Address = item.Address,
                    ContactData = item.ContactData,
                    Login = item.Login,
                    Email = item.Email,
                    ProfileComplete = item.ProfileComplete,
                    Exist = exist,
                    IsActive = active
                };
                resultList.Add(el);
            }


            PagedResultDto<ClientWithStatus> result = new PagedResultDto<ClientWithStatus>()
            {
                Items = resultList,
                Page = criteria.Page,
                RowsPerPage = criteria.RowsPerPage,
                TotalCount = TotalCount()
            };
            return result;
        }

        public PagedResultDto<Client> Get(SearchCriteria criteria, string search)
        {
            var list = _context.Clients.Skip(criteria.RowsPerPage * (criteria.Page - 1)).Take(criteria.RowsPerPage).Where(x => x.Name.Contains(search) || x.Surname.Contains(search)).ToList();
            PagedResultDto<Client> result = new PagedResultDto<Client>()
            {
                Items = list,
                Page = criteria.Page,
                RowsPerPage = criteria.RowsPerPage,
                TotalCount = TotalCount()
            };
            return result;
        }

        public void Save(Client p)
        {
            _context.Add(p);
            _context.SaveChanges();
        }

        public bool CheckLogin(string login)
        {
            return _context.Clients.Any(x => x.Login == login);
        }

        public string checkRole(Guid id)
        {
            const string client = "Client";
            const string trainer = "Trainer";
            const string admin = "Admin";

            bool clientRole = _context.Clients.Any(x => x.PersonID == id);
            if (clientRole) return client;

            bool doctorRole = _context.Trainers.Any(x => x.PersonID == id);
            if (doctorRole) return trainer;

            bool adminRole = _context.People.Any(x => x.PersonID == id);
            if (adminRole) return admin;

            return "";

        }

        public void DeleteClients(Guid id)
        {
            Client remove = _context.Clients.Where(x => x.PersonID == id).FirstOrDefault();
            _context.Remove(remove);
            _context.SaveChanges();
        }

        public Client GetDetails(Guid id)
        {
            var det = _context.Clients
                .Include(x => x.Carnets)
                .ThenInclude(y => y.Type)
                .Where(x => x.PersonID == id)
                .FirstOrDefault();
            return det;
        }

        public List<Training> GetGroupTraining(Guid id)
        {
            var list = _context.Training
                .Include(x => x.Trainer)
                .Include(x => x.PersonTraining)
                .Where(x => x.Type == TrainingType.Group && x.Start > DateTime.Now)
                .Where(x=> x.PersonTraining.All(y=>y.PersonID!=id))
                .OrderByDescending(x => x.Start)
                .ToList();


            return list;
        }

        public List<Training> GetIndividualTraining(Guid id)
        {
            var list= _context.Training
                .Include(x => x.Trainer)
                .Include(x => x.PersonTraining)
                .Where(x => x.Type == TrainingType.Individual && x.Start > DateTime.Now)
                .Where(x => x.PersonTraining.All(y => y.PersonID != id))
                .OrderByDescending(x => x.Start)
                .ToList();
            return list;
        }

        public List<Training> GetMyTraining(Guid id)
        {
            var list = _context.Training.Include(x => x.Trainer).Include(x => x.PersonTraining).ToList();
            var list2 = list.Where(x => x.PersonTraining.Any(c => c.PersonID == id)).OrderByDescending(x => x.Start).ToList();
            return list2;
        }

        public void UpdateProfile(Client c)
        {
            _context.Update(c);
            _context.SaveChanges();
        }


        public Carnet GetCarnet(Guid id)
        {
            return _context.Carnets.Include(x => x.Type).Where(x => x.ClientID == id).FirstOrDefault();
        }

        public Training GetTraining(Guid id)
        {
            var det = _context.Training
                .Include(x=>x.PersonTraining)
                    .Where(x => x.TrainingID == id)
                    .FirstOrDefault();
            return det;
        }

        public void TakePart(PersonTraining t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public Client GetClient(Guid iduser)
        {
            var det = _context.Clients
        .Where(x => x.PersonID == iduser)
        .FirstOrDefault();
            return det;
        }

        public void Unsubscribe(Guid id, Guid iduser)
        {
            PersonTraining remove = _context.PersonTrainings.Where(x => x.PersonID == iduser && x.TrainigID==id).FirstOrDefault();
            _context.Remove(remove);
            _context.SaveChanges();
        }

        public EntranceDto GetEntrance(Guid idPerson)
        {
            var carnet = _context.Carnets.Include(x=>x.Type).Where(x => x.ClientID == idPerson).FirstOrDefault();
            List<DateTime> dates = new List<DateTime>();
            if (carnet != null && carnet.IsActive)
            {
                dates = _context.Entrances.Where(x => x.CarnetID == carnet.CarnetID).Select(x => x.Date).OrderByDescending(x => x.Date).ToList();

            }
            EntranceDto dto = new EntranceDto
            {
                CarnetId = carnet.CarnetID,
                Dates = dates,
                Type = carnet.Type,
                DateEnd = carnet.DateEnd,
                DateStart = carnet.DateStart

            };

            return dto;
        }

        public int CountClients()
        {
            return _context.Clients.Include(x => x.Carnets).Where(x => x.ProfileComplete).Count();
        }

        public List<MonthStatisticsDto> Chart()
        {
            var month = DateTime.Now.Month;
            List<MonthStatisticsDto> list = new List<MonthStatisticsDto>();
            for (int i = 0; i < 6; i++)
            {
                MonthStatisticsDto dto = new MonthStatisticsDto()
                {
                    Month = month,
                    EntranceCount = _context.Entrances.Where(x => x.Date.Month == month).Count(),
                    IndividualCount = _context.Training.Where(x => x.Type == TrainingType.Individual && x.Start.Month == month).Count(),
                    GroupCount = _context.Training.Where(x => x.Type == TrainingType.Group && x.Start.Month == month).Count()
                };
                list.Add(dto);
                month--;
            }
            list.Reverse();
            return list;
        }

        public List<WeeksStatisticsDto> ChartWeek()
        {
            var day = DateTime.Now.Date;
            List<WeeksStatisticsDto> list = new List<WeeksStatisticsDto>();
            for (int i = 0; i < 7; i++)
            {
                WeeksStatisticsDto dto = new WeeksStatisticsDto()
                {
                    Day = day,
                    EntranceCount = _context.Entrances.Where(x => x.Date.Date == day).Count(),
                    IndividualCount = _context.Training.Where(x => x.Type == TrainingType.Individual && x.Start.Date==day).Count(),
                    GroupCount = _context.Training.Where(x => x.Type == TrainingType.Group && x.Start.Date==day).Count()
                };
                list.Add(dto);
                day=day.AddDays(-1);
            }
            list.Reverse();
            return list;
        }
    }
}
