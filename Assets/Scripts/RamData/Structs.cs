
using UnityEngine;

namespace Structs
{
    public struct TowerStruct
    {
        public Sprite towerSprite;
        public string towerName;
        public int towerIndex, towerHealth, towerDamage;
        public Vector2 size;

        public TowerStruct(Sprite towerSprite, string towerName, int towerIndex, int towerHealth, int towerDamage, Vector2 size)
        {
            this.towerSprite = towerSprite;
            this.towerName = towerName;
            this.towerIndex = towerIndex;
            this.towerHealth = towerHealth;
            this.towerDamage = towerDamage;
            this.size = size;
        }
    }

    public struct SoldierStruct
    {
        public Sprite soldierSprite;
        public int soldierIndex, soldierHealth, soldierDamage;

        public SoldierStruct(Sprite soldierSprite, int soldierIndex, int soldierHealth, int soldierDamage)
        {
            this.soldierSprite = soldierSprite;
            this.soldierIndex = soldierIndex;
            this.soldierHealth = soldierHealth;
            this.soldierDamage = soldierDamage;
        }
    }
}
