﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PropertyTracker.Application.DTO
{
    public class Site
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
