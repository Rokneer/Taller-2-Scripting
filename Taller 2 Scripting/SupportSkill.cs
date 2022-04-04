using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_2_Scripting
{
    internal abstract class SupportSkill : Card
    {
        public virtual int EffectPoints { get; protected set; }

        public SupportSkill(ERarity rarity, string name, int costPoints, int EP) : base(rarity, name, costPoints)
        {
            EffectPoints = (int)MathF.Abs(EP);
        }
    }
    internal class ReduceAP : SupportSkill
    {
        public ReduceAP(ERarity rarity, string name, int costPoints, int EP) : base(rarity, name, costPoints, EP)
        {
        }
    }
    internal class ReduceRP : SupportSkill
    {
        public ReduceRP(ERarity rarity, string name, int costPoints, int EP) : base(rarity, name, costPoints, EP)
        {
        }
    }
    internal class ReduceAll : SupportSkill
    {
        public ReduceAll(ERarity rarity, string name, int costPoints, int EP) : base(rarity, name, costPoints, EP)
        {
        }
    }
    internal class DestroyEquip : SupportSkill
    {
        //private Equip targetEquip = new Equip();
        internal Equip TargetEquip { get; private set; }
        public override int EffectPoints
        {
            get => base.EffectPoints;
            protected set => base.EffectPoints = 0;
        }
        public DestroyEquip(ERarity rarity, string name, int costPoints, int EP, Equip targetEquip) : base(rarity, name, costPoints, EP)
        {
            TargetEquip = targetEquip;
        }
    }
    internal class RestoreRP : SupportSkill
    {
        public RestoreRP(ERarity rarity, string name, int costPoints, int EP) : base(rarity, name, costPoints, EP)
        {
        }
    }
}
