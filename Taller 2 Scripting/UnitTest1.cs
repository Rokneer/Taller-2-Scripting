using NUnit.Framework;
using System.Linq;

namespace Taller_2_Scripting
{
    public class Tests
    {
        [Test]
        public void TestAddCards()
        {
            Deck deck = new Deck(20);
            //Adds a Character to the Deck
            Character character = new Character(ERarity.Common, "Juan", 5, 2, 3, EAffinity.Knight);
            deck.AddCard(character, deck);

            Assert.AreEqual(15, deck.CostPoints);

            //Adds an Equip to the Deck
            Equip equip = new Equip(ERarity.Rare, "Plancha", 3, 8, EAttributeType.AP, EAffinity.All);
            deck.AddCard(equip, deck);

            Assert.AreEqual(12, deck.CostPoints);

            //Adds a Skill to the Deck
            SupportSkill skill = new ReduceAll(ERarity.UltraRare, "Planchar", 10, 10);
            deck.AddCard(skill, deck);

            Assert.AreEqual(2, deck.CostPoints);

            //Fails to add a Character to the Deck because the cost is too high
            Character character2 = new Character(ERarity.UltraRare, "Super Juan", 15, 12, 13, EAffinity.Knight);
            deck.AddCard(character2, deck);
            Assert.False(deck.CharacterDeck.Contains(character2));
            Assert.AreEqual(2, deck.CostPoints);
        }

