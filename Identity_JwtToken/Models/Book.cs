﻿namespace Identity_JwtToken.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid CreatedBy { get; set; }
}
