using System;
using System.Collections.Generic;
using System.Text;
using Portal.Application.Models;
using Portal.Application.Models.Dto;

namespace Portal.Application.RepositoriesInterfaces
{
    public interface ICarnetRepository
    {
        void Save(CarnetType p);
        void AddCarnet(Carnet t);
        CarnetType GetTypeById(Guid id);
        void SelectCarnet(Carnet carnet);
        void ActivateCarnet(Carnet carnet);
        void Entrance(Entrance e);
        int CountCarnets();
        int CountEntrances(int days);
        List<UsersByCarnetsDto> UsersByCarnets();
        bool CheckIfExist(Guid id);
    }
}
