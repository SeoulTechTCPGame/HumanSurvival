using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRatio : MonoBehaviour
{
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
