using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class CharacterInfo : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject mCharacterButton;
    [SerializeField] GameObject mExplain;
    [SerializeField] CharacterInfoOfSelectionScene mSelectedCharacter;

    private GameObject mCharacterName;
    private GameObject mExplainName;
    private GameObject mExplainImage;
    private GameObject mExplainWeapon;
    private GameObject mExplainText;

    private CharacterScriptableObject mCharacterData;
   
    private void Start()
    {
        mCharacterName = mCharacterButton.transform.Find("Name").gameObject;
        mExplainName = mExplain.transform.Find("CharaName").gameObject;
        mExplainImage = mExplain.transform.Find("CharaImage").gameObject;
        mExplainText = mExplain.transform.Find("CharaExplain").gameObject;
        mExplainWeapon = mExplain.transform.Find("CharaWeapon").gameObject;

        string resourceName = "CharacterData/";
        try
        {
            resourceName += mCharacterButton.GetComponent<SelectCharacter>().Charname;
        }
        catch (NullReferenceException)
        {
            resourceName += "Alchemist";
        }
        mCharacterData = Resources.Load<CharacterScriptableObject>(resourceName);
        mCharacterName.GetComponent<TextMeshProUGUI>().text = mCharacterData.CharacterType.ToString();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mSelectedCharacter.LoadCharacterData(mCharacterData);
        mExplainName.GetComponent<TextMeshProUGUI>().text = mCharacterData.CharacterType.ToString();
        mExplainText.GetComponent<TextMeshProUGUI>().text = mCharacterData.explain;
        mExplainImage.GetComponent<Image>().sprite = mCharacterButton.transform.Find("Image").GetComponent<Image>().sprite;
        Enums.Weapon[] enumValues = (Enums.Weapon[])System.Enum.GetValues(typeof(Enums.Weapon));
        Enums.Weapon weapon = enumValues[mCharacterData.startingWeapon];
        string weapoonName = "Weapons/" + weapon.ToString();
        mExplainWeapon.GetComponent<Image>().sprite = Resources.Load<Sprite>(weapoonName);
    }
}