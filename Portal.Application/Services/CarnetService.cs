using Portal.Application.Models;
using Portal.Application.Models.Dto;
using Portal.Application.RepositoriesInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Services
{
    public class CarnetService : ICarnetService
    {
        private ICarnetRepository _carnetRepository;

        public CarnetService(ICarnetRepository ctx)
        {
            _carnetRepository = ctx;
        }

        public void Save(CarnetType p)
        {
            _carnetRepository.Save(p);
        }
        public void AddCarnet(CarnetType carnetType, Guid trenerId)
        {
            Carnet t = new Carnet()
            {
                IsActive = false,
                Type = carnetType,
                ClientID = trenerId
            };
            _carnetRepository.AddCarnet(t);
        }

        public CarnetType GetTypeById(Guid id)
        {
            return _carnetRepository.GetTypeById(id);
        }
        public void SelectCarnet(Guid idCarnet, Guid id)
        {
            CarnetType type = GetTypeById(idCarnet);
            Carnet carnet = new Carnet
            {
                Type = type,
                ClientID = id
            };
            if (_carnetRepository.CheckIfExist(id))
            {
                throw new ArgumentException("Posiadasz juz karnet", "original");
            }
            else
            {
                _carnetRepository.SelectCarnet(carnet);
            }
        }

        public void Entrance(Guid id)
        {
            Entrance e = new Entrance()
            {
                Date = DateTime.Now,
                CarnetID = id
            };
            _carnetRepository.Entrance(e);
        }

        public List<UsersByCarnetsDto> UsersByCarnets()
        {
            return _carnetRepository.UsersByCarnets();
        }
    }
}
