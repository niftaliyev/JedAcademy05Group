﻿using System.ComponentModel.DataAnnotations;

namespace EShop.Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int Count { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }

    public IEnumerable<OrderProduct> OrderProducts { get; set; }

}
