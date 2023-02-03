using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStage : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("MapSelection");
    }
}
