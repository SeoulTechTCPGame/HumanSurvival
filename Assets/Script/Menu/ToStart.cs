using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStart : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("InGame");
    }
}
