using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageInfo : MonoBehaviour, IPointerEnterHandler
{
    public GameObject stageButton;
    public MapScriptableObject stageData;
    public StageOfSelectionScene selectedStage;

    private GameObject MapName;
    private GameObject explainText;

    private void Start()
    {
        MapName = stageButton.transform.Find("Name").gameObject;
        explainText = stageButton.transform.Find("Explain").gameObject;
        MapName.GetComponent<TextMeshProUGUI>().text = stageData.StageName.ToString();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedStage.LoadMapData(stageData);
        explainText.GetComponent<TextMeshProUGUI>().text = stageData.StageExplain;
    }
}