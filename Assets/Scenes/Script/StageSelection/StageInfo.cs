using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static Singleton;

public class StageInfo : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject mStageButton;
    [SerializeField] MapScriptableObject mStageDataEN;
    [SerializeField] MapScriptableObject mStageDataKR;
    [SerializeField] StageOfSelectionScene mSelectedStage;

    private GameObject mMapName;
    private GameObject mExplainText;

    private void Start()
    {
        mMapName = mStageButton.transform.Find("Name").gameObject;
        mExplainText = mStageButton.transform.Find("Explain").gameObject;
        switch (S.curLangIndex)
        {
            case (int)Enums.ELangauge.EN:
                mMapName.GetComponent<TextMeshProUGUI>().text = mStageDataEN.StageName.ToString();
                break;
            case (int)Enums.ELangauge.KR:
                mMapName.GetComponent<TextMeshProUGUI>().text = mStageDataEN.StageName.ToString();
                break;
            default:
                break;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mSelectedStage.LoadMapData(mStageDataEN);
        switch (S.curLangIndex)
        {
            case (int)Enums.ELangauge.EN:
                mExplainText.GetComponent<TextMeshProUGUI>().text = mStageDataEN.StageName.ToString();
                break;
            case (int)Enums.ELangauge.KR:
                mExplainText.GetComponent<TextMeshProUGUI>().text = mStageDataEN.StageName.ToString();
                break;
            default:
                break;
        }
    }
}