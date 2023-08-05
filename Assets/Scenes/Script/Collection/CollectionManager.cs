using UnityEngine;
using System;
using System.Collections.Generic;
using Enums;

[Serializable]
public class CollectionData
{
    public string ClassName;
    public string Name;
    public string Explain;
    public string Rank;
    public string ImageName;
}

[Serializable]
public class CollectionContainer
{
    public CollectionData[] Collections;
}

public class CollectionManager
{
    public List<CollectionClass> Collections;

    public CollectionManager()
    {
        Collections = new List<CollectionClass>();
        TextAsset jsonData = Resources.Load<TextAsset>("GameData/CollectionDataKorean");
        if (jsonData == null)
            Debug.Log("Collection json 파싱 실패!!");
        CollectionContainer container = JsonUtility.FromJson<CollectionContainer>(jsonData.text);
        foreach (var jsonCollection in container.Collections)
        {
            Type collectionType = Type.GetType(jsonCollection.ClassName);

            if (collectionType != null)
            {
                CollectionClass collection = (CollectionClass)Activator.CreateInstance(collectionType);
                collection.Name = jsonCollection.Name;
                collection.Explain = jsonCollection.Explain;
                collection.Rank = jsonCollection.Rank;
                collection.Sprite = Resources.Load<Sprite>(jsonCollection.ImageName);

                Collections.Add(collection);
            }
        }
    }

    private TextAsset GetPickScriptText()
    {
        switch ((ELangauge)Singleton.S.curLangIndex)
        {
            case ELangauge.EN:
                return Resources.Load<TextAsset>("GameData/CollectionDataEnglish");
            case ELangauge.KR:
                return Resources.Load<TextAsset>("GameData/CollectionDataKorean");
            default:
                return null;
        }
    }
    public void UpdateCollections()
    {
        for (int i = 0; i < Collections.Count; i++)
        {
            if (!UserInfo.instance.UserDataSet.BCollection[i] && Collections[i].IsComplete())
            {
                UserInfo.instance.UserDataSet.BCollection[i] = true;
            }
        }
    }
}