using Portal.Application.Models;
using Portal.Application.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.RepositoriesInterfaces
{
    public interface ICarnetQueryRepository
    {
        List<CarnetTypeDto> GetTypes();
    }
}
