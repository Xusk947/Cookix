using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameInput))]
[RequireComponent(typeof(BlockEventsHandler))]
[RequireComponent(typeof(Content))]
[RequireComponent(typeof(ScreenshotManager))]
[RequireComponent(typeof(FoodTaskManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Canvas canvas;
    private void Start()
    {
        Instance = this;
    }
}
