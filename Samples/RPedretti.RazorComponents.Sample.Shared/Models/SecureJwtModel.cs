using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.Sample.Shared.Models
{
    /// <summary>
    /// Represents a JWT
    /// </summary>
    public sealed class SecureJwtModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the origin identifier.
        /// </summary>
        /// <value>
        /// The origin identifier.
        /// </value>
        [JsonPropertyName("id")]
        [Required]
        [Display(Name = "id")]
        public int OriginId { get; set; }

        /// <summary>
        /// Gets or sets the token model.
        /// </summary>
        /// <value>
        /// The token model.
        /// </value>
        [JsonPropertyName("token")]
        [Required]
        [Display(Name = "token")]
        public TokenModel TokenModel { get; set; }

        #endregion Properties
    }
}
