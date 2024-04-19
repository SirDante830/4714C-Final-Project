using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject UImenu;
    public bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isOn == true)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }

    }
    public void resumeGame()
    {
        //gun.SetActive(true);
        UImenu.SetActive(false);
        Time.timeScale = 1f;
        isOn = false;

    }
    public void pauseGame()
    {
        //gun.SetActive(false);
        UImenu.SetActive(true);
        Time.timeScale = 0f;
        isOn = true;
        //scope.SetActive(false);

        //GetComponent<mouseLook>().enabled = true;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
