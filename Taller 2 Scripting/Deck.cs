using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_2_Scripting
{
    internal class Deck
    {
        private int maxCostPoints;
        List<Character> characterDeck = new List<Character>();
        List<Equip> equipDeck = new List<Equip>();
        List<SupportSkill> skillDeck = new List<SupportSkill>();

        public int CostPoints { get => maxCostPoints; set => maxCostPoints = value; }
        internal List<Character> CharacterDeck { get => characterDeck; set => characterDeck = value; }
        internal List<Equip> EquipDeck { get => equipDeck; set => equipDeck = value; }
        internal List<SupportSkill> SkillDeck { get => skillDeck; set => skillDeck = value; }

        public Deck()
        {
            CostPoints = 20;
            CharacterDeck = characterDeck;
            EquipDeck = equipDeck;
            SkillDeck = skillDeck;
        }
        public Deck(int maxCostPoints)
        {
            CostPoints = maxCostPoints;

            #pragma warning disable CS8601 // Posible asignación de referencia nula
            if (characterDeck == null) CharacterDeck = characterDeck;
            else characterDeck.Clear(); CharacterDeck = characterDeck;

            if (equipDeck == null) EquipDeck = equipDeck;
            else equipDeck.Clear(); EquipDeck = equipDeck;

            if (skillDeck == null) SkillDeck = skillDeck;
            else skillDeck.Clear(); SkillDeck = skillDeck;
            #pragma warning restore CS8601 // Posible asignación de referencia nula

        }

        public void AddCard(Card card, Deck deck)
        {
            if(card is Character)
            {
                Character character = (Character)card;
                if (CostPoints - character.CostPoints >= 0 && deck.CharacterDeck.Count < 5)
                {
                    deck.CharacterDeck.Add(character);
                    CostPoints -= character.CostPoints;
                }
                else if (deck.CharacterDeck.Count >= 5) Console.WriteLine("Character Deck is full");
                else if (CostPoints - character.CostPoints < 0) Console.WriteLine("Not enough Cost Points");
            }
            else if (card is Equip)
            {
                Equip equip = (Equip)card;
                if (CostPoints - equip.CostPoints >= 0 && deck.EquipDeck.Count < 10)
                {
                    deck.EquipDeck.Add(equip);
                    CostPoints -= equip.CostPoints;
                }
                else if (deck.EquipDeck.Count >= 10) Console.WriteLine("Equip Deck is full");
                else if (CostPoints - equip.CostPoints < 0) Console.WriteLine("Not enough Cost Points");
            }
            else if (card is SupportSkill)
            {
                SupportSkill skill = (SupportSkill)card;
                if (CostPoints - skill.CostPoints >= 0 && deck.SkillDeck.Count < 5)
                {
                    deck.SkillDeck.Add(skill);
                    CostPoints -= skill.CostPoints;
                }
                else if (deck.SkillDeck.Count >= 5) Console.WriteLine("Support skill Deck is full");
                else if (CostPoints - skill.CostPoints < 0) Console.WriteLine("Not enough Cost Points");
            }
        }
        public void DestroyCard(Card card, Deck deck)
        {
            if (card is Character)
            {
                Character character = (Character)card;
                deck.characterDeck.Remove(character);
            }
            else if (card is Equip)
            {
                Equip equip = (Equip)card;
                deck.equipDeck.Remove(equip);
            }
            else if (card is SupportSkill)
            {
                SupportSkill skill = (SupportSkill)card;
                deck.skillDeck.Remove(skill);
            }
        }
    }
}
