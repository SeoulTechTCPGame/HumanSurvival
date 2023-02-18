using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory
{
    public int AccessoryIndex;
    public int AccessoryLevel;
    public int AccessoryMaxLevel;

    public EquipmentData EquipmentData;
    public Accessory(int accessoryIndex)
    {
        AccessoryIndex = accessoryIndex;
        AccessoryLevel = 0;
        AccessoryMaxLevel = EquipmentData.AccessoriesMaxLevel[accessoryIndex];
    }

    public bool IsMaster()
    {
        return AccessoryLevel == AccessoryMaxLevel;
    }
    public void Upgrade()
    {
        Character character = GameObject.Find("Player").GetComponent<Character>();
        AccessoryLevel++;
        foreach ((var statIndex, var data) in EquipmentData.AccessoryUpgrade[AccessoryIndex][AccessoryLevel])
        {
            character.CharacterStats[statIndex] += data;
            if (statIndex == (int)Enums.Stat.Luck)
            {
                character.UpdateLuck(character.CharacterStats[statIndex]);
            }
        }
    }
}
