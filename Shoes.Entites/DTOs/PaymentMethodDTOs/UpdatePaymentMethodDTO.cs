﻿namespace Shoes.Entites.DTOs.PaymentMethodDTOs
{
    public class UpdatePaymentMethodDTO
    {
        public Guid Id { get; set; }
        public bool IsApi { get; set; } 
        public Dictionary<string,string> Lang { get; set; }
    }
}
