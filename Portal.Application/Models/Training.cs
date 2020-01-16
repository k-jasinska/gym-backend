using Portal.Application.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portal.Application.Models
{
    public class Training
    {
        [Key]
        public Guid TrainingID { get; set; }
        public string Title { get; set; }
        public TrainingType Type { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public Trainer Trainer { get; set; }
        public int Quantity { get; set; }
        public List<PersonTraining> PersonTraining { get; set; }
    }
}
