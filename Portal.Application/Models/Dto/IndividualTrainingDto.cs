using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Application.Models
{
    public class IndividualTrainingDto
    {
        public Guid TrainingID { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public int Quantity { get; set; }
        public int Count { get; set; } = 0;
        public string Name { get; set; }
        public string Surname { get; set; }


        public List<PersonTraining> PersonTraining { get; set; }

        public IndividualTrainingDto(Training t)
        {
            TrainingID = t.TrainingID;
            Title = t.Title;
            Start = t.Start;
            Duration = t.Duration;
            Quantity = t.Quantity;
  
            if (t.PersonTraining != null)
            {
                  Count = t.PersonTraining.Where(x => x.TrainigID == t.TrainingID).Count();
            }
            PersonTraining = t.PersonTraining;
        }
    }
}
