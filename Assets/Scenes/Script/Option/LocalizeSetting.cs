using System.Collections.Generic;
using UnityEngine;
using static Singleton;
using TMPro;

public class LocalizeSetting : MonoBehaviour
{
    private TMP_Dropdown mdropdown;

    private void Start()
    {
        mdropdown = GetComponent<TMP_Dropdown>();
        if (mdropdown.options.Count != S.Langs.Count)
        {
            SetLangOption();
        }
        mdropdown.onValueChanged.AddListener((d) => S.SetLangIndex(mdropdown.value));
        LocalizeSettingChanged();
        S.LocalizeSettingChanged += LocalizeSettingChanged;
    }
    private void OnDestroy()
    {
        S.LocalizeSettingChanged -= LocalizeSettingChanged;
    }
    private void SetLangOption()
    {
        List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < S.Langs.Count; i++)
        {
            optionDatas.Add(new TMP_Dropdown.OptionData() { text = S.Langs[i].langLocalize });
        }
        mdropdown.options = optionDatas;
    }
    private void LocalizeSettingChanged()
    {
        mdropdown.value = S.curLangIndex;
    }
}