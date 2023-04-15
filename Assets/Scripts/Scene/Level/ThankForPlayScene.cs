using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThankForPlayScene : MonoBehaviour
{
    public void OnExitButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnDiscordButton()
    {
        // TO DO : ADD DISCORD
    }
}
