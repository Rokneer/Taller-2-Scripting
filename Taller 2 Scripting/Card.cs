using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_2_Scripting
{
    public abstract class Card
    {
        public string Name { get; protected set; }
        public int CostPoints { get; protected set; }
        public ERarity Rarity { get; set; }

        public Card(ERarity rarity, string name, int costPoints)
        {
            Name = name;
            CostPoints = costPoints;
            Rarity = rarity;
        }
    }
}
