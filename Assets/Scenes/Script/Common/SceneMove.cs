using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public void ToAchievements()
    {
        SceneManager.LoadScene("Achievements");
    }
    public void ToBack()
    {
        SceneManager.LoadScene("Main");
    }
    public void ToCharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
    public void ToCollection()
    {
        SceneManager.LoadScene("Collection");
    }
    public void ToOption()
    {
        SceneManager.LoadScene("Option");
    }
    public void ToPowerUp()
    {
        SceneManager.LoadScene("PowerUp");
    }
    public void ToCredit()
    {
        SceneManager.LoadScene("Credit");
    }
    public void ToStage()
    {
        SceneManager.LoadScene("StageSelection");
    }
    public void ToStart()
    {
        SceneManager.LoadScene("InGame");
    }
    public void ToResultScene()
    {
        SceneManager.LoadScene("GameResult");
    }
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}