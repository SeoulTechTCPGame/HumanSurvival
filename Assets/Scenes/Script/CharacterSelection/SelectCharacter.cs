using UnityEngine;
using Enums;
public class SelectCharacter : MonoBehaviour
{
    public CharacterType charname;
    public void OnClickCharacter()
    {
        DataManager.instance.currentCharcter=charname;
    }
}
