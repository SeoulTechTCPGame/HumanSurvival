using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInhoOfSelectionScene : MonoBehaviour
{
    //Name, MaxStamina, Recovery, Defense, Speed, Damage, ProjectileSpeed, Durationn, AttackRange, Cooldown, NumberOfProjectiles, Magnet, Luck, Growth
    private TextMeshProUGUI[] textMeshes;

    void Start()
    {
        textMeshes = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI textMesh in textMeshes)
        {
            Debug.Log(textMesh.text);
        }
    }
}
