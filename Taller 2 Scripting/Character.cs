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
        List<Equip> equipmentSlots = new List<Equip>();

        private int affinityBonus = 0;
        private int skillDebuff = 0;
        private int equipBuff = 0;

        private Character target = new Character();
        private Deck deck = new Deck();

        public int AttackPoints 
        {
            get => baseAttackPoints + affinityBonus + equipBuff - skillDebuff;
            set => baseAttackPoints = value; 
        }
        public int ResistPoints 
        { 
            get => baseResistPoints; 
            set => baseResistPoints = value; 
        }
        internal List<Equip> EquipmentSlots { get => equipmentSlots; set => equipmentSlots = value; }
        public EAffinity Affinity { get; private set; }

        public Character() : base()
        {
            AttackPoints = 1;
            ResistPoints = 1;
            Affinity = EAffinity.Knight;
            EquipmentSlots = equipmentSlots;
        }
        public Character(ERarity rarity, string name, int costPoints, int AP, int RP, EAffinity affinity) : base(rarity, name, costPoints)
        {
            AttackPoints = (int)MathF.Abs(AP);
            ResistPoints = (int)MathF.Abs(RP);

            if (affinity == EAffinity.All) Affinity = EAffinity.Knight;
            else Affinity = affinity;

            EquipmentSlots = equipmentSlots;           
        }

        public void SetTarget(Character character)
        {
            target = character;
            Console.WriteLine("Target set");
        }
        public void SetDeck(Deck deck)
        {
            this.deck = deck;
            Console.WriteLine("Deck set");
        }
        private void ModifyRP(int delta)
        {
            ResistPoints += delta;
        }
        public void ApplyHeal(int delta)
        {
            ModifyRP(delta);
        }
        public void ApplyDamage(int delta)
        {
            ModifyRP(-delta);
            if(ResistPoints <= 0)
            {
                ResistPoints = 0;
            }
        }
        public void CheckAffinity()
        {
            switch (Affinity)
            {
                case EAffinity.Knight:
                    if (target.Affinity is EAffinity.Mage)
                    {
                        affinityBonus = 1;
                        target.affinityBonus = -1;
                    }
                    else if (target.Affinity is EAffinity.Undead)
                    {
                        affinityBonus = -1;
                        target.affinityBonus = 1;
                    }
                    break;
                case EAffinity.Mage:
                    if (target.Affinity is EAffinity.Undead)
                    {
                        affinityBonus = 1;
                        target.affinityBonus = -1;
                    }
                    else if (target.Affinity is EAffinity.Knight)
                    {
                        affinityBonus = -1;
                        target.affinityBonus = 1;
                    }
                    break;
                case EAffinity.Undead:
                    if (target.Affinity is EAffinity.Knight)
                    {
                        affinityBonus = 1;
                        target.affinityBonus = -1;
                    }
                    else if (target.Affinity is EAffinity.Mage)
                    {
                        affinityBonus = -1;
                        target.affinityBonus = 1;
                    }
                    break;
                default: break;
            }
        }
        public void Battle()
        {
            CheckAffinity();

            ApplyDamage(target.AttackPoints); //Recibe daño
            Console.WriteLine("Current RP: " + ResistPoints);
            target.ApplyDamage(AttackPoints); //Causa daño
            Console.WriteLine("Enemy RP: " + target.ResistPoints);

            if (ResistPoints == 0) deck.DestroyCard(this, deck);
            if (target.ResistPoints == 0) target.deck.DestroyCard(target, target.deck);
            if (deck.CharacterDeck.Count == 0) GameOver();
        }
        public void UseSkill(SupportSkill skill)
        {
            if (skill is ReduceAP) target.skillDebuff = skill.EffectPoints;
            else if (skill is ReduceRP) target.ApplyDamage(skill.EffectPoints);
            else if (skill is ReduceAll)
            {
                target.skillDebuff = skill.EffectPoints;
                target.ApplyDamage(skill.EffectPoints);
            }
            else if (skill is DestroyEquip)
            {
                DestroyEquip destroyEquip = (DestroyEquip)skill;
                switch (destroyEquip.TargetEquip.TargetAttribute)
                {
                    case EAttributeType.AP:
                        {
                            equipBuff -= destroyEquip.TargetEquip.EffectPoints;
                            break;
                        }
                    case EAttributeType.RP:
                        {
                            ApplyDamage(destroyEquip.TargetEquip.EffectPoints);
                            if (target.ResistPoints == 0) target.deck.DestroyCard(target, target.deck);
                            break;
                        }
                    case EAttributeType.ALL:
                        {
                            equipBuff -= destroyEquip.TargetEquip.EffectPoints;
                            ApplyDamage(destroyEquip.TargetEquip.EffectPoints);
                            if (target.ResistPoints == 0) target.deck.DestroyCard(target, target.deck);
                            break;
                        }
                }
                deck.DestroyCard(equipmentSlots.ElementAt(1), target.deck);
                equipmentSlots.Remove(equipmentSlots.ElementAt(1));
                Console.WriteLine("Equipment broke!");
            }
            else if (skill is RestoreRP) ApplyHeal(skill.EffectPoints);
        }
        public void AddEquip(Equip equip)
        {
            if (equipmentSlots.Count() < 3)
            {
                if (equip.Affinity == Affinity || equip.Affinity == EAffinity.All)
                {
                    equipmentSlots.Add(equip);
                    switch (equip.TargetAttribute)
                    {
                        case EAttributeType.AP:
                            {
                                equipBuff += equip.EffectPoints;
                                Console.WriteLine("Equipment Added!");
                                Console.WriteLine("Current AP: " + AttackPoints);
                                break;
                            }
                        case EAttributeType.RP:
                            {
                                ApplyHeal(equip.EffectPoints);
                                Console.WriteLine("Equipment Added!");
                                Console.WriteLine("Current RP: " + ResistPoints);
                                break;
                            }
                        case EAttributeType.ALL:
                            {
                                equipBuff += equip.EffectPoints;
                                ApplyHeal(equip.EffectPoints);
                                Console.WriteLine("Equipment Added!");
                                Console.WriteLine("Current AP: " + AttackPoints);
                                Console.WriteLine("Current RP: " + ResistPoints);
                                break;
                            }
                    }
                }
                else Console.WriteLine("Wrong type of affinity");
            }
            else Console.WriteLine("Maximum amount of equipments reached");
        }
        public void GameOver()
        {
            Console.WriteLine("You have no more characters in your deck");
            Console.WriteLine("Game Over");
        }
    }
}
