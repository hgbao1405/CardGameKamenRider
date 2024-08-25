using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;

namespace Assets
{
    public static class CardSkill
    {
        public static Mess Active(KamenRider player,Skill skill,List<Card> Target)
        {
            switch(skill.Name) {
                case "UseCounter":
                    return UseCounter(player, Target, skill.value);
                case "AddCounter":
                    return AddCounter(player, Target, skill.value);
                case "ChangeForm":
                    return ChangeForm(player,Target, skill.value);
                default:
                    return new Mess() { Error = true,Message="Không tìm thấy hiệu ứng này" };
            }
        }

        private static Mess AddCounter(KamenRider player, List<Card> target, int value)
        {
            Mess mess = new Mess();
            foreach (Card card in target)
            {
                if (card.IsHasCouter)
                {
                    card.counter.AddCounter(value);
                }
                else
                {
                    mess.Error = true;
                    mess.Message = "Thẻ này không có bộ đếm";
                }
            }
            return mess;
        }

        private static Mess UseCounter(KamenRider player, List<Card> target, int value)
        {
            Mess mess = new Mess();
            foreach (Card card in target)
            {
                if (card.IsHasCouter)
                {
                    card.counter.MinusCouter(value);
                }
                else
                {
                    mess.Error = true;
                    mess.Message = "Thẻ này không có bộ đếm";
                }
            }
            return mess;
        }

        private static Mess ChangeForm(KamenRider player, List<Card> target, int value)
        {
            Mess mess = new Mess();
            if(player is CombinationRider rider) {
                List<int> ids = new List<int>();

                foreach (Card card in target)
                {
                    if (card.Formid == null|| card.Formid == 0)
                    {
                        mess.Error = true;
                        mess.Message="Không dủ nguyên liệu chuyển form này";
                        return mess;
                    }
                    ids.Add(card.Formid);
                }
                rider.ChangeForm(ids);
            }
            else if (player is NormalRider nRider)
            {
                nRider.ChangeForm(value);
            }
            return mess;
        }

    }
    public class Skill
    {
        public Skill(string name, int value)
        {
            Name = name;
            this.value = value;
        }

        public string Name { get; set; }
        public int value { get; set; }
    }
    public class Mess
    {
        public bool Error {  get; set; }
        public string Message { get; set; }
    }
}