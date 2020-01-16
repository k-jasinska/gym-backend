using Portal.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Application.Models
{
    public class TrainingDto
    {
        public Guid TrainingID { get; set; }
        public string Title { get; set; }
        public TrainingType Type { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public Trainer Trainer { get; set; }
        public int Quantity { get; set; }
        public int Count { get; set; } = 0;

        public List<PersonTraining> PersonTraining { get; set; }

        public TrainingDto(Training t)
        {
            TrainingID = t.TrainingID;
            Title = t.Title;
            Type = t.Type;
            Start = t.Start;
            Duration = t.Duration;
            Trainer = t.Trainer;
            Quantity = t.Quantity;
            if (t.PersonTraining != null)
            {
                Count = t.PersonTraining.Where(x => x.TrainigID == t.TrainingID).Count();
            }
            PersonTraining = t.PersonTraining;
        }
    }

}
