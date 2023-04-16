using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameInput))]
[RequireComponent(typeof(BlockEventsHandler))]
[RequireComponent(typeof(ScreenshotManager))]
[RequireComponent(typeof(FoodTaskManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Camera MainCamera;
    public Canvas Canvas;
    public List<FoodReciever> Recievers;

    public GameObject clientEntter, clienExit;
    public Rules rules;
    public bool IsPaused = false;

    private void Awake()
    {
        Instance = this;
        Recievers = new List<FoodReciever>();
        if (rules == null) rules = Rules.Normal.Copy();

        if (Content.Instance == null) new Content().Load();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Pause(bool showMenu = true)
    {
        Time.timeScale = 0.0f;
        IsPaused = true;
        if (showMenu)
        {
            InGameUI.Instance.Show();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        IsPaused = false;
        InGameUI.Instance.Hide();
    }

    private void OnDestroy()
    {
        Time.timeScale = 1.0f;
    }
}
