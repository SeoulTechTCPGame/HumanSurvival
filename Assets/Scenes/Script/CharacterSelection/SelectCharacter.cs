using UnityEngine;
using Enums;
public class SelectCharacter : MonoBehaviour
{
    public CharacterType Charname;
    public void OnClickCharacter()
    {
        DataManager.instance.CurrentCharcter=Charname;
    }
}