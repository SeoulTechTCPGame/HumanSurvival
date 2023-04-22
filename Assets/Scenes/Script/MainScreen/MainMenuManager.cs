using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine.SceneManagement;

public enum State { Quit, MainscreenOption, CharacterSelection, PowerUp, Collection, Achievements };


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mUserSelect;
    [SerializeField] GameObject[] mButtons;

    [SerializeField] TextMeshProUGUI mMoneyText;

    [SerializeField] int mSelectState;
    void Start()
    {
        mSelectState = (int)State.CharacterSelection;
        setSelect(mButtons[mSelectState]);
        SetMoneyText();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(mSelectState > (int)State.MainscreenOption && mSelectState <= (int)State.Collection)
            {
                mSelectState--;
                setSelect(mButtons[mSelectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (mSelectState == (int)State.MainscreenOption)
            {
                mSelectState--;
                setSelect(mButtons[mSelectState]);
            }
            else if(mSelectState == (int)State.PowerUp)
            {
                mSelectState++;
                setSelect(mButtons[mSelectState]);
            }
            else if (mSelectState == (int)State.Achievements)
            {
                mSelectState -= 2;
                setSelect(mButtons[mSelectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (mSelectState > (int)State.Quit && mSelectState < (int)State.PowerUp)
            {
                mSelectState++;
                setSelect(mButtons[mSelectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (mSelectState == (int)State.Quit)
            {
                mSelectState++;
                setSelect(mButtons[mSelectState]);
            }
            else if(mSelectState == (int)State.PowerUp)
            {
                mSelectState += 2;
                setSelect(mButtons[mSelectState]);
            }
            else if (mSelectState == (int)State.Collection)
            {
                mSelectState--;
                setSelect(mButtons[mSelectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (mSelectState != (int)State.Quit)
            {
                SceneManager.LoadScene(((State)mSelectState).ToString());
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

    void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }

    private void setSelect(GameObject nowObject)
    {
        mUserSelect.transform.position = nowObject.transform.position;
    }
}
