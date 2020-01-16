using System;
using System.Collections.Generic;

namespace Portal.Models
{
    public interface IUserContext
    {
        Guid CurrentUserId { get; }
    }
}