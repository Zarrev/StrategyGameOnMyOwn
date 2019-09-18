using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace backend.Model
{
    public class Country
    {
        [Key]
        public string key { get; set; }
        public int pearl { get; set; }
        public int flowController { get; set; }
        public int reefCastle { get; set; }
        public int assaultSeaDog { get; set; }
        public int battleSeahorse { get; set; }
        public int laserShark { get; set; }
        public int points { get; set; }
        public int rounds { get; set; }
        public bool mudTractor { get; set; }
        public bool sludgeharvester { get; set; }
        public bool coralWall { get; set; }
        public bool sonarGun { get; set; }
        public bool underwaterMaterialArts { get; set; }
        public bool alchemy { get; set; }
    }
}
