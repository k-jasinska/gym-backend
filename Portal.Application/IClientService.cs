using Microsoft.AspNetCore.Mvc;
using Portal.Application.Models;
using Portal.Application.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application
{
    public interface IClientService
    {
        bool CheckLogin(string login);
        Task CreatePerson(Person p);
        void Save(Person p);
        void DeleteClients(List<Guid> selected);
        Client GetDetails(Guid id);
        List<TrainingDto> GetGroupTraining(Guid id);
        List<TrainingDto> GetIndividualTraining(Guid id);
        List<TrainingDto> GetMyTraining(Guid id);
        void UpdateProfile(Guid id, ClientDto client);
        CompleteDto CheckComplete(Guid id);
        void ActivateCarnet(Guid id);
        void TakePart(Guid id, Guid iduser);
        void Unsubscribe(Guid guid, Guid iduser);
        Task SetUserPassword(Guid id, PasswordDto dto);
        CountStatisticsDto CountStatistics();
        List<MonthStatisticsDto> Chart();
        List<WeeksStatisticsDto> ChartWeek();
    }
}
