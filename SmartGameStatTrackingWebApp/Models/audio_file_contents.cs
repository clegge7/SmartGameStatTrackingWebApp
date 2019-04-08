using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartGameStatTrackingWebApp.Models
{
    public class audio_file_contents
    {
        [Key]
        public int a_f_id { get; set; }

        public int g_id { get; set; }

        [Column(TypeName = "varchar")]
        public string file_text { get; set; }

        [Column(TypeName = "varchar")]
        public string file_name { get; set; }
        
        public int manual_control { get; set; }
    }
}