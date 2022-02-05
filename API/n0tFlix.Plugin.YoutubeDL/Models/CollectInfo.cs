using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Plugin.YoutubeDL.Models
{
    public class CollectInfo
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string URL { get; set; } = null!;
    }
}
