﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.Sample.Shared.Models
{
    /// <summary>
    /// Represents a User authentication
    /// </summary>
    public sealed class UserAuthenticationModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [JsonPropertyName("password")]
        [Required]
        [Display(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [JsonPropertyName("username")]
        [Required]
        [Display(Name = "username")]
        public string Username { get; set; }

        #endregion Properties
    }
}
