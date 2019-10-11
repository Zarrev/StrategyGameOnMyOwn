using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Model.Backend
{
    public class BattleResult
    {
        public string WinnerId { get; set; }
        public string LoserId { get; set; }
        public Battle Battle { get; set; }
    }
}
