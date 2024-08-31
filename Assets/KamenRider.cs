using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets
{
    public class KamenRider
    {
        public KamenRider(string name, float formMaxHP,
            float playerMaxHP, List<SlotCard> cardSlot, List<Form> forms, bool isHasCouter, Counter counter)
        {
            Name = name;
            FormMaxHP = formMaxHP;
            PlayerMaxHP = playerMaxHP;
            CurrentPlayerHP = playerMaxHP;
            CardSlot = cardSlot;
            this.forms = forms;
            IsHasCouter = isHasCouter;
            Counter = counter;
        }

        protected string Name { get; set; }

        public float FormMaxHP { get; private set; }
        public float PlayerMaxHP { get; private set; }

        public float CurrentFormHP { get; private set; } = 0;
        public float CurrentPlayerHP { get; private set; }

        public List<string> Avartars { get; private set; }

        public List<SlotCard> CardSlot { get; private set; }
        public List<Form> forms { get; private set; }

        public bool IsHasCouter {  get; private set; }
        public Counter Counter { get; private set; }

        public virtual void ChangeForm(int id) { }
        public virtual void ChangeForm(List<int> ids) { }

        public virtual string GetName() { return this.Name; }
        public virtual List<string> GetAvatars() { return new List<string>(); }
        public virtual float GetKickDamage() { return 0f; }
        public virtual float GetPunchDamage() { return 0f; }
        public virtual void TakeDamage(float damage,int typeHp=1) {
            if(damage < 0f) damage = 0f;
            switch(typeHp)
            {
                case 1:
                    CurrentFormHP -= damage;
                    if(CurrentFormHP < 0f)
                    {
                        TakeDamage(-CurrentFormHP, typeHp);
                        CurrentFormHP = 0f;
                    }
                    break;
                case 2:
                    CurrentPlayerHP -= damage;
                    if(CurrentPlayerHP < 0f)
                    {
                        CurrentPlayerHP = 0f;
                    }
                    break;
            }
        }
        public virtual void AddCounter(int counter) {
            if (IsHasCouter)
            {
                if (counter > 0)
                {
                    Counter.AddCounter(counter);
                }
            }
        }
        public virtual void UseCounter(int counter)
        {
            if (IsHasCouter)
            {
                if (counter > 0)
                {
                    Counter.MinusCouter(counter);
                }
            }
        }

        public virtual float GetSpeed()
        {
            return 0;
        }
    }
    public class CombinationRider : KamenRider
    {
        public List<Form> CurrentForm { get; private set; }
        //Num item in a form
        public int maxids { get; private set; }
        private bool equals(List<int> ids1,List<int> ids2)
        {
            if(ids1 == null || ids2 == null) return false;
            foreach(int id in ids1)
            {
                if (!ids2.Contains(id))
                {
                    return false;
                }
            }
            foreach (int id in ids2)
            {
                if (!ids1.Contains(id))
                {
                    return false;
                }
            }
            return true;
        }
        public CombinationRider(string name, float formMaxHP, float playerMaxHP,
            List<SlotCard> cardSlot, List<Form> forms, int maxids, bool isHasCouter, Counter counter) :
            base(name, formMaxHP, playerMaxHP, cardSlot, forms,isHasCouter,counter)
        {
            this.maxids = maxids;
        }

        public override void ChangeForm(List<int> ids) {
            if (ids.Count < maxids)
            {
                throw new Exception("Không đủ item để chuyển form");
            }
            Form comboform = this.forms.FirstOrDefault(x => equals(x.ids,ids));
            if (comboform != null)
            {
                this.CurrentForm = this.forms.Where(x => ids.Contains(x.Id)).ToList();
                this.CurrentForm.Add(comboform);
            }
            else
                this.CurrentForm = this.forms.Where(x =>ids.Contains(x.Id)).ToList();
        }

        public override string GetName()
        {
            List<string> names = new List<string>(); 
            Form form1 = CurrentForm.FirstOrDefault(x => x.ids != null);
            if (form1 != null)
            {
                return form1.FormName;
            }
            foreach (Form form in this.CurrentForm)
            {
                names.Add(form.FormName);
            }
            return this.Name + ": " + string.Join(" ", names);
        }

        public override float GetPunchDamage()
        {
            float damage = 0;
            foreach (Form form in this.CurrentForm)
            {
                damage+= form.PunchDamage;
            }
            return damage;
        }

        public override float GetKickDamage()
        {
            float damage = 0;
            foreach (Form form in this.CurrentForm)
            {
                damage += form.KickDamage;
            }
            return damage;
        }
        public override List<string> GetAvatars()
        {
            List<string> strings = new List<string>();
            Form form1 = CurrentForm.FirstOrDefault(x => x.ids != null);
            if (form1!=null)
            {
                strings.Add(form1.Avatar);
                return strings;
            }
            foreach (Form form in this.CurrentForm)
            {
                strings.Add(form.Avatar);
            }
            return strings;
        }

        public override float GetSpeed()
        {
            float speed = 0;
            foreach (Form form in this.CurrentForm)
            {
                speed += form.Speed;
            }
            return base.GetSpeed()+speed;
        }
    }
    public class NormalRider : KamenRider
    {
        public Form BaseForm { get; set; }
        public Form CurrentForm { get; private set; }

        public NormalRider(string name, float formMaxHP, float playerMaxHP,
            List<SlotCard> cardSlot, List<Form> forms,
            int idBaseForm, bool isHasCouter, Counter counter) :
            base(name, formMaxHP, playerMaxHP, cardSlot, forms,isHasCouter,counter)
        {
            CurrentForm = forms.First(x=>x.Id==idBaseForm);
        }

        public override void ChangeForm(int id)
        {
            var form = this.forms.FirstOrDefault(x=>x.Id==id);
            if (form != null)
            {
                this.CurrentForm = form;
            }
            else
            {
                throw new Exception("Can not find this form");
            }
        }

        public override string GetName()
        {
            return this.Name + ": " + string.Join(" ", CurrentForm.FormName);
        }

        public override float GetPunchDamage()
        {
            return this.CurrentForm.PunchDamage;
        }

        public override float GetKickDamage()
        {
            return this.CurrentForm.KickDamage;
        }
        public override void TakeDamage(float damage, int typeHp = 1)
        {
            base.TakeDamage(damage, typeHp);
            if (BaseForm.Id!=CurrentForm.Id && CurrentFormHP == 0)
            {
                this.CurrentForm = this.BaseForm;
            }
        }
        public override List<string> GetAvatars()
        {
            List<string> strings = new List<string>();
            strings.Add(CurrentForm.Avatar);
            return strings;
        }

        public override float GetSpeed()
        {
            float speed = CurrentForm.Speed;
            return base.GetSpeed() + speed;
        }
    }
    public class SlotCard
    {
        public float x;
        public float y;
        public string keyword;       // Lưu keyword của vị trí

        public SlotCard(float x, float y, string keyword)
        {
            this.x = x;
            this.y = y;
            this.keyword = keyword;
        }
        public SlotCard()
        {
            this.x = 0;
            this.y = 0;
            this.keyword = "";
        }
    }
}
public class Form
{
    public int Id { get; set; }
    public string FormName { get; set; }
    public string FormType { get; set; }
    public string Avatar { get; set; }
    public float FormMaxHP { get; set; }
    public float PunchDamage { get; set; }
    public float KickDamage { get; set; }
    public float Defense { get; set; } = 0;
    public List<int> ids { get; set; }
    public float Speed {  get; set; }   

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