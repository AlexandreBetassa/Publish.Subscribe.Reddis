﻿using DatabaseAPI.Enum.v1;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAPI.Models.v1
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public double Value { get; set; }
        public string Cpf { get; set; }
        public string CreditCard { get; set; }
        public StatusEnum Status { get; set; }
    }
}