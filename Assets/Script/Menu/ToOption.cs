using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToOption : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("MainscreenOption");
    }
}
