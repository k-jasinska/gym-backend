using Portal.Application.Models;
using Portal.Application.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application
{
    public interface IClientRepository
    {
        void Save(Client p);
        bool CheckLogin(string login);
        void DeleteClients(Guid selected);
        Client GetDetails(Guid id);
        List<Training> GetGroupTraining(Guid id);
        List<Training> GetIndividualTraining(Guid id);
        List<Training> GetMyTraining(Guid id);
        void UpdateProfile(Client c);
        Carnet GetCarnet(Guid id);
        Training GetTraining(Guid id);
        void TakePart(PersonTraining t);
        Client GetClient(Guid iduser);
        void Unsubscribe(Guid id, Guid iduser);
        int CountClients();
        List<MonthStatisticsDto> Chart();
        List<WeeksStatisticsDto> ChartWeek();
    }
}
