using System;

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
        AccessoryLevel++;
        foreach ((var statIndex, var data) in EquipmentData.AccessoryUpgrade[AccessoryIndex][AccessoryLevel])
        {
            GameManager.instance.CharacterStats[statIndex] += data;
            if (statIndex == (int)Enums.Stat.Luck)
            {
                GameManager.instance.UpdateLuck(GameManager.instance.CharacterStats[statIndex]);
            }
        }
    }
}
