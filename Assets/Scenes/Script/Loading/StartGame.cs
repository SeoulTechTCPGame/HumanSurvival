using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private bool bCanProceed = false;

    private void Update()
    {
        // 아무 키를 누르면 bCanProceed를 true로 설정하여 다음 씬으로 넘어갑니다.
        if (bCanProceed || Input.anyKeyDown)
        {
            GetComponent<SceneMove>().ToBack();
        }
    }
    public void OnPanelClick()
    {
        // 패널이 클릭되면 bCanProceed를 true로 설정하여 다음 씬으로 넘어갑니다.
        bCanProceed = true;
    }
}