using UnityEngine;
using TMPro;

public class StageOfSelectionScene : MonoBehaviour
{
    [SerializeField] TMP_Text mMoneyText;

    //StageName, Time, DoubleSpeed, GoldCoinBonus, LuckBonus, ExperienceBonus
    private TextMeshProUGUI[] mTextMeshes;

    private void Start()
    {
        SetMoneyText();
        mTextMeshes = GetComponentsInChildren<TextMeshProUGUI>();
    }
    public void LoadMapData(MapScriptableObject mapData)
    {
        mTextMeshes[0].SetText(mapData.StageName);
        mTextMeshes[1].SetText("게임 시간: " + mapData.PlayTime);
        mTextMeshes[2].SetText("배속: " + mapData.DoubleSpeed);
        mTextMeshes[3].SetText("골드 보너스: " + mapData.GoldCoinBonus);
        mTextMeshes[4].SetText("행운 보너스: " + mapData.LuckBonus);
        mTextMeshes[5].SetText("경험치 보너스: " + mapData.ExperienceBonus);
    }
    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
}