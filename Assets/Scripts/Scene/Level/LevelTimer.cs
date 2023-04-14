using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTimer : LevelController
{
    [SerializeField, Description("In Seconds")]
    private int _waitTime = 120;
    private int _finishLevelCooldown = 3;
    private float _timer;
    protected override void Start()
    {
        base.Start();
        _timer = 1f;

        InGameUI.Instance.ChangeWaitTime(_waitTime);
    }

    protected override void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _waitTime -= 1;
            _timer = 1f;
           InGameUI.Instance.ChangeWaitTime(_waitTime);
           if (_waitTime <= 0)
            {
                if (_finishLevelCooldown == 3)
                {
                    FinishLevel();
                }
                _finishLevelCooldown -= 1;
                if (_finishLevelCooldown <= 0)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }

    protected override void FinishLevel()
    {
        InGameUI.Instance.ChangeWaitTimeText("Time's Up!");
    }
}
