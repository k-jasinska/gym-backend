using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Models;
using Portal.Application.Models.Dto;
using Portal.Application.RepositoriesInterfaces;
using Portal.Models;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarnetController : ControllerBase
    {
        private readonly ICarnetQueryRepository _queryCarnet;
        private readonly IUserContext _ctx;

        public CarnetController(IUserContext ctx, ICarnetQueryRepository queryCarnet)
        {
            _queryCarnet = queryCarnet;
            _ctx = ctx;
        }


        [HttpGet("Types")]
        public ActionResult<List<CarnetTypeDto>> GetCarnetTypes()
        {
            return _queryCarnet.GetTypes();
        }
    }
}