using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCharacterSelection : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}
