using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LevelTimer : LevelController
{
    [SerializeField, Description("In Seconds")]
    private int _waitTime = 60;
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
        }
    }
}
