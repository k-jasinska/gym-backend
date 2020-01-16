using Microsoft.EntityFrameworkCore;
using Portal.Application;
using Portal.Application.Helpers;
using Portal.Application.Models;
using Portal.Application.RepositoriesInterfaces;
using Portal.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Infrastructure
{
    public class TrainerRepository : ITrainerRepository, ITrainerQueryRepository
    {
        private readonly Context _context;

        public TrainerRepository(Context context)
        {
            _context = context;
        }
        private int TotalCount()
        {
            var result = _context.Trainers.Count();
            return result;
        }

        public PagedResultDto<Trainer> Get(SearchCriteria criteria)
        {
            var list = _context.Trainers.Skip(criteria.RowsPerPage * (criteria.Page - 1)).Take(criteria.RowsPerPage).ToList();
            PagedResultDto<Trainer> result = new PagedResultDto<Trainer>()
            {
                Items = list,
                Page = criteria.Page,
                RowsPerPage = criteria.RowsPerPage,
                TotalCount = TotalCount()
            };
            return result;
        }

        public void Save(Trainer p)
        {
            _context.Add(p);
            _context.SaveChanges();
        }

        public string CreateTraining(Training training)
        {
            _context.Add(training);
            _context.SaveChanges();
            return training.Title;
        }

        public Trainer GetTrainerById(Guid idTrainer)
        {
            return _context.Trainers.Where(x => x.PersonID == idTrainer).FirstOrDefault();
        }

        public List<Training> GetTrainingListById(Guid idTrainer)
        {
            var list = _context.Training
                .Where(x => x.Trainer.PersonID == idTrainer)
                .ToList();
            return list;
        }

        public List<Training> GetNextThree(Guid id)
        {
            var y= _context.Training.Where(x => x.Trainer.PersonID == id && x.Start>=DateTime.Now).OrderByDescending(x => x.Start).Take(3).ToList();
            return y;
        }

        public Trainer GetDetails(Guid id)
        {
           var trainer= _context.Trainers.Where(x => x.PersonID == id).FirstOrDefault();
            trainer.Training = GetNextThree(id);
            return trainer;
        }

        public List<IndividualTrainingDto> GetIndividualTrening(Guid idTrainer, bool ends)
        {
            List<IndividualTrainingDto> list= new List<IndividualTrainingDto>();
            if (ends)
            {
                 list = _context.Training
                    .Include(x => x.PersonTraining)
                    .Where(x => x.Trainer.PersonID == idTrainer && x.Type == TrainingType.Individual &&  x.Start<DateTime.Now)
                    .OrderBy(x => x.Start)
                    .Select(x=>new IndividualTrainingDto(x))
                    .ToList();
            }
            else
            {
                list = _context.Training
                    .Include(x => x.PersonTraining)
                    .Where(x => x.Trainer.PersonID == idTrainer && x.Type == TrainingType.Individual && x.Start > DateTime.Now)
                    .OrderBy(x => x.Start)
                    .Select(x => new IndividualTrainingDto(x))
                    .ToList();
            }
            foreach (var item in list)
            {
                if (item.Count!=0)
                {
                    Client klient = _context.Clients.Where(x => x.PersonID == item.PersonTraining[0].PersonID).FirstOrDefault();

                    item.Name = klient.Name;
                    item.Surname = klient.Surname;
                }
                }
            return list;

        }

        public List<TrainingDto> GetGroupTrening(Guid idTrainer, bool ends)
        {
            List<TrainingDto> list = new List<TrainingDto>();
            if (ends)
            {
                list = _context.Training.Include(x => x.PersonTraining).Where(x => x.Trainer.PersonID == idTrainer && x.Type==TrainingType.Group && x.Start < DateTime.Now).OrderBy(x => x.Start).Select(x => new TrainingDto(x)).ToList();
            }
            else
            {
                list = _context.Training.Include(x => x.PersonTraining).Where(x => x.Trainer.PersonID == idTrainer && x.Type == TrainingType.Group && x.Start > DateTime.Now).OrderBy(x => x.Start).Select(x => new TrainingDto(x)).ToList();

            }
            return list;

        }

        public List<NamesDto> GetPeople(Guid id)
        {
            List<Guid> ids = _context.PersonTrainings.Where(x => x.TrainigID == id).Select(x=>x.PersonID).ToList();
            List<NamesDto> list= new List<NamesDto>();
            foreach (var item in ids)
            {
                Client k = _context.Clients.Where(x => x.PersonID == item).FirstOrDefault();
                NamesDto dto = new NamesDto
                {
                    Name = k.Name,
                Surname=k.Surname
                };
                list.Add(dto);
            }
            return list;
        }

        public int CountTrainers()
        {
            return _context.Trainers.Count();
        }
    }
}
