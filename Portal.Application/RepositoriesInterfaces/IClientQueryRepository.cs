using Portal.Application.Models;
using Portal.Application.Models.Dto;
using System;
using System.Collections.Generic;

namespace Portal.Application
{
     public interface IClientQueryRepository
    {
        PagedResultDto<ClientWithStatus> Get(SearchCriteria criteria);
        PagedResultDto<Client> Get(SearchCriteria criteria, string search);
        string checkRole(Guid id);
        EntranceDto GetEntrance(Guid id);
    }
}
