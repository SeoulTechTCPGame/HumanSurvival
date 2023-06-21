using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    public GameObject Weaponlist;
    public GameObject Accessorylist;

    //[SerializeField] TMP_Text map = null;
    [SerializeField] TMP_Text mTime = null;
    [SerializeField] TMP_Text mCoin = null;
    [SerializeField] TMP_Text mLevel = null;
    [SerializeField] TMP_Text mKill = null;
    [SerializeField] TMP_Text mCharacter = null;
    [SerializeField] Image mCharacterImage = null;

    private float mGameTime;
    private GameObject mWeaponInfo;

    private void Start()
    {
        mLevel.text = string.Format("{0}", GameManager.instance.Level);
        mKill.text = string.Format("{0}", GameManager.instance.Kill);
        mCoin.text = string.Format("{0}", GameManager.instance.Coin);
        mGameTime = GameManager.instance.GameTime;
        float seconds = Mathf.Floor(mGameTime % 60);
        float minutes = Mathf.Floor(mGameTime / 60);
        mTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        string charName = "" + DataManager.instance.CurrentCharcter;
        mCharacter.text = string.Format("{0}", charName);
        string source = "CharacterSprite/" + charName + "_0";
        mCharacterImage.sprite = Resources.Load<Sprite>(source);
        for (int i = 0; i < GameManager.instance.EquipManageSys.Weapons.Count; i++)
        {
            mWeaponInfo = Resources.Load<GameObject>("Weapons/WeaponInfo");
            GameObject row = Instantiate(mWeaponInfo);
            row.GetComponent<WeaponListUI>().SetWeaponResultData(i,GameManager.instance.EquipManageSys.Weapons[i].WeaponIndex);
            row.transform.SetParent(Weaponlist.transform, false);
        }
        for(int i = 0; i < GameManager.instance.EquipManageSys.Accessories.Count; i++)
        {
            //밑에 숫자가 뭘까..
        }
    }
    public void ClickCompleteBtn()
    {
        UserInfo.instance.UpdateAccumulatedTime(GameManager.instance.GameTime);
        UserInfo.instance.UpdateAccumulatedKill(GameManager.instance.Kill);
        UserInfo.instance.UpdateGold(GameManager.instance.Coin);
    }
}