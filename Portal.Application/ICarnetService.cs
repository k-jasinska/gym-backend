using Microsoft.AspNetCore.Mvc;
using Portal.Application.Models;
using Portal.Application.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application
{
    public interface ICarnetService
    {
        void Save(CarnetType type);
        void AddCarnet(CarnetType carnetType, Guid trenerId);
        void SelectCarnet(Guid idCarnet, Guid id);
        void Entrance(Guid id);
       List<UsersByCarnetsDto> UsersByCarnets();
    }
}
