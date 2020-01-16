using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Application;
using Portal.Application.Models;
using Portal.Application.Models.Dto;
using Portal.Models;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IUserContext _ctx;
        private readonly ICarnetService _carnetService;
        private readonly IClientQueryRepository _queryClient;
        private readonly IClientService _clientService;

        public ClientController(IClientQueryRepository contextClient, IUserContext ctx, IClientService clientService, ICarnetService carnetService)
        {
            _ctx = ctx;
            _clientService = clientService;
            _carnetService=carnetService;
            _queryClient = contextClient;
        }

        [HttpGet("Details")]
        public ActionResult<Client> GetDetails()
        {
            Guid Id = _ctx.CurrentUserId;
            return _clientService.GetDetails(Id);
        }

        [HttpGet("GroupTraining")]
        public ActionResult<List<TrainingDto>> GetGroupTraining()
        {
            Guid id = _ctx.CurrentUserId;
            return _clientService.GetGroupTraining(id);
        }

        [HttpGet("IndividualTraining")]
        public ActionResult<List<TrainingDto>> GetIndividualTraining()
        {
            Guid id = _ctx.CurrentUserId;
            return _clientService.GetIndividualTraining(id);
        }

        [HttpGet("MyTraining")]
        public ActionResult<List<TrainingDto>> GetMyTraining()
        {
            Guid id = _ctx.CurrentUserId;
            return _clientService.GetMyTraining(id);
        }
        [HttpGet("CheckComplete")]
        public ActionResult<CompleteDto> CheckComplete()
        {
            Guid id = _ctx.CurrentUserId;
            return _clientService.CheckComplete(id);
        }
        
        [HttpPut("Details")]
        public ActionResult AddCarnet([FromBody]ClientDto client)
        {
            Guid id = _ctx.CurrentUserId;
            _clientService.UpdateProfile(id, client);
            return Ok();
        }

        [HttpPost("Carnet")]
        public ActionResult AddCarnet([FromBody]CarnetType carnetType)
        {
            Guid trenerId = _ctx.CurrentUserId;
            _carnetService.AddCarnet(carnetType, trenerId);
            return Ok();
        }


        [HttpPost("Select")]
        public ActionResult SelectCarnet([FromBody]IdDto id)
        {
            Guid iduser = _ctx.CurrentUserId;
            try
            {
                _carnetService.SelectCarnet(id.Id, iduser);
            }
            catch
            {
                return BadRequest(new { message = "Karnet już został wybrany!" });
            }
            return Ok();
        }

        [HttpPost("TakePart")]
        public ActionResult TakePart([FromBody]IdDto id)
        {
            Guid iduser = _ctx.CurrentUserId;
            _clientService.TakePart(id.Id, iduser);
            return Ok();
        }


        [HttpDelete]
        public ActionResult Delete([FromBody]List<Guid> selected)
        {
            Guid iduser = _ctx.CurrentUserId;
            _clientService.Unsubscribe(selected[0], iduser);
            return Ok();
        }
        [HttpGet("GetEntrance")]
        public ActionResult<EntranceDto> GetEntrance()
        {
            Guid iduser = _ctx.CurrentUserId;
            return Ok(_queryClient.GetEntrance(iduser));
        }

        [HttpPut("ChangePass")]
        public async Task<ActionResult> ChangePass([FromBody]PasswordDto dto)
        {
            Guid iduser = _ctx.CurrentUserId;
            try
            {
               await  _clientService.SetUserPassword(iduser, dto);
            }
            catch
            {
                return BadRequest(new { message = "Stare hasło jest nieprawidłowe" });
            }
            return Ok();
        }
    }
}