        [Test]
        public void TestDeckLimit()
        {
            Deck deck = new Deck(20);
            Character character = new Character(ERarity.Common, "Juan", 1, 2, 3, EAffinity.Knight);
            deck.AddCard(character, deck);
            deck.AddCard(character, deck);
            deck.AddCard(character, deck);
            deck.AddCard(character, deck);
            deck.AddCard(character, deck);
            //Checks if there are 5 Characters in the Deck
            Assert.AreEqual(5, deck.CharacterDeck.Count);

            deck.AddCard(character, deck);
            //Checks if there are 5 Characters in the Deck after trying to add a sixth one
            Assert.AreEqual(5, deck.CharacterDeck.Count);

            Equip equip = new Equip(ERarity.Rare, "Plancha", 1, 8, EAttributeType.AP, EAffinity.All);
            deck.AddCard(equip, deck);
            deck.AddCard(equip, deck);
            deck.AddCard(equip, deck);
            deck.AddCard(equip, deck);
            deck.AddCard(equip, deck);
            deck.AddCard(equip, deck);
            deck.AddCard(equip, deck);
            deck.AddCard(equip, deck);
            deck.AddCard(equip, deck);
            deck.AddCard(equip, deck);
            //Checks if there are 10 Equips in the Deck
            Assert.AreEqual(10, deck.EquipDeck.Count);
            deck.AddCard(equip, deck);

            //Checks if there are 10 Equips in the Deck after trying to add a eleventh one
            Assert.AreEqual(10, deck.EquipDeck.Count);

            SupportSkill skill = new ReduceAll(ERarity.UltraRare, "Planchar", 1, 10);
            deck.AddCard(skill, deck);
            deck.AddCard(skill, deck);
            deck.AddCard(skill, deck);
            deck.AddCard(skill, deck);
            deck.AddCard(skill, deck);
            //Checks if there are 5 Skills in the Deck
            Assert.AreEqual(5, deck.SkillDeck.Count);

            deck.AddCard(skill, deck);
            //Checks if there are 5 Skills in the Deck after trying to add a sixth one
            Assert.AreEqual(5, deck.SkillDeck.Count);
        }
        [Test]
        public void TestAddEquip()
        {
            Character character = new Character(ERarity.Common, "Juan", 1, 2, 5, EAffinity.Knight);
            Equip equip1 = new Equip(ERarity.Rare, "Olla", 1, 5, EAttributeType.RP, EAffinity.All);
            Equip equip2 = new Equip(ERarity.Rare, "Plancha", 1, 8, EAttributeType.AP, EAffinity.All);
            Equip equip3 = new Equip(ERarity.Rare, "Banano", 1, 2, EAttributeType.ALL, EAffinity.All);

            character.AddEquip(equip1); //Increases RP
            character.AddEquip(equip2); //Increases AP
            character.AddEquip(equip3); //Increases RP and AP

            Assert.AreEqual(12, character.ResistPoints);
            Assert.AreEqual(12, character.AttackPoints);
        }
        [Test]
        public void TestEquipLimit()
        {
            Character character = new Character(ERarity.Common, "Juan", 1, 2, 5, EAffinity.Knight);
            Equip equip1 = new Equip(ERarity.Rare, "Cuchara", 1, 2, EAttributeType.RP, EAffinity.Knight);

            character.AddEquip(equip1);
            character.AddEquip(equip1);
            character.AddEquip(equip1);
            //Checks if there are 3 Equips in the Equipment Slots
            Assert.AreEqual(3, character.EquipmentSlots.Count);
            character.AddEquip(equip1);
            //Checks if there are 3 Equips in the Equipment Slots fter trying to add a fourth one
            Assert.AreEqual(3, character.EquipmentSlots.Count);
        }
        [Test]
        public void TestEquipAffinity()
        {
            Character character = new Character(ERarity.Common, "Juan", 1, 2, 5, EAffinity.Knight);
            Equip equip = new Equip(ERarity.Rare, "Delantal", 1, 6, EAttributeType.ALL, EAffinity.Mage);

            character.AddEquip(equip); //Fails to add equipment because of Affinity

            Assert.AreEqual(5, character.ResistPoints);
            Assert.AreEqual(2, character.AttackPoints);
        }
        [Test]
        public void TestSupportSkillReduceAP()
        {
            Character player = new Character(ERarity.Common, "Juan", 1, 12, 5, EAffinity.Knight);
            Character enemy = new Character(ERarity.Common, "Jorge", 1, 10, 8, EAffinity.Undead);

            SupportSkill skill1 = new ReduceAP(ERarity.Rare, "Mojar", 2, 4);

            player.SetTarget(enemy);
            player.UseSkill(skill1);
            Assert.AreEqual(6, enemy.AttackPoints);
        }
        [Test]
        public void TestSupportSkillReduceRP()
        {
            Character player = new Character(ERarity.Common, "Juan", 1, 12, 5, EAffinity.Knight);
            Character enemy = new Character(ERarity.Common, "Jorge", 1, 10, 8, EAffinity.Undead);

            SupportSkill skill2 = new ReduceRP(ERarity.SuperRare, "Quemar", 5, 5);

            player.SetTarget(enemy);
            player.UseSkill(skill2);
            Assert.AreEqual(3, enemy.ResistPoints);
        }
        [Test]
        public void TestSupportSkillReduceALL()
        {
            Character player = new Character(ERarity.Common, "Juan", 1, 12, 5, EAffinity.Knight);
            Character enemy = new Character(ERarity.Common, "Jorge", 1, 10, 8, EAffinity.Undead);

            SupportSkill skill3 = new ReduceAll(ERarity.UltraRare, "Planchar", 10, 10);

            player.SetTarget(enemy);
            player.UseSkill(skill3);
            Assert.AreEqual(0, enemy.AttackPoints);
            Assert.AreEqual(0, enemy.ResistPoints);
        }
        [Test]
        public void TestSupportSkillDestroyEquipment()
        {
            Deck playerDeck = new Deck(20);
            Deck enemyDeck = new Deck(20);
            Character player = new Character(ERarity.Common, "Juan", 1, 12, 5, EAffinity.Knight);
            Character enemy = new Character(ERarity.Common, "Jorge", 1, 10, 8, EAffinity.Undead);

            player.SetTarget(enemy);
            player.SetDeck(playerDeck);
            playerDeck.AddCard(player, playerDeck);

            enemy.SetDeck(enemyDeck);
            enemyDeck.AddCard(enemy, enemyDeck);

            Equip equip = new Equip(ERarity.Rare, "Cuchara", 1, 2, EAttributeType.RP, EAffinity.All);
            SupportSkill skill4 = new DestroyEquip(ERarity.Rare, "Borrar", 7, 0, enemy.EquipmentSlots.ElementAt(1));

            player.UseSkill(skill4);
            enemy.AddEquip(equip);

            Assert.False(enemy.EquipmentSlots.Contains(equip));
        }
        [Test]
        public void TestSupportSkillRestoreRP()
        {
            Character player = new Character(ERarity.Common, "Juan", 1, 12, 5, EAffinity.Knight);

            SupportSkill skill5 = new RestoreRP(ERarity.Common, "Comer", 1, 2);

            player.UseSkill(skill5);
            Assert.AreEqual(7, player.ResistPoints);
        }
        [Test]
        public void TestBattle()
        {
            Deck playerDeck = new Deck(20);
            Deck enemyDeck = new Deck(20);
            Character player = new Character(ERarity.Common, "Juan", 1, 4, 5, EAffinity.Knight);
            Character enemy = new Character(ERarity.Common, "Jorge", 1, 2, 8, EAffinity.Knight);

            player.SetTarget(enemy);
            player.SetDeck(playerDeck);
            playerDeck.AddCard(player, playerDeck);

            enemy.SetDeck(enemyDeck);
            enemyDeck.AddCard(enemy, enemyDeck);

            player.Battle();

            Assert.AreEqual(3, player.ResistPoints);
            Assert.AreEqual(4, enemy.ResistPoints);
        }
        [Test]
        public void TestBattlePositiveAffinity()
        {
            Deck playerDeck = new Deck(20);
            Deck enemyDeck = new Deck(20);
            Character player = new Character(ERarity.Common, "Juan", 1, 4, 5, EAffinity.Knight);
            Character enemy = new Character(ERarity.Common, "Jorge", 1, 2, 8, EAffinity.Undead);

            player.SetTarget(enemy);
            player.SetDeck(playerDeck);
            playerDeck.AddCard(player, playerDeck);

            enemy.SetDeck(enemyDeck);
            enemyDeck.AddCard(enemy, enemyDeck);

            player.Battle();

            Assert.AreEqual(4, player.ResistPoints);
            Assert.AreEqual(3, enemy.ResistPoints);
        }
        [Test]
        public void TestBattleNegativeAffinity()
        {
            Deck playerDeck = new Deck(20);
            Deck enemyDeck = new Deck(20);
            Character player = new Character(ERarity.Common, "Juan", 1, 4, 5, EAffinity.Knight);
            Character enemy = new Character(ERarity.Common, "Jorge", 1, 2, 8, EAffinity.Mage);

            player.SetTarget(enemy);
            player.SetDeck(playerDeck);
            playerDeck.AddCard(player, playerDeck);

            enemy.SetDeck(enemyDeck);
            enemyDeck.AddCard(enemy, enemyDeck);

            player.Battle();

            Assert.AreEqual(2, player.ResistPoints);
            Assert.AreEqual(5, enemy.ResistPoints);
        }
        [Test]
        public void TestPlayerLose()
        {
            Deck playerDeck = new Deck(20);
            Deck enemyDeck = new Deck(20);
            Character player = new Character(ERarity.Common, "Juan", 1, 4, 5, EAffinity.Knight);
            Character enemy = new Character(ERarity.Common, "Jorge", 1, 4, 8, EAffinity.Mage);

            player.SetTarget(enemy);
            player.SetDeck(playerDeck);
            playerDeck.AddCard(player, playerDeck);

            enemy.SetDeck(enemyDeck);
            enemyDeck.AddCard(enemy, enemyDeck);

            player.Battle();

            Assert.AreEqual(0,playerDeck.CharacterDeck.Count);
        }
    }
}