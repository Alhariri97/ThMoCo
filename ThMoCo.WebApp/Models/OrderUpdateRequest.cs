﻿using ThMoCo.WebApp.DTO;

namespace ThMoCo.WebApp.Models;

public class OrderUpdateRequest
{
    public List<OrderItemDTO> Items { get; set; }

}
