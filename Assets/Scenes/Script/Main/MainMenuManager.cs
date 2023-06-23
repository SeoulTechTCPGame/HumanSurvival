using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum EState { Quit, Option, CharacterSelection, PowerUp, Collection, Achievements };

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mUserSelect;
    [SerializeField] GameObject[] mButtons;
    [SerializeField] TextMeshProUGUI mMoneyText;
    [SerializeField] int mSelectState;

    private void Start()
    {
        mSelectState = (int)EState.CharacterSelection;
        SetSelect(mButtons[mSelectState]);
        SetMoneyText();
    }
    private void Update() //TODO: refactoring 예정
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(mSelectState > (int)EState.Option && mSelectState <= (int)EState.Collection)
            {
                mSelectState--;
                SetSelect(mButtons[mSelectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (mSelectState == (int)EState.Option)
            {
                mSelectState--;
                SetSelect(mButtons[mSelectState]);
            }
            else if(mSelectState == (int)EState.PowerUp)
            {
                mSelectState++;
                SetSelect(mButtons[mSelectState]);
            }
            else if (mSelectState == (int)EState.Achievements)
            {
                mSelectState -= 2;
                SetSelect(mButtons[mSelectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (mSelectState > (int)EState.Quit && mSelectState < (int)EState.PowerUp)
            {
                mSelectState++;
                SetSelect(mButtons[mSelectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (mSelectState == (int)EState.Quit)
            {
                mSelectState++;
                SetSelect(mButtons[mSelectState]);
            }
            else if(mSelectState == (int)EState.PowerUp)
            {
                mSelectState += 2;
                SetSelect(mButtons[mSelectState]);
            }
            else if (mSelectState == (int)EState.Collection)
            {
                mSelectState--;
                SetSelect(mButtons[mSelectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (mSelectState != (int)EState.Quit)
            {
                SceneManager.LoadScene(((EState)mSelectState).ToString());
            }
            else
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
            }
        }
        SetMoneyText();
    }
    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
    private void SetSelect(GameObject nowObject)
    {
        mUserSelect.transform.position = nowObject.transform.position;
    }
}