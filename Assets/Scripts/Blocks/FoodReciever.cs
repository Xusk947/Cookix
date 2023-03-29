using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodReciever : Block
{
    private const float _TIME = 5f;
    private FoodTask _task;
    private FoodTaskImage UIHolder;
    private float _timer;

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
        GameManager.Instance.recievers.Add(this);
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
            foreach (PlayerController player in PlayerController.players)
        {
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            if (distance < 2.5f)
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
            player.RemoveItem();
        }
    }

    private bool PlayerItemIsNotFoodEntity(PlayerController player)
    {
        if (player.CurrentItem == null) return true;
        if (player.CurrentItem is not FoodEntity) return true;
        return false;
    }
}
