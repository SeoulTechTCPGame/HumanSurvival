using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponListUI : MonoBehaviour
{
    GameObject weaponInfo;

    [SerializeField] Image WeaponImage = null;
    [SerializeField] TMP_Text Weaponname = null;
    [SerializeField] TMP_Text Weaponlevel = null;
    [SerializeField] TMP_Text Weapondamage = null;
    [SerializeField] TMP_Text WeaponTime = null;
    [SerializeField] TMP_Text WeaponDamagePerSec = null;
    public void SetWeaponResultData(int i, float gameTime)
    {
        string source = "Weapons/" + i + "Img";
        //string source = "Weapons/0Img";
        WeaponImage.sprite = Resources.Load<Sprite>(source); 
         //무기 이미지
        Weaponname.text = string.Format("{0}", i); //무기 이름
        //Weaponlevel.text = string.Format("{0}", GameManager.instance.equipManageSys.Weapons[i].WeaponIevel); //무기 레벨

        float weaponDamage = GameManager.instance.weaponDamage[i];
        Weapondamage.text = string.Format("{0}", weaponDamage); //무기 데미지

        float weaponTime = gameTime - GameManager.instance.weaponGetTime[i];
        float weaponSeconds = Mathf.Floor(weaponTime % 60);
        float weaponMinutes = Mathf.Floor(weaponTime / 60);
        WeaponTime.text = string.Format("{0:00}:{1:00}", weaponMinutes, weaponSeconds);// 무기 시간
        Debug.Log(weaponDamage);
        Debug.Log((weaponTime));
        WeaponDamagePerSec.text = string.Format("{0}", weaponDamage / (gameTime - weaponTime));// 무기 초당데미지
    }
}
