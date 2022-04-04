using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_2_Scripting
{
    internal class Equip : Card
    {
        public int EffectPoints { get; protected set; }
        public EAttributeType TargetAttribute { get; private set; }
        public EAffinity Affinity { get; private set; }
        public Equip() : base()
        {
            EffectPoints = 0;
            TargetAttribute = EAttributeType.ALL;
            Affinity = EAffinity.All;
        }
        public Equip(ERarity rarity, string name, int costPoints, int EP, EAttributeType targetAttribute, EAffinity affinity) : base(rarity, name, costPoints)
        {
            EffectPoints = (int)MathF.Abs(EP);
            TargetAttribute = targetAttribute;
            Affinity = affinity;
        }

    }
}
