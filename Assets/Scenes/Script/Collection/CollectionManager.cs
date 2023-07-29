using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class CollectionData
{
    public string ClassName;
    public string Name;
    public string Explain;
    public string Rank;
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

                Collections.Add(collection);
            }
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