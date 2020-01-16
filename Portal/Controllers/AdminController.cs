using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application;
using Portal.Application.Models;
using Portal.Application.Models.Dto;
using Portal.Application.RepositoriesInterfaces;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Authorize]
    public class AdminController:Controller
    {
        private readonly IClientQueryRepository _queryClient;
        private readonly ITrainerQueryRepository _queryTrainer;
        private readonly IUserContext _ctx;
        private readonly IClientService _clientService;
        private readonly ITrainerService _trainerService;
        private readonly ICarnetService _carnetService;

        

        public AdminController(IClientQueryRepository contextClient, ITrainerQueryRepository contextTrainer,IUserContext ctx, IClientService clientService, ITrainerService trainerService, ICarnetService carnetService)
        {
            _queryClient = contextClient;
            _queryTrainer = contextTrainer;
            _ctx = ctx;
            _clientService = clientService;
            _trainerService = trainerService;
            _carnetService = carnetService;
        }

        [HttpPost("Carnet")]
        public async Task<IActionResult> CreateCarnetType([FromBody]CarnetType type)
        {
            _carnetService.Save(type);
            return Ok();
        }

        [HttpGet("Clients")]
        public ActionResult<PagedResultDto<ClientWithStatus>> GetClients([FromQuery]SearchCriteria criteria)
        {
            return Json(_queryClient.Get(criteria));
        }
        [HttpGet("Trainers")]
        public ActionResult<PagedResultDto<Client>> GetTrainers([FromQuery]SearchCriteria criteria)
        {
            return Json(_queryTrainer.Get(criteria));
        }
        [HttpGet("Search/Clients")]
        public ActionResult<PagedResultDto<Client>> SearchClients([FromQuery]SearchCriteria criteria, [FromQuery]string value)
        {
            return Json(_queryClient.Get(criteria,value));
        }

        [HttpGet("loginValidation")]
        public ActionResult<PagedResultDto<Client>> CheckLogin([FromQuery]string login)
        {
            if (_clientService.CheckLogin(login))
            {
                return Json(new { message= "Podany login jest już zajęty!" });
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete([FromBody]List<Guid> selected)
        {
            _clientService.DeleteClients(selected);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("checkRole")]
        public ActionResult<string> CheckRole()
        {
            var id = _ctx.CurrentUserId;
            return Json(_queryClient.checkRole(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Person person)
        {
            person.Password = person.Login;
            if (_clientService.CheckLogin(person.Login))
            {
                return BadRequest(new { message = "Podany login jest już zajęty!" });
            }

            await _clientService.CreatePerson(person);
            if (person.Role == "Client")
            {
                _clientService.Save(person);
            }
            else if (person.Role == "Trainer")
            {
                _trainerService.Save(person);
            }
            return Ok();
        }

        [HttpGet("GetEntrance")]
        public ActionResult<EntranceDto> GetEntrance([FromQuery]Guid id)
        {

            return Json(_queryClient.GetEntrance(id));
        }

        [HttpPost("Entrance")]
        public ActionResult Entrance([FromBody]IdDto id)
        {
            _carnetService.Entrance(id.Id);
            return Ok();
        }

        [HttpGet("CheckCompleteById")]
        public ActionResult<CompleteDto> CheckComplete([FromQuery]Guid id)
        {
            return _clientService.CheckComplete(id);
        }

        //liczba wejsc dzisiaj
        //liczba wszystkich osob
        //liczba wszystkich aktywnych karmetów teraz
        //liczba trenerów
        [HttpGet("CountStatistics")]
        public ActionResult<CountStatisticsDto> CountStatistics()
        {
            return _clientService.CountStatistics();
        }

        [HttpGet("UsersByCarnets")]
        public ActionResult<List<UsersByCarnetsDto>> UsersByCarnets()
        {
            return _carnetService.UsersByCarnets();
        }

        [HttpGet("Chart")]
        public ActionResult<List<MonthStatisticsDto>> Chart()
        {
            return _clientService.Chart();
        }

        [HttpGet("ChartWeek")]
        public ActionResult<List<WeeksStatisticsDto>> ChartWeek()
        {
            return _clientService.ChartWeek();
        }

        [HttpGet("Details")]
        public ActionResult<Client> Details([FromQuery]Guid id)
        {
            return _clientService.GetDetails(id);
        }

        [HttpPut("Details")]
        public ActionResult Details([FromBody]ClientDto client, [FromQuery]Guid id)
        {
            _clientService.UpdateProfile(id, client);
            return Ok();
        }

        [HttpPut("Activate")]
        public ActionResult ActivateCarnet([FromBody]IdDto id)
        {
            _clientService.ActivateCarnet(id.Id);
            return Ok();
        }

        
    }
}
