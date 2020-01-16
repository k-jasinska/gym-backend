using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Portal.Application.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Application.Models
{
    public class Client:Person
    {
        public bool ProfileComplete { get; set; } = false;
        public Sex Sex { get; set; }
        public List<PersonTraining> PersonTraining { get; set; }
        public List<Carnet> Carnets { get; set; }
    }
}
