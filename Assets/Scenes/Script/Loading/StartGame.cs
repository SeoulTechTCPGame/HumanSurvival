using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private bool canProceed = false;

    private void Update()
    {
        // 아무 키를 누르면 canProceed를 true로 설정하여 다음 씬으로 넘어갑니다.
        if (canProceed || Input.anyKeyDown)
        {
            ProceedToNextScene();
        }
    }

    public void OnPanelClick()
    {
        // 패널이 클릭되면 canProceed를 true로 설정하여 다음 씬으로 넘어갑니다.
        canProceed = true;
    }

    private void ProceedToNextScene()
    {
        // 다음 씬으로 전환합니다.
        SceneManager.LoadScene("MainScreen");
    }
}