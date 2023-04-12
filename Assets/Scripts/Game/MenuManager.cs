using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Level-1-1", LoadSceneMode.Single);
    }

    public void OnSettingsButtonClick()
    {

    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
