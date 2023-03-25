using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator Instance { get; private set; }

    [SerializeField]
    private List<FoodTask> _foodTasks;

    public void Start()
    {
        Instance = this;
    }

    public FoodTask GetTask()
    {
        return _foodTasks[Random.Range(0, _foodTasks.Count)];
    }
}
