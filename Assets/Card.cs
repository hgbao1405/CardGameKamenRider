using System;
using System.Collections.Generic;

namespace Assets
{
    public class Card:CardEvent
    {
        public int Id { get; set; }
        public string CardType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public float Value { get; set; } = 0;
        public bool IsHasCouter {  get; set; }=false;
        public Counter counter { get; set; } = null;
        public List<string> Keywords { get; set; }
        public int Formid { get; set; }
        public int Hp {  get; set; }
        public int Attack { get; set; }
    }

    public class Counter
    {
        public int Value { get; set; } = 0;
        public string Name { get; set; }
        public string Avatar { get; set; }

        public void MinusCouter(int value)
        {
            if(this.Value < value)
            {
                throw new Exception(string.Format("Không đủ điểm {0}", this.Name));
                return;
            }
            this.Value-= value;
            if(this.Value < 0)
            {
                this.Value = 0;
            }
        }

        public void AddCounter(int value)
        {
            if(value < 0)
            {
                value = 0;
            }
            this.Value += value;
        }
    }

    public enum StatusType
    {
        Poision,
        Stun,
        Burn,
    }
    public enum CardType
    {
        ContinousSpell,
        Spell,
        Monster
    }
    public enum EffectType
    {
        Attack,
        Heal,
        Form,
        Eqip
    }
}