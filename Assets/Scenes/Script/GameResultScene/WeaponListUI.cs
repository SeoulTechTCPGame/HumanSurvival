using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Enums;
using System;
public class WeaponListUI : MonoBehaviour
{

    GameObject weaponInfo;

    [SerializeField] Image WeaponImage = null;
    [SerializeField] TMP_Text Weaponname = null;
    [SerializeField] TMP_Text Weaponlevel = null;
    [SerializeField] TMP_Text Weapondamage = null;
    [SerializeField] TMP_Text WeaponTime = null;
    [SerializeField] TMP_Text WeaponDamagePerSec = null;
    public void SetWeaponResultData(int orderIndex,int weaponIndex, float gameTime)
    {
        string source = "Weapons/" + weaponIndex + "Img";
        //string source = "Weapons/0Img";
        WeaponImage.sprite = Resources.Load<Sprite>(source); 
         //무기 이미지
        Weaponname.text = string.Format("{0}", Enum.GetName(typeof(Enums.Weapon),weaponIndex)); //무기 이름
        Weaponlevel.text = string.Format("{0}", GameManager.instance.equipManageSys.Weapons[orderIndex].WeaponLevel); //무기 레벨

        float weaponDamage = GameManager.instance.weaponDamage[weaponIndex];
        Weapondamage.text = string.Format("{0}", weaponDamage); //무기 데미지

        float weaponTime = gameTime - GameManager.instance.weaponGetTime[weaponIndex];
        float weaponSeconds = Mathf.Floor(weaponTime % 60);
        float weaponMinutes = Mathf.Floor(weaponTime / 60);
        WeaponTime.text = string.Format("{0}:{1:00}", weaponMinutes, weaponSeconds);// 무기 시간
        if (gameTime - weaponTime == 0)
        {
            WeaponDamagePerSec.text = string.Format("{0:N1}", weaponDamage / (gameTime ));// 무기 초당데미지
        }
        else
        {
            WeaponDamagePerSec.text = string.Format("{0:N1}", weaponDamage / (gameTime - weaponTime));// 무기 초당데미지
        }
    }
}
