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

    public class ActiveListSkill : List<Skill>
    {
        public Mess Active(KamenRider kr,List<Card> Target)
        {
            Mess mess = new Mess();
            foreach(Skill skill in this)
            {
                mess=CardSkill.Active(kr, skill, Target);
                if (mess.Error)
                {
                    return mess;
                }
            }
            return mess;
        }
    }
}