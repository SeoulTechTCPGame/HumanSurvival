using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class CharacterInfo : MonoBehaviour, IPointerDownHandler
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
    private Image mCharacterImage;
   
    private void Start()
    {
        mCharacterName = mCharacterButton.transform.Find("Name").gameObject;
        mExplainName = mExplain.transform.Find("CharacterName").gameObject;
        mExplainImage = mExplain.transform.Find("CharacterImage").gameObject;
        mExplainText = mExplain.transform.Find("CharacterExplain").gameObject;
        mExplainWeapon = mExplain.transform.Find("CharacterWeapon").gameObject;
        mCharacterImage = transform.GetChild(0).gameObject.GetComponent<Image>();

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
        if(!UserInfo.instance.UserDataSet.BUnlockCharacters[(int)mCharacterButton.GetComponent<SelectCharacter>().Charname])
        {
            mCharacterImage.color = Color.black;
        }
        else
        {
            mCharacterImage.color = Color.white;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mSelectedCharacter.LoadCharacterData(mCharacterData);
        mExplainName.GetComponent<TextMeshProUGUI>().text = mCharacterData.CharacterType.ToString();
        mExplainText.GetComponent<TextMeshProUGUI>().text = mCharacterData.explain;
        mExplainImage.GetComponent<Image>().sprite = mCharacterImage.sprite;
        Enums.EWeapon[] enumValues = (Enums.EWeapon[])System.Enum.GetValues(typeof(Enums.EWeapon));
        Enums.EWeapon weapon = enumValues[mCharacterData.startingWeapon];
        string weapoonName = "Weapons/" + weapon.ToString();
        mExplainWeapon.GetComponent<Image>().sprite = Resources.Load<Sprite>(weapoonName);

        if (!UserInfo.instance.UserDataSet.BUnlockCharacters[(int)mCharacterButton.GetComponent<SelectCharacter>().Charname])
        {
            mExplainImage.GetComponent<Image>().color = Color.black;
        }
        else
        {
            mExplainImage.GetComponent<Image>().color = Color.white;
        }
    }
}