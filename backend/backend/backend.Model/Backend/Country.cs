using System.Collections.Generic;

namespace backend.Model
{
    public class Country
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string CountryName { get; set; }
        public int DevelopingName { get; set; }
        public int BuildingName { get; set; }
        public int Inhabitant { get; set; }
        public int Coral { get; set; }
        public int Pearl { get; set; }
        public int FlowController { get; set; }
        public int ReefCastle { get; set; }
        public int AssaultSeaDog { get; set; }
        public int BattleSeahorse { get; set; }
        public int LaserShark { get; set; }
        public int Points { get; set; }
        public int DevRounds { get; set; }
        public int BuildRounds { get; set; }
        public bool MudTractor { get; set; }
        public bool Sludgeharvester { get; set; }
        public bool CoralWall { get; set; }
        public bool SonarGun { get; set; }
        public bool UnderwaterMaterialArts { get; set; }
        public bool Alchemy { get; set; }
    }
}
