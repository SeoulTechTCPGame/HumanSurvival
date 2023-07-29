using UnityEngine;
using System;
using System.Collections.Generic;

public class AchievementData
{
    public string ClassName;
    public string Explain;
    public string Obtain;
}

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> Achievements;

    private void Awake()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("GameData/ItemExplainDataKorean");
        AchievementData[] jsonAchievements = JsonUtility.FromJson<AchievementData[]>(jsonData.text);
        foreach (var jsonAchievement in jsonAchievements)
        {
            Type achievementType = Type.GetType(jsonAchievement.ClassName);

            if (achievementType != null)
            {
                Achievement achievement = (Achievement)Activator.CreateInstance(achievementType);
                achievement.Explain = jsonAchievement.Explain;

                Achievements.Add(achievement);
            }
        }
    }

    public void UpdateAchievements()
    {
        for(int i = 0; i < Achievements.Count; i++) 
        {
            if (!UserInfo.instance.UserDataSet.BAchievements[i] && Achievements[i].IsComplete())
            {
                UserInfo.instance.UserDataSet.BAchievements[i] = true;
                Achievements[i].EarnRewards();
            }
        }
        UserDataManager.instance.SaveData();
    }
}