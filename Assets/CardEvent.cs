using System.Collections.Generic;
using Unity.VisualScripting;

namespace Assets
{
    public class CardEvent
    {
        public List<Skill> OnActive         { get; set; } = new List<Skill>();
        public List<Skill> OnTable          { get; set; } = new List<Skill>();
        public List<Skill> OnPlayerTurn     { get; set; } = new List<Skill>();
        public List<Skill> OnOponentTurn    { get; set; } = new List<Skill>();
        public List<Skill> OnPlayerEndTurn  { get; set; } = new List<Skill>();
        public List<Skill> OnOponentEndTurn { get; set; } = new List<Skill>();
        public List<Skill> OnTurn           { get; set; } = new List<Skill>();
        public List<Skill> OnEndTurn        { get; set; } = new List<Skill>();
    }

    public static class SkillExtensions
    {
        public static Mess Active(this List<Skill> skills, KamenRider kr, List<Card> targetCards)
        {
            Mess mess = new Mess();
            foreach (Skill skill in skills)
            {
                mess = CardSkill.Active(kr, skill, targetCards);
                if (mess.Error)
                {
                    return mess;
                }
            }
            return mess;
        }
    }
}