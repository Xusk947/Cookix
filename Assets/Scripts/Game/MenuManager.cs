using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Canvas _canvas;
    private GameObject _buttonStart, _buttonSettings, _buttonExit;
    [SerializeField]
    private LeanTweenType _outType;

    private void Start()
    {
        GameObject startMenu = _canvas.transform.Find("StartMenu").gameObject;
        _buttonStart = startMenu.transform.Find("Start").gameObject;
        _buttonSettings = startMenu.transform.Find("Settings").gameObject;
        _buttonExit = startMenu.transform.Find("Exit").gameObject;
    }

    private void Update()
    {
        _camera.transform.position += new Vector3(0, 0, 0.01f * Time.deltaTime);
        if (_camera.transform.position.z > 5f) _camera.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    public void OnStartButtonClick()
    {
        ChangeScene();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Level-1-1", LoadSceneMode.Single);
    }

    public void OnSettingsButtonClick()
    {

    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
