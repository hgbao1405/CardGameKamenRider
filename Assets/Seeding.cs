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
            forms = new List<Form>() {
                new Form(){Id = 1,FormName="Taka",FormType= "MultiComponent",},
                new Form(){Id = 2,FormName="Tora",FormType= "MultiComponent",},
                new Form(){Id = 3,FormName="Batta",FormType= "MultiComponent",Speed=40 },
                new Form(){Id = 4,FormName="Tatoba",FormType= "Combination",ids=new List<int>{1,2,3}, }
            };

            List <SlotCard> listSlot = new List<SlotCard>();
            listSlot.Add(new SlotCard(1f, 1f, "CoreHeadMedal"));
            listSlot.Add(new SlotCard(2f, 1f, "CoreArmMedal"));
            listSlot.Add(new SlotCard(3f, 1f, "CoreLegMedal"));

            KamenRider kamenRiders = new CombinationRider("OOO",0,1000f,listSlot,forms,3,true,new Counter {
                Name="Cell Medal",Avatar= "Assets/Resources/Images/OOO/Couter/cellmedal.psd" });
            kamenRiders.ChangeForm(new List<int> { 1, 2, 3 });

            return kamenRiders;
        }

        public static List<Card> SeedCardsOOO()
        {
            List<Card> cards = new List<Card>() {
                new Card(){Id = 1,Name = "Scanner",CardType = "Spell",OnActive=new List<Skill>() {
                    new Skill("UseCouter",3),
                    new Skill("ChangeForm",0)},
                    Description="Khi các core medal để đúng vị trí, sử dụng 3 cell medal để kích hoạt chuyển đổi form cho OOO theo các core medal"
                },
                new Card(){Id = 2,Name = "Taka",CardType = "ContinousSpell",Keywords = new List<string>() {
                    "CoreHeadMedal", "CoreMedal", "OOO-Medal"
                },Formid=1,Avatar="Images/OOO/Carrd/Taka"},
                new Card(){Id = 3,Name = "Tora",CardType = "ContinousSpell",Keywords = new List<string>() {
                    "CoreArmMedal", "CoreMedal" ,"OOO-Medal"
                },Formid=2,Avatar="Images/OOO/Carrd/Batta"},
                new Card(){Id = 4,Name = "Batta",CardType = "ContinousSpell",Keywords = new List<string>() {
                    "CoreLegMedal", "CoreMedal", "OOO-Medal"
                },Formid=3,Avatar="Images/OOO/Carrd/Tora"},
            };
            return cards;
        }
    }
}
