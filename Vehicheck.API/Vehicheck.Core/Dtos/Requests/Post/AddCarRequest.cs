﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Requests.Post
{
    public class AddCarRequest
    {
        public int YearOfManufacture { get; set; }
        public int CarMileage { get; set; }
        public int UserId { get; set; }
        public int CarModelId { get; set; }
        public int CarManufacturerId { get; set; }

        public Car ToEntity()
        {
            return new Car
            {
                YearOfManufacture = YearOfManufacture,
                CarMileage = CarMileage,
                UserId = UserId,
                CarModelId = CarModelId,
                CarManufacturerId = CarManufacturerId
            };
        }
    }
}
