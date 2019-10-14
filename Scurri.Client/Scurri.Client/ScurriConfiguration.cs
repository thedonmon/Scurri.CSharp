using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Scurri.Client
{
    /// <summary>
    /// Configuration Values to Access API. 
    /// </summary>
    public class ScurriConfiguration : IScurriConfiguration
    {
        [Required]
        public string CompanySlug { get; set; }
        /// <summary>
        /// Authtoken using base64 encoding. Optional if username and secret are provided
        /// </summary>
        public string AuthToken { get; set; }
        /// <summary>
        /// Username used to generate authtoken with secret. Need both to be valid if authtoken is not provided
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Secret used to generate authtoken with username. Need both to be valid if authtoken is not provided
        /// </summary>
        public string Secret { get; set; }
    }
}
