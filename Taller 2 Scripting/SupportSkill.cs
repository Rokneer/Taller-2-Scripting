using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_2_Scripting
{
    internal class SupportSkill : Card
    {
        private int baseEffectPoints;
        public int EffectPoints
        {
            get => baseEffectPoints;
            set => baseEffectPoints = value;
        }
        public EEffectType TargetEffect { get; private set; }

        public SupportSkill(ERarity rarity, string name, int costPoints, int effectPoints, EEffectType targetEffect) : base(rarity, name, costPoints)
        {
            EffectPoints = effectPoints;
            TargetEffect = targetEffect;
        }
    }
}
