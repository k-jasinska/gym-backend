using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Application;
using Portal.Application.Models;
using Portal.Application.RepositoriesInterfaces;
using Portal.Models;


namespace Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Trainer")]
    [Authorize]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly IUserContext _ctx;
        private readonly ITrainerQueryRepository _queryTrainer;
        private readonly ICarnetService _carnetService;


        public TrainerController(ITrainerService trainerService, IUserContext ctx, ITrainerQueryRepository trainerRepo, ICarnetService carnetService)
        {
            _ctx = ctx;
            _queryTrainer = trainerRepo;
            _trainerService = trainerService;
            _carnetService = carnetService;
        }
        [HttpPost]
        public ActionResult CreateTraining([FromBody]Training training)
        {
            Guid idTrainer = _ctx.CurrentUserId;
            training.Trainer = _queryTrainer.GetTrainerById(idTrainer);
            _trainerService.CreateTraining(training);
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<Training>> GetTreningList()
        {
            Guid idTrainer = _ctx.CurrentUserId;
            List<Training>  list= _queryTrainer.GetTrainingListById(idTrainer);
            return list;
        }

        [HttpGet("GroupTrening")]
        public ActionResult<List<TrainingDto>> GetGroupTrening([FromQuery]bool ends)
        {
            Guid idTrainer = _ctx.CurrentUserId;
            List<TrainingDto> list = _queryTrainer.GetGroupTrening(idTrainer, ends);
            return list;
        }

        [HttpGet("IndividualTrening")]
        public ActionResult<List<IndividualTrainingDto>> GetIndividualTrening([FromQuery]bool ends)
        {
            Guid idTrainer = _ctx.CurrentUserId;
            List<IndividualTrainingDto> list = _queryTrainer.GetIndividualTrening(idTrainer, ends);
            return list;
        }

        [HttpGet("Participants")]
        public ActionResult<List<NamesDto>> GetPeople([FromQuery]Guid id)
        {
            List<NamesDto> list = _queryTrainer.GetPeople(id);
            return list;
        }


        
        [HttpGet("Details")]
        public ActionResult<Trainer> GetDetails()
        {
            Guid Id = _ctx.CurrentUserId;
             return _trainerService.GetDetails(Id);
        }
    }
}