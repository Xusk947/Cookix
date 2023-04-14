using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FoodReciever : Block
{
    public bool isEmpty = true;
    public ClientController Client;
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
    private float _minScale = 0.5f;
    private float _maxScale = 1f;
    private float _targetScale = 1f;
    [SerializeField]
    private float _angleToStay = 0f;
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
                    UIHolder.transform.localScale = new Vector3(_minScale, _minScale, _minScale);
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
        if (UIHolder == null) return;
        _targetScale = _minScale;
        // For each player on the map try to scale up when any player close to this block
        foreach (PlayerController player in PlayerController.players)
        {
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            if (distance < 1.5f)
            {
                _targetScale = _maxScale;
                continue;
            }
        }
        if (Client != null)
        {
            UIHolder.UpdateBackground(Client.WaitTimer / Client.WaitTime);

            float scale = Mathf.Lerp(UIHolder.transform.localScale.x, _targetScale, Time.deltaTime * 2.5f);
            UIHolder.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
    public override void Interact(ChefController player)
    {
        if (Task == null) return;
        if (PlayerItemIsNotPlateEntity(player)) return;
        FoodEntity playerItem = (player.CurrentItem as PlateEntity).ItemOn;

        // Remove player Item if task is finished and later remove this task in TaskManager
        if (Task.Compare(playerItem))
        {
            FoodTaskManager.Instance.RemoveTask(Task);
            Client.GetAnOrder();
            Task = null;
            IconHide iconHide = Instantiate(Content.Instance.IconAccept);
            iconHide.transform.position = transform.position + Vector3.up;
            player.RemoveItem();
        }
    }

    public void CancelOrder()
    {
        FoodTaskManager.Instance.RemoveTask(Task);
        Task = null;
        IconHide iconHide = Instantiate(Content.Instance.IconCancel);
        iconHide.transform.position = transform.position + Vector3.up;
    }

    /// <summary>
    /// Check player current item for food entity class
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private bool PlayerItemIsNotPlateEntity(ChefController player)
    {
        if (player.CurrentItem == null) return true;
        if (player.CurrentItem is not PlateEntity) return true;
        return false;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Recievers.Remove(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector3 pos = GetPositionForClient();

        Gizmos.DrawWireSphere(pos, 0.2f);
    }

    public Vector3 GetPositionForClient()
    {
        return transform.position + RotateForward(transform, _angleToStay);
    }
    private static Vector3 RotateForward(Transform transform, float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        return rotation * transform.forward;
    }
}
