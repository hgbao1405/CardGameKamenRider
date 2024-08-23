namespace Assets
{
    public class Card
    {
        public int Id { get; set; }
        public EffectType[] ListEffect { get; set; }
        public string CardType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public float Value { get; set; } = 0;
        public Form Form { get; set; }=null;
        public bool IsHasCouter {  get; set; }=false;
        public Counter counter { get; set; }
    }

    public class Counter
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
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