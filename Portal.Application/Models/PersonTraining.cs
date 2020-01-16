using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portal.Application.Models
{
    public class PersonTraining
    {
        [Key]
        public Guid PersonTrainingID { get; set; }
        public Guid PersonID { get; set; }
        public Client Client { get; set; }
        public Guid TrainigID { get; set; }
        public Training Training { get; set; }
    }
}
