﻿using GetFood.Entities.Dtos;
using GetFood.Entities.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFood.Business.Abstract
{
    public interface IRestaurantService
    {

        public IResponse<RestaurantDto> CreateRestaurant(int id, RestaurantCreateDto restaurant);

    }
}
