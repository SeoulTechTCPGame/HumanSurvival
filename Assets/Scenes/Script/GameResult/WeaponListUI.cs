using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class WeaponListUI : MonoBehaviour
{
    [SerializeField] Image mWeaponImage = null;
    [SerializeField] TMP_Text mWeaponname = null;
    [SerializeField] TMP_Text mWeaponlevel = null;
    [SerializeField] TMP_Text mWeapondamage = null;
    [SerializeField] TMP_Text mWeaponTime = null;
    [SerializeField] TMP_Text mWeaponDamagePerSec = null;
    private GameObject mWeaponInfo;
    public void SetWeaponResultData(int orderIndex,int weaponIndex)
    {
        float gameTime = GameManager.instance.GameTime;
        Enums.EWeapon[] enumValuesWeapon = (Enums.EWeapon[])System.Enum.GetValues(typeof(Enums.EWeapon));
        Enums.EWeapon weapon = enumValuesWeapon[weaponIndex];
        string source = "Weapons/" + weapon;
        //string source = "Weapons/0Img";
        mWeaponImage.sprite = Resources.Load<Sprite>(source); 
         //무기 이미지
        mWeaponname.text = string.Format("{0}", Enum.GetName(typeof(Enums.EWeapon),weaponIndex)); //무기 이름
        mWeaponlevel.text = string.Format("{0}", GameManager.instance.EquipManageSys.Weapons[orderIndex].WeaponLevel); //무기 레벨

        float weaponDamage = GameManager.instance.WeaponDamage[weaponIndex];
        mWeapondamage.text = string.Format("{0}", weaponDamage); //무기 데미지

        float weaponTime = gameTime - GameManager.instance.WeaponGetTime[weaponIndex];
        float weaponSeconds = Mathf.Floor(weaponTime % 60);
        float weaponMinutes = Mathf.Floor(weaponTime / 60);
        mWeaponTime.text = string.Format("{0}:{1:00}", weaponMinutes, weaponSeconds);// 무기 시간
        if (gameTime - weaponTime == 0)
        {
            mWeaponDamagePerSec.text = string.Format("{0:N1}", weaponDamage / gameTime );// 무기 초당데미지
        }
        else
        {
            mWeaponDamagePerSec.text = string.Format("{0:N1}", weaponDamage /  weaponTime);// 무기 초당데미지
        }
    }
}
