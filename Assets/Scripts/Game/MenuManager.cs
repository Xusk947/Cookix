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
    [SerializeField]
    private LeanTweenType _outType;

    private void Update()
    {
        if (Camera.main == null) return;
        Camera.main.transform.position += new Vector3(0, 0, 0.01f * Time.deltaTime);
        if (Camera.main.transform.position.z > 5f) Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
    }
    public void OnStartButtonClick()
    {
        ChangeScene();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("BaseTutorial", LoadSceneMode.Single);
    }

    public void OnSettingsButtonClick()
    {

    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
