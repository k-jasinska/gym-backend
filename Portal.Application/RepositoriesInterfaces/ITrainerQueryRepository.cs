using Portal.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.RepositoriesInterfaces
{
    public interface ITrainerQueryRepository
    {
        PagedResultDto<Trainer> Get(SearchCriteria criteria);
        Trainer GetTrainerById(Guid idTrainer);
        List<Training> GetTrainingListById(Guid idTrainer);
        List<IndividualTrainingDto> GetIndividualTrening(Guid idTrainer, bool ends);
        List<TrainingDto> GetGroupTrening(Guid idTrainer, bool ends);
        List<NamesDto> GetPeople(Guid id);
    }
}
