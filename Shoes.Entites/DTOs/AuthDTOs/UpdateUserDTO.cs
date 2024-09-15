﻿namespace Shoes.Entites.DTOs.AuthDTOs
{
    public class UpdateUserDTO
    {
        public Guid UserId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Adress { get; set; }
        public string? Username { get; set; }
        public string ? CurrentPassword { get; set; }
        public string ? ConfirmCurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }
    }
}
