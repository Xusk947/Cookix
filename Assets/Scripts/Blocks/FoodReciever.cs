using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodReciever : Block
{
    /// <summary>
    /// A variable for _timer which provide a Time for all recievers
    /// </summary>
    private const float _TIME = 5f;
    /// <summary>
    /// current Food Task
    /// </summary>
    private FoodTask _task;
    /// <summary>
    /// GameObject of UIHolder which automaticlly create a Grid of ingredients for a Tasks
    /// </summary>
    private FoodTaskImage UIHolder;
    /// <summary>
    /// When timer is gone take a Task from TaskManager
    /// </summary>
    private float _timer;
    /// <summary>
    /// Current block Task, when set a new one it's create a Grid of ingredients and add UIHolder if doens't exist 
    /// When seted to null Destory UIHolder
    /// </summary>
    public FoodTask Task { 
        get 
        {
            return _task;
        }
        set
        {
            _task = value;
            if (_task == null)
            {
                if (UIHolder != null)
                {
                    // TODO : Change to hide it and clean
                    Destroy(UIHolder.gameObject);
                }
            } else
            {
                if (UIHolder == null)
                {
                    UIHolder = Instantiate(Content.Instance.FoodTaskUI);
                    UIHolder.CreateFromTask(_task);
                    UIHolder.transform.SetParent(transform, false);
                    UIHolder.transform.localPosition = new Vector3(0, 1.5f, 0);
                    UIHolder.transform.localScale = new Vector3(.6f, .6f, .6f);
                }
                else
                {
                    UIHolder.CreateFromTask(_task);
                }
                UIHolder.gameObject.SetActive(true);
            }
        }
    }

    private new void Start()
    {
        base.Start();
        GameManager.Instance.Recievers.Add(this);
    }

    private void Update()
    {
        if (UIHolder == null)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                _timer = _TIME;
                if (Task == null)
                {
                    Task = FoodTaskManager.Instance.TakeTask();
                }
            }
            return;
        }
        // For each player on the map try to scale up when any player close to this block
        foreach (PlayerController player in PlayerController.players)
        {
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            if (distance < 2.5f && distance > 2f)
            {
                float scale = 1.5f / distance;
                if (scale > 1.2f) scale = 1.2f;
                UIHolder.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
    public override void Interact(PlayerController player)
    {
        if (Task == null) return;
        if (PlayerItemIsNotFoodEntity(player)) return;
        FoodEntity playerItem = player.CurrentItem as FoodEntity;

        // Remove player Item if task is finished and later remove this task in TaskManager
        if (Task.Compare(playerItem))
        {
            FoodTaskManager.Instance.RemoveTask(Task);
            Task = null;
            IconHide iconHide = Instantiate(Content.Instance.IconAccept);
            iconHide.transform.position = transform.position + Vector3.up;
            player.RemoveItem();
        }
    }
    /// <summary>
    /// Check player current item for food entity class
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private bool PlayerItemIsNotFoodEntity(PlayerController player)
    {
        if (player.CurrentItem == null) return true;
        if (player.CurrentItem is not FoodEntity) return true;
        return false;
    }
}
