using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageInfo : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject mStageButton;
    [SerializeField] MapScriptableObject mStageData;
    [SerializeField] StageOfSelectionScene mSelectedStage;

    private GameObject mMapName;
    private GameObject mExplainText;

    private void Start()
    {
        mMapName = mStageButton.transform.Find("Name").gameObject;
        mExplainText = mStageButton.transform.Find("Explain").gameObject;
        mMapName.GetComponent<TextMeshProUGUI>().text = mStageData.StageName.ToString();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mSelectedStage.LoadMapData(mStageData);
        mExplainText.GetComponent<TextMeshProUGUI>().text = mStageData.StageExplain;
    }
}