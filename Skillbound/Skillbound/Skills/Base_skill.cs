using System;
using System.Collections.Generic;
using System.Text;

namespace Skillbound.Skills
{
    class Base_skill
    {
        public static Dictionary<Skill, Base_skill> skills;

        public static void Initialize()
        {
            skills = new Dictionary<Skill, Base_skill>()
            {
                { Skill.Dash, new Dash() },
            };

            Main.UpdateEvent += UpdateSkills;
        }

        public static void UpdateSkills()
        {
            foreach(Base_skill x in skills.Values)
            {
                x.Update();
            }
        }

        public string name;
        public float energyUsage;
        public GameValue cooldown;

        public Base_skill(string _name, float _energyUsage, GameValue _cooldown)
        {
            name = _name;
            energyUsage = _energyUsage;
            cooldown = _cooldown;
        }

        public virtual void Execute()
        {
            if (cooldown.Percent() >= 1f)
            {
                cooldown.AffectValue(0f);
            }
        }

        public virtual void Update()
        {
            cooldown.Regenerate();
        }

        public enum Skill
        {
            Dash
        }
    }
}
