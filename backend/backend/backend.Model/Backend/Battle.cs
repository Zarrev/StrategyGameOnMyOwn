using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Model.Backend
{
    public class Battle
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int AssaultSeaDog { get; set; }
        public int BattleSeahorse { get; set; }
        public int LaserShark { get; set; }

        public string EnemyId { get; set; }
        public virtual User Enemy { get; set; }
        public int EnemyAssaultSeaDog { get; set; }
        public int EnemyBattleSeahorse { get; set; }
        public int EnemyLaserShark { get; set; }

    }
}
