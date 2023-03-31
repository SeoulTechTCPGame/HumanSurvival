using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [Header("#Main Info")]
    public CharacterType characterType;
    
    public int MaxHealth;//100hp
    public float Recovery;//0 HP/s
    public int Armor;//0 Armor
    public float MoveSpeed;//1
    public float Might;//1
    //public float MaxMight;//10
    public float Area;//1
    //public float MaxArea;//10
    public float ProjectileSpeed;//1
    //public float MaxProjectileSpeed;//5
    public float Duration;//1
    //public float MaxDuration;//5
    public int Amount;//0
    //public int MaxAmount;//10
    public float Cooldown;//1
    //public float MaxCooldown;//0.1
    public float Luck;//1
    public float Growth;//1
    public float Greed;//1
    public float Curse;//1
    public float MagnetBonus;//1 //Magnet = 30 * character bonus * PowerUp multiplier
    public int Revival;//0
    public int Reroll;//0
    public int Skip;//0
    public int Banish;//0
    
    public int Ommi;
    public int Reflection;
    public Weapon startingWeapon;
    //ToDo: 특수 능력 구현
    public string explain;
}
