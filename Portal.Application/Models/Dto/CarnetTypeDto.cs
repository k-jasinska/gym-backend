using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Models.Dto
{
    public class CarnetTypeDto
    {
        public Guid CarnetTypeID { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Count { get; set; }
        public CarnetTypeDto(CarnetType x)
        {
            CarnetTypeID = x.CarnetTypeID;
            Price = x.Price;
            Name = x.Name;
            Duration = x.Duration;
        }
    }
}
