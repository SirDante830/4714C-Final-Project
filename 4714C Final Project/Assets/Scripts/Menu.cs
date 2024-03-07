using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoBackToMain()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("Credits");
    }

    // This function will make it so if you press the quit button, the application will close
    public void QuitButton()
    {
        Application.Quit();
    }
}
