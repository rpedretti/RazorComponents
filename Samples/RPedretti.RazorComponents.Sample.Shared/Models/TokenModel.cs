using System;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.Sample.Shared.Models
{
    /// <summary>
    /// Represents a Token response
    /// </summary>
    public sealed class TokenModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the expires.
        /// </summary>
        /// <value>
        /// The expires.
        /// </value>
        [JsonPropertyName("expires")]
        public DateTime Expires { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is expired.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is expired; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpired => new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() > new DateTimeOffset(Expires).ToUnixTimeSeconds();

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>
        /// The refresh token.
        /// </value>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh URL.
        /// </summary>
        /// <value>
        /// The refresh URL.
        /// </value>
        [JsonPropertyName("refresh_url")]
        public string RefreshUrl { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [JsonPropertyName("token")]
        public string Token { get; set; }

        #endregion Properties
    }
}
