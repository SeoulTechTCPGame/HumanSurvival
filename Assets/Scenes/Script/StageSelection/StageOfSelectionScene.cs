using UnityEngine;
using TMPro;
using static Singleton;

public class StageOfSelectionScene : MonoBehaviour
{
    [SerializeField] TMP_Text mMoneyText;
    [SerializeField] TMP_Text mStageName;
    [SerializeField] TMP_Text mTime;
    [SerializeField] TMP_Text mDoubleSpeed;
    [SerializeField] TMP_Text mGoldCoinBonus;
    [SerializeField] TMP_Text mLuckBonus;
    [SerializeField] TMP_Text mExperienceBonus;

    private void Start()
    {
        SetMoneyText();
    }
    public void LoadMapData(MapScriptableObject mapData)
    {
        switch (S.curLangIndex)
        {
            case (int)Enums.ELangauge.EN:
                mStageName.SetText(mapData.StageNameEN);
                break;
            case (int)Enums.ELangauge.KR:
                mStageName.SetText(mapData.StageNameKR);
                break;
            default:
                break;
        }
        mTime.SetText(mapData.PlayTime.ToString() + ":00");
        mDoubleSpeed.SetText(mapData.DoubleSpeed.ToString());
        mGoldCoinBonus.SetText(mapData.GoldCoinBonus.ToString());
        mLuckBonus.SetText(mapData.LuckBonus.ToString());
        mExperienceBonus.SetText(mapData.ExperienceBonus.ToString());
    }
    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
}