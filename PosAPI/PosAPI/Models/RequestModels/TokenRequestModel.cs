﻿namespace PosAPI.Models.RequestModels
{
    public class TokenRequestModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
