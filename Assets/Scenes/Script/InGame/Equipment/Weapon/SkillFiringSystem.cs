using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    public GameObject[] weaponPrefabs; //무기 프리팹
    public GameObject[] evolutionWeaponPrefabs; //진화 무기 프리팹
    public GameObject[] Birds; // peachone, EbonyWings, 둘의 진화체에 등장하는 새
    public GameObject[] Circles;
    
    public static SkillFiringSystem instance = null;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        foreach (var weapon in GameManager.instance.EquipManageSys.Weapons)
        {
            weapon.Attack();
        }
    }
}