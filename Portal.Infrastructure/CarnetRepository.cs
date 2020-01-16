using Microsoft.EntityFrameworkCore;
using Portal.Application.Models;
using Portal.Application.Models.Dto;
using Portal.Application.RepositoriesInterfaces;
using Portal.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Infrastructure
{
    public class CarnetRepository : ICarnetQueryRepository, ICarnetRepository
    {

        private readonly Context _context;

        public CarnetRepository(Context context)
        {
            _context = context;
        }
        public List<CarnetTypeDto> GetTypes()
        {
           var list= _context.CarnetTypes.ToList();
            List<CarnetTypeDto> dto = new List<CarnetTypeDto>();
            foreach (var item in list)
            {
                CarnetTypeDto i = new CarnetTypeDto(item);
                i.Count = _context.Carnets.Where(x => x.Type == item && x.DateEnd>DateTime.Now).Count();
                dto.Add(i);

            }
            return dto;
        }

        public void Save(CarnetType p)
        {
            _context.Add(p);
            _context.SaveChanges();
        }

        public void AddCarnet(Carnet t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public CarnetType GetTypeById(Guid id)
        {
            return _context.CarnetTypes.Where(x => x.CarnetTypeID == id).FirstOrDefault();
        }

        public void SelectCarnet(Carnet carnet)
        {
            _context.Add(carnet);
            _context.SaveChanges();
        }

        public void ActivateCarnet(Carnet c)
        {
            _context.Update(c);
            _context.SaveChanges();
        }

        public void Entrance(Entrance e)
        {
            _context.Add(e);
            _context.SaveChanges();
        }

        public int CountCarnets()
        {
            return _context.Carnets.Where(x => x.IsActive && x.DateEnd > DateTime.Now).Count();
        }

        public int CountEntrances(int days)
        {
            DateTime date = DateTime.Now.AddDays(days);
            return _context.Entrances.Where(x => x.Date>date).Count();
        }

        public List<UsersByCarnetsDto> UsersByCarnets()
        {
            var list = _context.CarnetTypes.ToList();
            int countAll = _context.Carnets.Where(x => x.DateEnd > DateTime.Now).Count();
            List<UsersByCarnetsDto> dto = new List<UsersByCarnetsDto>();
            foreach (var item in list)
            {
                UsersByCarnetsDto i = new UsersByCarnetsDto();
                i.CarnetName = item.Name;
                i.CountUsers = _context.Carnets.Where(x => x.Type == item && x.DateEnd > DateTime.Now).Count();
                i.Percent = (i.CountUsers*100) / countAll;
                dto.Add(i);
            }
            return dto;
        }

        public bool CheckIfExist(Guid id)
        {
            int exist = _context.Carnets.Where(x => x.ClientID == id && x.DateStart==x.DateEnd).Count();
            if (exist!=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
