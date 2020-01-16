using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Application.Models;
using Portal.Application.Models.Dto;
using Portal.Application.RepositoriesInterfaces;

namespace Portal.Application
{
    public class ClientService : IClientService
    {
        private IClientRepository _clientRepository;
        private ITrainerRepository _trainerRepository;
        private ICarnetRepository _carnetRepository;
        private IUserRepository _ctxr;

        public ClientService(IClientRepository ctx, IUserRepository ctxr, ICarnetRepository carnet, ITrainerRepository trainer)
        {
            _trainerRepository = trainer;
            _clientRepository = ctx;
            _ctxr = ctxr;
            _carnetRepository = carnet;
        }

        public CompleteDto CheckComplete(Guid id)
        {
            Client client = GetDetails(id);
            Carnet carnet = GetCarnet(id);
            bool exist = false;
            bool active = false;
            if (carnet != null)
            {
                exist = true;
                active = carnet.IsActive;
            }
            CompleteDto c = new CompleteDto
            {
                Complete = client.ProfileComplete,
                Exist = exist,
                Active = active
            };
            return c;
        }

        public bool CheckLogin(string login)
        {
            return _clientRepository.CheckLogin(login);
        }

        public async Task CreatePerson(Person p)
        {
            await _ctxr.CreatePerson(p);
        }

        public void DeleteClients(List<Guid> selected)
        {
            foreach (var id in selected)
            {
                _clientRepository.DeleteClients(id);
                _ctxr.DeleteClients(id);
            }
        }

        public Client GetDetails(Guid id)
        {
            return _clientRepository.GetDetails(id);

        }
        public Carnet GetCarnet(Guid id)
        {
            return _clientRepository.GetCarnet(id);
        }

        public List<TrainingDto> GetGroupTraining(Guid id)
        {
            var list=_clientRepository.GetGroupTraining(id);
            List<TrainingDto> trainigs = new List<TrainingDto>();
            foreach (var item in list)
            {
                TrainingDto dto = new TrainingDto(item);

                trainigs.Add(dto);
            }

            return trainigs;
        }

        public List<TrainingDto> GetIndividualTraining(Guid id)
        {
            var list= _clientRepository.GetIndividualTraining(id);
            List<TrainingDto> trainigs = new List<TrainingDto>();
            foreach (var item in list)
            {
                TrainingDto dto = new TrainingDto(item);

                trainigs.Add(dto);
            }

            return trainigs;

        }

        public List<TrainingDto> GetMyTraining(Guid id)
        {
            var list = _clientRepository.GetMyTraining(id);
            List<TrainingDto> trainigs = new List<TrainingDto>();
            foreach (var item in list)
            {
                TrainingDto dto = new TrainingDto(item);
               
                trainigs.Add(dto);
            }

            return trainigs;
        }

        public void Save(Person p)
        {
            Client c = new Client()
            {
                PersonID = p.PersonID,
                Name = p.Name,
                Surname = p.Surname,
                Login = p.Login,
                Password = p.Password,
                Email = p.Email,
                Role = p.Role
            };
            _clientRepository.Save(c);
        }

        public void UpdateProfile(Guid id, ClientDto client)
        {
            Client c = GetDetails(id);
            c.Name = client.Name;
            c.Surname = client.Surname;
            c.Sex = client.Sex;
            c.Address = client.Address;
            c.ContactData = client.ContactData;
            c.ProfileComplete = true;

            _clientRepository.UpdateProfile(c);
        }

        public void ActivateCarnet(Guid id)
        {
            DateTime start = DateTime.Now;
            Carnet carnet = GetCarnet(id);
            carnet.IsActive = true;
            carnet.DateStart = start;
            carnet.DateEnd = start.AddDays(carnet.Type.Duration);

            _carnetRepository.ActivateCarnet(carnet);
        }

        public void TakePart(Guid id, Guid iduser)
        {
            Training training = _clientRepository.GetTraining(id);

            int count=training.PersonTraining.Where(x => x.TrainigID == id).Count();
            if (count < training.Quantity)
            {
                Client client = _clientRepository.GetDetails(iduser);
                PersonTraining pt = new PersonTraining()
                {
                    PersonID = iduser,
                    TrainigID = id,
                    Client = client,
                    Training = training
                };

                _clientRepository.TakePart(pt);
            }
            else
            {
               throw new ArgumentException("Ilość miejsc osiągnięta", "original");
            }
            }

        public void Unsubscribe(Guid guid, Guid iduser)
        {
            _clientRepository.Unsubscribe(guid, iduser);
        }

        public async Task SetUserPassword(Guid id, PasswordDto dto)
        {
            Client c = GetDetails(id);
            if (dto.OldPassword == c.Password)
            {
                c.Password = dto.NewPassword;
                _clientRepository.UpdateProfile(c);
                await _ctxr.SetUserPassword(id.ToString(), dto.NewPassword);
            }
            else
            {
                throw new ArgumentException("Stare hasło jest niepoprawne");
                //throw new InvalidOperationException("Stare hasło jest niepoprawne");
            }
        }

        public CountStatisticsDto CountStatistics()
        {
            int carnets = _carnetRepository.CountCarnets();
            int clients = _clientRepository.CountClients();
            int trainers = _trainerRepository.CountTrainers();
            int entrances= _carnetRepository.CountEntrances(-1);

            CountStatisticsDto dto = new CountStatisticsDto()
            {
                AllCarnets= carnets,
                AllClient= clients,
                AllTrainer= trainers,
                LastEntrances = entrances
            };
            return dto;
        }

        public List<MonthStatisticsDto> Chart()
        {
            return _clientRepository.Chart();
        }

        public List<WeeksStatisticsDto> ChartWeek()
        {
            return _clientRepository.ChartWeek();
        }
    }
}
