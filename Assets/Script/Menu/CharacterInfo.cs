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
    private Text characterName;
    private Image explainImagege;
    CharacterScriptableObject characterData;
    public CharacterInfoOfSelectionScene selectedCharacter;

    private void Start()
    {
        characterName = characterButton.GetComponentInChildren<Text>();
        explainImagege = explain.GetComponentInChildren<Image>();

        string resourceName = "Resources/";
        try
        {
            resourceName += DataManager.instance.currentCharcter;
        }
        catch (NullReferenceException)
        {
            resourceName += "Alchemist";
        }
        characterData = Resources.Load<CharacterScriptableObject>(resourceName);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedCharacter.LoadCharacterData();
        characterName.text = characterData.name;
        explain.GetComponentInChildren<Image>().sprite = explainImagege.sprite;
    }
}