using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class CharacterInfo : MonoBehaviour, IPointerEnterHandler
{
    public GameObject characterButton;
    public GameObject explain;
    public CharacterInfoOfSelectionScene selectedCharacter;

    private GameObject characterName;
    private GameObject explainName;
    private GameObject explainImage;
    private GameObject explainText;

    CharacterScriptableObject characterData;
   
    private void Start()
    {
        characterName = characterButton.transform.Find("Name").gameObject;
        explainName = explain.transform.Find("CharaName").gameObject;
        explainImage = explain.transform.Find("CharaImage").gameObject;
        explainText = explain.transform.Find("CharaExplain").gameObject;

        string resourceName = "CharacterData/";
        try
        {
            resourceName += characterButton.GetComponent<SelectCharacter>().charname;
        }
        catch (NullReferenceException)
        {
            resourceName += "Alchemist";
        }
        characterData = Resources.Load<CharacterScriptableObject>(resourceName);
        characterName.GetComponent<TextMeshProUGUI>().text = characterData.characterType.ToString();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedCharacter.LoadCharacterData(characterData);
        explainName.GetComponent<TextMeshProUGUI>().text = characterData.characterType.ToString();
        explainText.GetComponent<TextMeshProUGUI>().text = characterData.explain;
        explainImage.GetComponent<Image>().sprite = characterButton.transform.Find("Image").GetComponent<Image>().sprite;
    }
}