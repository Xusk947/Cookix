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
    private void Awake()
    {
        Instance = this;
        Recievers = new List<FoodReciever>();
        rules = Rules.Normal.Copy();

        Content content = gameObject.AddComponent<Content>();
        content.Load();
    }
}
