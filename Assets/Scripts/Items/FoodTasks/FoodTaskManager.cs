using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTaskManager : MonoBehaviour
{
    public static FoodTaskManager Instance { get; private set; }
    [SerializeField]
    private List<FoodTask> _foodTasks;
    private List<FoodTask> _activeTasks;
    private float timer = 0f;
    private void Start()
    {
        Instance = this;
        _activeTasks = new List<FoodTask>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            if (_activeTasks.Count < 3) 
            {
                FoodTask task = GenerateTask();
                print("New task Become: " + task);
                _activeTasks.Add(task);
            }
            timer = 10f;
        }
    }

    public FoodTask GenerateTask()
    {
        return _foodTasks[Random.Range(0, _foodTasks.Count)];
    }

    public FoodTask CheckItemForTask(FoodEntity foodEntity)
    {
        if (_activeTasks.Count == 0)
        {
            print("All task Finished!");
            return null;
        }
        for (int i = 0; i < _activeTasks.Count; i++)
        {
            FoodTask foodTask = _activeTasks[i];
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
        _activeTasks.Remove(task);
    }
}
