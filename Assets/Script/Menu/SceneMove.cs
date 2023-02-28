using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public void ToAchievements()
    {
        SceneManager.LoadScene("MainAchievements");
    }

    public void ToBack()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void ToCharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void ToCollection()
    {
        SceneManager.LoadScene("MainCollection");
    }

    public void ToOption()
    {
        SceneManager.LoadScene("MainscreenOption");
    }

    public void ToPowerUp()
    {
        SceneManager.LoadScene("PowerUp");
    }

    public void ToStage()
    {
        SceneManager.LoadScene("StageSelection");
    }

    public void ToStart()
    {
        SceneManager.LoadScene("InGame");
    }
}
