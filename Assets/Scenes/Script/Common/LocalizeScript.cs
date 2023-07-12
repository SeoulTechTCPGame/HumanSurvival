using UnityEngine;
using static Singleton;
using TMPro;

public class LocalizeScript : MonoBehaviour
{
    public string TextKey;

    private void Start()
    {
        LocalizeChanged(S.curLangIndex);
        S.LocalizeChanged += LocalizeChanged;
    }
    private void OnDestroy()
    {
        S.LocalizeChanged -= LocalizeChanged;
    }
    private string Localize(string key, int langIndex)
    {
        int keyIndex = S.Langs[0].value.FindIndex(x => x.ToLower() == key.ToLower());
        return S.Langs[langIndex].value[keyIndex];
    }
    private void LocalizeChanged(int langIndex)
    {
        TMP_Text tmpText = GetComponent<TMP_Text>();
        if (tmpText != null)
        {
            tmpText.text = Localize(TextKey, langIndex);
        }
    }
}