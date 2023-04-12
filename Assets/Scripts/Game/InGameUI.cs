using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class InGameUI : MonoBehaviour
{
    private Canvas _canvas;
    private GameObject _settings;
    private Image _background;
    private Color _baseColor;
    private Color _preferColor;
    private Color _empty = new Color();

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _settings = _canvas.transform.Find("Settings").gameObject;
        _background = _settings.transform.Find("Background").GetComponent<Image>();

        _baseColor = _background.color;
        _preferColor = _empty;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_background.color != _preferColor)
        {
            _background.color = Color.Lerp(_background.color, _preferColor, 10f * Time.deltaTime);
            if (_background.color == _empty)
            {
            }
        }
    }

    public void Show()
    {
        _preferColor = _baseColor;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _preferColor = _empty;
        gameObject.SetActive(false);
    }
    public void OnContinueButtonClick()
    {
        GameManager.Instance.Resume();
    }

    public void OnSettingsButtonClick()
    {

    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
