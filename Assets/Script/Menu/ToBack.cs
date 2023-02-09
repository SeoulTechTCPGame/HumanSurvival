using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBack : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
