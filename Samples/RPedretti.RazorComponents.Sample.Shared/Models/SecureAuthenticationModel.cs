using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.Sample.Shared.Models
{
    /// <summary>
    /// Represents a Authentication Model
    /// </summary>
    public sealed class SecureAuthenticationModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [JsonPropertyName("content")]
        [Required]
        [Display(Name = "content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonPropertyName("id")]
        [Required]
        [Display(Name = "id")]
        public string Id { get; set; }

        #endregion Properties
    }
}
