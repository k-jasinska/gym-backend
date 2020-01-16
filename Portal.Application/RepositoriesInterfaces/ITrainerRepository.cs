using Portal.Application.Models;
using System;

namespace Portal.Application
{
    public interface ITrainerRepository
    {
        void Save(Trainer p);
        string CreateTraining(Training training);
        Trainer GetDetails(Guid id);
        int CountTrainers();
    }
}
