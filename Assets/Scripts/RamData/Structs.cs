using UnityEngine;

namespace Structs
{
    public struct TowerStruct
    {
        public Sprite towerSprite;
        public string towerName;
        public int towerIndex, towerHealth, towerDamage;
        public BoundsInt size;
        public Enums.ObjectType objectType;
        public bool isPlaced;

        public TowerStruct(Sprite towerSprite, string towerName, int towerIndex, int towerHealth, int towerDamage, BoundsInt size, Enums.ObjectType objectType)
        {
            this.towerSprite = towerSprite;
            this.towerName = towerName;
            this.towerIndex = towerIndex;
            this.towerHealth = towerHealth;
            this.towerDamage = towerDamage;
            this.size = size;
            this.objectType = objectType;
            this.isPlaced = false;
        }
    }

    public struct SoldierStruct
    {
        public Sprite soldierSprite;
        public int soldierIndex, soldierHealth, soldierDamage;
        public Enums.ObjectType objectType;
        public bool isPlaced;
        public BoundsInt size;

        public SoldierStruct(Sprite soldierSprite, int soldierIndex, int soldierHealth, int soldierDamage, Enums.ObjectType objectType, BoundsInt size)
        {
            this.soldierSprite = soldierSprite;
            this.soldierIndex = soldierIndex;
            this.soldierHealth = soldierHealth;
            this.soldierDamage = soldierDamage;
            this.objectType = objectType;
            this.isPlaced = false;
            this.size = size;
        }
    }
}
