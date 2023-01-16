using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine.SceneManagement;

public enum State { Quit, MainscreenOption, CharacterSelection, PowerUp, Collection, Achievements };


public class User : MonoBehaviour
{
    public GameObject userSelect;
    public GameObject[] buttons;

    public TextMeshProUGUI moneyText;

    public int selectState;
<<<<<<< HEAD
    public int money;
=======
>>>>>>> origin/develop-mainCollection
    // Start is called before the first frame update
    void Start()
    {
        // TODO - Read user data
<<<<<<< HEAD
        money = 100;
=======
        UserInfo.money = 100;
        for(int i = 0; i < UserInfo.itemCount; i++){
            UserInfo.userItem[i] = false;
        }
        for(int i = 0; i < UserInfo.achiCount; i++){
            UserInfo.userAchi[i] = false;
        }
        // Example for item
        // UserInfo.userAchi[0] = true;
        // UserInfo.userItem[0] = true;
>>>>>>> origin/develop-mainCollection

        selectState = (int)State.CharacterSelection;
        setSelect(buttons[selectState]);
        SetMoneyText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(selectState > (int)State.MainscreenOption && selectState <= (int)State.Collection)
            {
                selectState--;
                setSelect(buttons[selectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectState == (int)State.MainscreenOption)
            {
                selectState--;
                setSelect(buttons[selectState]);
            }
            else if(selectState == (int)State.PowerUp)
            {
                selectState++;
                setSelect(buttons[selectState]);
            }
            else if (selectState == (int)State.Achievements)
            {
                selectState -= 2;
                setSelect(buttons[selectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectState > (int)State.Quit && selectState < (int)State.PowerUp)
            {
                selectState++;
                setSelect(buttons[selectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectState == (int)State.Quit)
            {
                selectState++;
                setSelect(buttons[selectState]);
            }
            else if(selectState == (int)State.PowerUp)
            {
                selectState += 2;
                setSelect(buttons[selectState]);
            }
            else if (selectState == (int)State.Collection)
            {
                selectState--;
                setSelect(buttons[selectState]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (selectState != (int)State.Quit)
            {
                SceneManager.LoadScene(((State)selectState).ToString());
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
<<<<<<< HEAD
        moneyText.text = "������: " + money.ToString();
=======
        moneyText.text = UserInfo.money.ToString();
>>>>>>> origin/develop-mainCollection
    }

    private void setSelect(GameObject nowObject)
    {
        userSelect.transform.position = nowObject.transform.position;
    }
}
