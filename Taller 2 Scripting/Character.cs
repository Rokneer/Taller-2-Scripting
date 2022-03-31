using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_2_Scripting
{
    internal class Character: Card
    {
        private int baseAttackPoints;
        private int baseResistPoints;
        Equip[] equipmentSlots = new Equip[3];

        public int AttackPoints 
        {
            get => baseAttackPoints;
            set => baseAttackPoints = value; 
        }
        public int ResistPoints 
        { 
            get => baseResistPoints; 
            set => baseResistPoints = value; 
        }
        internal Equip[] EquipmentSlots { get => equipmentSlots; set => equipmentSlots = value; }
        public EAffinity Affinity { get; private set; }

        public Character(ERarity rarity, string name, int costPoints, int AP, int RP, EAffinity affinity, Equip[] equipmentSlots) : base(rarity, name, costPoints)
        {
            AttackPoints = AP;
            ResistPoints = RP;
            Affinity = affinity;
            EquipmentSlots = equipmentSlots;
        }

        public void PerformAttack()
        {

        }
    }
}
