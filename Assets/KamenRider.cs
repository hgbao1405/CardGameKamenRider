using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class KamenRider
    {
        public string Name { get; private set; }
        public string kamenRiderType { get; private set; }
        public float FormMaxHP { get; private set; }
        public float PlayerMaxHP { get; private set; }

        public float CurrentFormHP { get; private set; } = 0;
        public float CurrentPlayerHP { get; private set; }

        public List<Card> Deck { get; private set; }
        public List<Form> CurrentForm { get; private set; }
        public List<Form> BaseForm { get; private set; }

        public KamenRider(List<Card> deck, List<Form> _BaseForm, string type)
        {
            BaseForm = _BaseForm;
            CurrentForm = _BaseForm;
            Name = GetName(_BaseForm,type);
            kamenRiderType= type;
            FormMaxHP = GetFormMaxHP(_BaseForm, type);
            PlayerMaxHP = 100f;

            CurrentFormHP = FormMaxHP;
            CurrentPlayerHP = PlayerMaxHP;

            Deck = deck;
        }

        private float GetFormMaxHP(List<Form> baseForm, string type)
        {
            float HP = 0f;
            if (type == KamenRiderType.Combination.ToString())
                foreach (Form form in baseForm)
                {
                    HP += form.FormMaxHP;
                }
            else
            {
                HP = baseForm[0].FormMaxHP;
            }
            return HP;
        }

        private float GetPunchDamage(List<Form> baseForm, string type)
        {
            float HP = 0f;
            if (type == KamenRiderType.Combination.ToString())
                foreach (Form form in baseForm)
                {
                    HP += form.PunchDamage;
                }
            else
            {
                HP = baseForm[0].PunchDamage;
            }
            return HP;
        }

        private float GetKickDamage(List<Form> baseForm, string type)
        {
            float HP = 0f;
            if (type == KamenRiderType.Combination.ToString())
                foreach (Form form in baseForm)
                {
                    HP += form.KickDamage;
                }
            else
            {
                HP = baseForm[0].KickDamage;
            }
            return HP;
        }

        private string GetName(List<Form> baseForm, string type)
        {
            string Name = "";
            if (type == KamenRiderType.Combination.ToString())
                foreach (Form form in baseForm)
                {
                    Name += form.FormName;
                }
            else
            {
                Name = baseForm[0].FormName;
            }
            return Name;
        }

        public List<string> GetAvatar()
        {
            List<string> avatar = new List<string>();
            foreach (Form form in BaseForm)
            {
                avatar.Add(form.Avatar);
            }
            return avatar;
        }

        // Phương thức tấn công, nhận sát thương và hồi phục
        public void AttackPunch(KamenRider target, int hpType)
        {
            float damage = GetPunchDamage(CurrentForm, kamenRiderType);
            target.TakeDamage(damage, hpType);
        }
        // Phương thức tấn công, nhận sát thương và hồi phục
        public void AttackKick(KamenRider target, int hpType)
        {
            float damage = GetKickDamage(CurrentForm, kamenRiderType);
            target.TakeDamage(damage, hpType);
        }

        public void TakeDamage(float damage, int hpType = 2)
        {
            foreach (Form form in CurrentForm)
            {
                form.minusDamage(ref damage);
            }
            switch (hpType)
            {
                case 1:
                    CurrentPlayerHP -= damage;
                    break;
                case 2:
                    if (CurrentFormHP > 0)
                    {
                        CurrentFormHP -= damage;
                        if (CurrentFormHP < 0)
                        {
                            this.TakeDamage(-CurrentFormHP, 1);
                            CurrentFormHP = 0;
                            CurrentForm = BaseForm;
                        }
                    }
                    else
                    {
                        this.TakeDamage(damage, 1);
                    }
                    break;
            }

        }

        public void Heal(float amount, int hpType = 1)
        {
            switch (hpType)
            {
                case 1:
                    CurrentPlayerHP += amount;
                    if (CurrentPlayerHP > PlayerMaxHP) CurrentPlayerHP = PlayerMaxHP;
                    break;
                case 2:
                    CurrentFormHP += amount;
                    if (CurrentFormHP > FormMaxHP) CurrentFormHP = FormMaxHP;
                    break;
            }
        }

        // Phương thức sử dụng thẻ bài
        public void UseCard(Card card, KamenRider target)
        {
            foreach (CardType type in card.Type)
            {
                // Logic sử dụng thẻ bài, ví dụ thẻ bài tấn công hoặc phòng thủ
                if (type == CardType.Attack)
                {
                    target.TakeDamage(card.Value);
                }
                else if (type == CardType.Heal)
                {
                    Heal(card.Value);
                }
            }

        }

        public Card DrawCard()
        {
            if (Deck.Count == 0) return null;
            Card drawnCard = Deck[0];
            Deck.RemoveAt(0);
            return drawnCard;
        }


        // Phương thức kiểm tra trạng thái bị đánh bại
        public bool IsDefeated()
        {
            return CurrentPlayerHP <= 0 && CurrentPlayerHP
                <= 0;
        }

        // Phương thức đổi hình dạng
        public void ChangeForm(List<Form> newForm)
        {
            // Logic đổi hình dạng
            this.CurrentForm = newForm;
            this.Name = GetName(newForm,kamenRiderType);
            FormMaxHP = GetFormMaxHP(newForm,kamenRiderType);
            CurrentFormHP = FormMaxHP;
        }
    }
}
public class Form
{
    public string FormName { get; set; }
    public string FormType { get; set; }
    public string Avatar { get; set; }
    public float FormMaxHP { get; set; }
    public float PunchDamage { get; set; }
    public float KickDamage { get; set; }
    public float Defense { get; set; } = 0;

    public void minusDamage(ref float damage)
    {
        damage -= (float)this.Defense;
    }
}

public enum FormType
{
    Combination,
    MultiComponent,
    Base,
    Final
}
public enum KamenRiderType
{
    Combination,
    Normal
}