using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance { get; private set; }
    private Canvas _canvas;
    // -- Game UI Group
    private GameObject _game;
    private TextColorAnimation _textScore;
    private TextColorAnimation _textTimer;
    // -- Setting Group
    private GameObject _settings;
    private Image _background;
    private Color _baseColor;
    private Color _preferColor;
    private Color _empty = new Color();

    private void Start()
    {
        Instance = this;
        _canvas = GetComponent<Canvas>();

        _game = _canvas.transform.Find("Game").gameObject;  
        _textScore = _game.transform.Find("Timer").GetComponentInChildren<TextColorAnimation>();
        _textTimer = _game.transform.Find("Score").GetComponentInChildren<TextColorAnimation>();

        _settings = _canvas.transform.Find("Settings").gameObject;
        _background = _settings.transform.Find("Background").GetComponent<Image>();

        _baseColor = _background.color;
        _preferColor = _empty;
        _settings.SetActive(false);
        _game.SetActive(true);
        ChangeScore(0);
    }

    private void Update()
    {

    }

    public void ChangeScore(float score)
    {
        _textScore.Text.text = ((int)score).ToString();
        _textScore.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
        _textScore.Text.transform.eulerAngles += new Vector3(0, 0, Random.Range(10f, 20f));
        _textScore.Text.color = Color.yellow;
    }

    public void Show()
    {
        _preferColor = _baseColor;
        _settings.SetActive(true);
    }

    public void Hide()
    {
        _preferColor = _empty;
        _settings.SetActive(false);
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
