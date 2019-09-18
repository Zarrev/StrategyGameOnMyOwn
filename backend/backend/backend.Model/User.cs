using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
    public class User
    {
        [Key]
        public string key { get; set; }
        [ForeignKey("country")]
        public string countryKey { get; set; }
        public Country country { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string password { get; set; }
        public int points { get; set; }

    }
}
