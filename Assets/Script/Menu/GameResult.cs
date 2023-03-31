using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    //[SerializeField] TMP_Text map = null;
    [SerializeField] TMP_Text time = null;
    float gameTime;
    [SerializeField] TMP_Text coin = null;
    [SerializeField] TMP_Text level = null;
    [SerializeField] TMP_Text kill = null;
    [SerializeField] TMP_Text character = null;
    [SerializeField] Image characterImage=null;

    public GameObject Weaponlist;
    GameObject weaponInfo;

    TMP_Text Weaponname;
    TMP_Text Weaponlevel;
    TMP_Text Weapondamage;
    TMP_Text WeapondamagePerSec;

    void Start()
    {
        level.text = string.Format("{0}", GameManager.instance.level);
        kill.text = string.Format("{0}", GameManager.instance.kill);
        coin.text = string.Format("{0}", GameManager.instance.coin);
        gameTime = GameManager.instance.gameTime;
        float seconds = Mathf.Floor(gameTime % 60);
        float minutes = Mathf.Floor(gameTime / 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        string charName = "" + DataManager.instance.currentCharcter;
        character.text=string.Format("{0}",charName);
        string source="CharacterSprite/"+ charName + "_0";
        characterImage.sprite = Resources.Load<Sprite>(source);
        for(int i = 0; i < 6; i++)
        {
            //갖고있는 무기 정보 출력
            weaponInfo = Resources.Load<GameObject>("Weapons/WeaponInfo");
            GameObject row = (GameObject)Instantiate(weaponInfo);
            string adrs = "" + i;
            Sprite image = Resources.Load<Sprite>(adrs);
            row.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = image;
            //row.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text=
            //weaponInfo.transform.SetParent(Weaponlist.transform);
        }
        
        // Weapondamage = GameManager.instance.weaponDamage[필요한 무기 인덱스]
        // WeapondamagePerSec = Weapondamage / (gameTime - GameManager.instance.weaponGetTime[필요한 무기 인덱스]) 이렇게 쓰시면 됩니당 누님 충성~
    }
   
}