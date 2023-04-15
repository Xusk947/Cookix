using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTaskManager : MonoBehaviour
{
    public static FoodTaskManager Instance { get; private set; }
    [SerializeField]
    private List<FoodTask> _foodTasks;
    private List<FoodTask> _activeTasks;
    private List<FoodTask> _takedTasks;

    private GameObject foodTaskHolder;

    public List<FoodTask> FoodTasks
    {
        get { return _foodTasks; }
        set { _foodTasks = value; }
    }

    private void Start()
    {
        Instance = this;
        _activeTasks = new List<FoodTask>();
        _takedTasks = new List<FoodTask>();
        CreateBaseForTasks();
    }
    public FoodTask CreateTask()
    {
        FoodTask task = GenerateTask();
        _activeTasks.Add(task);
        return task;
    }

    public FoodTask TakeTask()
    {
        if (_activeTasks.Count == 0) return null;
        FoodTask takedTask = _activeTasks[0];
        _activeTasks.RemoveAt(0);
        _takedTasks.Add(takedTask);
        return takedTask;
    }
    private FoodTask GenerateTask()
    {
        RouletteWheelSelection<FoodTask> wheel = new RouletteWheelSelection<FoodTask>();
        foreach(FoodTask task in _foodTasks)
        {
            wheel.Add(task, task.difficult);
        }
        return wheel.Spin();
    }

    public FoodTask CheckItemForTask(FoodEntity foodEntity)
    {
        if (_takedTasks.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < _takedTasks.Count; i++)
        {
            FoodTask foodTask = _takedTasks[i];
            if (foodTask.Compare(foodEntity))
            {
                return foodTask;
            }
        }
        return null;
    }

    public void AddTask(FoodTask task)
    {
        _activeTasks.Add(task);
    }
    public void RemoveTask(FoodTask task)
    {
        _takedTasks.Remove(task);
    }

    private void CreateBaseForTasks()
    {
        // Create a Holder
        foodTaskHolder = new GameObject();
        foodTaskHolder.name = "FoodTaskHolder";

        // Create and fix Rect Component
        RectTransform rectTransform = foodTaskHolder.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(0, 0);
        rectTransform.position = new Vector2(50f, 50f);

        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, rectTransform.rect.width);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, rectTransform.rect.height);
        rectTransform.anchorMin = new Vector2(0f, 0f);
        rectTransform.anchorMax = new Vector2(0f, 0f);

        foodTaskHolder.AddComponent<VerticalLayoutGroup>();

        foodTaskHolder.transform.SetParent(GameManager.Instance.Canvas.transform, false);
    }
}
