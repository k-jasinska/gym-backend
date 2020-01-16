using Microsoft.AspNetCore.Mvc;
using Portal.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application
{
    public interface ITrainerService
    {
        void Save(Person p);
        void CreateTraining(Training training);
        Trainer GetDetails(Guid id);
    }
}
