using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace Assets
{
    public class Player
    {
        public KamenRider KamenRider;
        public TextMeshProUGUI Title;
        public Image[] Avatar;
        public HealBar Hp;
        public List<Card> Deck;
        public List<Card> Hand;


    }
}
