using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToPowerUp : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("PowerUp");
    }
}
