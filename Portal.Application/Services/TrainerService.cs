using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Helpers;
using Portal.Application.Models;

namespace Portal.Application.Services
{
    public class TrainerService:ITrainerService
    {
        private ITrainerRepository _trainerRepository;

        public TrainerService(ITrainerRepository ctx)
        {
            _trainerRepository = ctx;
        }

        public void CreateTraining(Training training)
        {
                 _trainerRepository.CreateTraining(training);
        }

        public Trainer GetDetails(Guid id)
        {
            return _trainerRepository.GetDetails(id);
        }

        public void Save(Person p)
        {
            Trainer t = new Trainer()
            {
                PersonID = p.PersonID,
                Name = p.Name,
                Surname = p.Surname,
                Login = p.Login,
                Password = p.Password,
                Email = p.Email,
                Role = p.Role
            };
            _trainerRepository.Save(t);

        }
    }
}
