using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.System
{
    public class Stat
    {
        public int Might;
        public int Armor;
        public int MaxHealth;
        public int Recovery;
        public int Cooldown;
        public int Area;
        public int ProjectileSpeed;
        public int Duration;
        public int Amount;
        public int MoveSpeed;
        public int Magnet;
        public int Luck;
        public int Growth;
        public int Greed;
        public int Curse;
        public int Revival;
        public int Reroll;
        public int Skip;
        public int Banish;
        public int Ommi;

        public Stat()
        {
            Reset();
        }


        public void Reset()
        {
            Might = 0;
            Armor = 0;
            MaxHealth = 0;
            Recovery = 0;
            Cooldown = 0;
            Armor= 0;
            ProjectileSpeed= 0;
            Duration = 0;
            Amount= 0;
            MoveSpeed= 0;
            Magnet= 0;
            Luck= 0;
            Growth = 0;
            Greed= 0; 
            Curse = 0;
            Revival= 0;
            Reroll= 0;
            Skip= 0;
            Banish= 0;
            Ommi= 0;
        }
    }
}
