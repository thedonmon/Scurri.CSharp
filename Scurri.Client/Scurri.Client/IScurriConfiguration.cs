using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Scurri.Client
{
    /// <summary>
    /// Configuration Values to Access API. Useful for DI
    /// </summary>
    public interface IScurriConfiguration
    {
        [Required]
        string CompanySlug { get; set; }
        /// <summary>
        /// Authtoken using base64 encoding. Optional if username and secret are provided
        /// </summary>
        string AuthToken { get; set; }
        /// <summary>
        /// Username used to generate authtoken with secret. Need both to be valid if authtoken is not provided
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// Secret used to generate authtoken with username. Need both to be valid if authtoken is not provided
        /// </summary>
        string Secret { get; set; }
    }
}
