using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public static class Seeding
    {
        public static KamenRider KamenRiderOOOSeed()
        {
            List<Card> cards = new List<Card>();
            List<Form> forms = new List<Form>();
            forms=new List<Form>() {
                new Form(){Id = 1,FormName="Taka",FormType= "MultiComponent",},
                new Form(){Id = 2,FormName="Tora",FormType= "MultiComponent",},
                new Form(){Id = 3,FormName="Batta",FormType= "MultiComponent",},
                new Form(){Id = 4,FormName="Tatoba",FormType= "Combination",ids=new List<int>{1,2,3}, }
            };

            List <SlotCard> listSlot = new List<SlotCard>();
            listSlot.Add(new SlotCard(-226, 300, "CoreHeadMedal"));
            listSlot.Add(new SlotCard(-155, 300, "CoreArmMedal"));
            listSlot.Add(new SlotCard(-84, 300, "CoreLegMedal"));

            KamenRider kamenRiders = new CombinationRider("OOO",0,1000f,listSlot,forms,3);
            kamenRiders.ChangeForm(new List<int> { 1, 2, 3 });

            return kamenRiders;
        }

        public static List<Card> SeedCardsOOO()
        {
            List<Card> cards = new List<Card>() {
                new Card(){Id = 1,Name = "Scanner",CardType = "Spell",OnActive=new List<Skill>() { 
                    new Skill("UseCouter",3),
                    new Skill("ChangeForm",0)} 
                },
                new Card(){Id = 2,Name = "Taka",CardType = "ContinousSpell",Keywords = new List<string>() {
                    "CoreHeadMedal", "CoreMedal" 
                },Formid=1},
                new Card(){Id = 3,Name = "Tora",CardType = "ContinousSpell",Keywords = new List<string>() {
                    "CoreArmMedal", "CoreMedal" 
                },Formid=2},
                new Card(){Id = 4,Name = "Batta",CardType = "ContinousSpell",Keywords = new List<string>() {
                    "CoreLegMedal", "CoreMedal"
                },Formid=3},
            };
            return cards;
        }
    }
}
