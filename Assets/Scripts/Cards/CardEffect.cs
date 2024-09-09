using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Cards
{
    public class CardEffect: TurnBehaviour
    {
        public Card card;
        public KamenRider KamenRider;

        public override void OnActive()
        {
            base.OnActive();
            List<Card> list = new List<Card>();

            card.OnActive.Active(KamenRider,list);
        }

        public override void OnEndTurn()
        {
            base.OnEndTurn();
            List<Card> list = new List<Card>();
            card.OnEndTurn.Active(KamenRider, list);
        }

        public override void OnOponentEndTurn()
        {
            base.OnOponentEndTurn();
            List<Card> list = new List<Card>();
            card.OnOponentEndTurn.Active(KamenRider, list);
        }

        public override void OnOponentTurn()
        {
            base.OnOponentTurn();
            List<Card> list = new List<Card>();
            card.OnOponentTurn.Active(KamenRider, list);
        }

        public override void OnPlayerEndTurn()
        {
            base.OnPlayerEndTurn();
            List<Card> list = new List<Card>();
            card.OnPlayerEndTurn.Active(KamenRider, list);
        }

        public override void OnPlayerTurn()
        {
            base.OnPlayerTurn();
            List<Card> list = new List<Card>();
            card.OnPlayerTurn.Active(KamenRider, list);
        }

        public override void OnTable()
        {
            base.OnTable();
            List<Card> list = new List<Card>();
            card.OnTable.Active(KamenRider, list);
        }

        public override void OnTurn()
        {
            base.OnTurn();
            List<Card> list = new List<Card>();
            card.OnTurn.Active(KamenRider, list);
        }
    }
}
