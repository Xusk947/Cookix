using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class LevelController : MonoBehaviour
{
    [SerializeField, ReadOnly(true)]
    private LevelData _data;
    protected virtual void Start()
    {
        _data = new LevelData
        {
            SceneID = SceneManager.GetActiveScene().buildIndex
        };
        Events.ClientOrderFinish += ClientOrderFinish;
        Events.ClientOrderFail += ClientOrderFail;
    }

    protected abstract void Update();

    private void ClientOrderFinish(object sender, ClientArgs args)
    {
        _data.Score += args.Client.Order.difficult * 100f;
        InGameUI.Instance.ChangeScore(_data.Score, true);
    }

    private void ClientOrderFail(object sender, ClientArgs args)
    {
        _data.Score -= 0.5f * args.Client.Order.difficult * 100f;
        InGameUI.Instance.ChangeScore(_data.Score, false);
    }

    protected virtual void FinishLevel()
    {
        InGameUI.Instance.ChangeWaitTimeText("Time's Up!");
    }

    public void LoadNext()
    {

    }

    public void ExitToMainMenu()
    {

    }

    private void OnDestroy()
    {
        Events.ClientOrderFinish -= ClientOrderFinish;
        Events.ClientOrderFail -= ClientOrderFail;
    }
}
