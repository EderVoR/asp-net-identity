﻿using System.ComponentModel.DataAnnotations;

namespace MvcWebIdentity.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "As senhas não são iguais")]
        public string? ConfirmPassword { get; set; }
    }
}
