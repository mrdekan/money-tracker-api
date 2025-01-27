﻿using System.ComponentModel.DataAnnotations;

namespace money_tracker.Application.Dtos.Requests.Stores
{
    public class UpdateStoreDto
    {
        [Required]
        [StringLength(
            50,
            MinimumLength = 2,
            ErrorMessage = "Name must be between 2 and 50 characters."
        )]
        public string Name { get; set; }
    }
}
