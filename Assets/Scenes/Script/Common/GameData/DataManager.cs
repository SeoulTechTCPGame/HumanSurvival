using UnityEngine;
using Enums;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public ECharacterType CurrentCharcter;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}