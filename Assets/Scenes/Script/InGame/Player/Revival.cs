using UnityEngine;

public class Revival : MonoBehaviour
{
    public void DoRevival()
    {   
        GameManager.instance.Character.RevivalHp();
        GameManager.instance.RevivalPanel.SetActive(false);
        GameManager.instance.CharacterStats[(int)Enums.EStat.Revival] -= 1;
        Time.timeScale = 1;
    }
}
