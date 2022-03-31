using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_2_Scripting
{
    internal class Equip : Card
    {
        private int baseEffectPoints;
        public int EffectPoints 
        {
            get => baseEffectPoints;
            set => baseEffectPoints = value; 
        }
        public EAttributeType TargetAttribute { get; private set; }
        public EAffinityEquip Affinity { get; private set; }

        public Equip(ERarity rarity, string name, int costPoints, int EP, EAttributeType targetAttribute, EAffinityEquip affinity) : base(rarity, name, costPoints)
        {
            EffectPoints = EP;
            TargetAttribute = targetAttribute;
            Affinity = affinity;
        }
    }
}
