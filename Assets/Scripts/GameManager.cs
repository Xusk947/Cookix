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
[InitializeOnLoad]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Camera MainCamera;
    public Canvas Canvas;
    public List<FoodReciever> Recievers;

    public GameObject clientEntter, clienExit;
    public Rules rules;
    public bool IsPaused = false;

    private InGameUI _gameUI;
    private void Awake()
    {
        Instance = this;
        Recievers = new List<FoodReciever>();
        rules = Rules.Normal.Copy();

        _gameUI = Canvas.GetComponent<InGameUI>();

        Content content = gameObject.AddComponent<Content>();
        content.Load();
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

    public void Pause()
    {
        Time.timeScale = 0.0f;
        IsPaused = true;
        _gameUI.Show();
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        IsPaused = false;
        _gameUI.Hide();
    }
}
