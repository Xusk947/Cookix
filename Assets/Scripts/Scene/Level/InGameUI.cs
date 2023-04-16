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
    // -- Round Start Group
    private GameObject _startTimer;
    // -- Game UI Group
    private GameObject _game;
    private TextColorAnimation _textScore;
    private TextColorAnimation _textTimer;
    // -- Game Settings 
    private GameObject _gameSettings;
    // -- Setting Group
    private GameObject _settings;
    private Image _background;
    private Color _baseColor;
    private Color _preferColor;
    private Color _empty = new Color();
    // -- Tutorial Group
    private bool _tutorialClosed;
    private GameObject _tutorial;
    // THIS SHITTY CODE SHOULD BE REMOVED FROM PROJECT BUT IT STILL ALIVE BECAUSE I'M LAZY 
    private void Awake()
    {
        Instance = this;

        _canvas = GetComponent<Canvas>();

        _game = _canvas.transform.Find("Game").gameObject;  
        _textScore = _game.transform.Find("Score").GetComponentInChildren<TextColorAnimation>();
        _textTimer = _game.transform.Find("Timer")?.GetComponentInChildren<TextColorAnimation>();

        Transform tutorialTransform = _canvas.transform.Find("Tutorial");
        _tutorialClosed = true;
        if (tutorialTransform != null)
        {
            _tutorial = tutorialTransform.gameObject;
            if (_tutorial.transform.childCount > 0)
            {
                Time.timeScale = 0.0f;
                _tutorialClosed = false;
            }
        }
        
    }
    private void Start()
    {
        if (_tutorial != null) _tutorial.SetActive(true);
        _settings = _canvas.transform.Find("Settings").gameObject;
        _background = _settings.transform.Find("Background").GetComponent<Image>();

        _baseColor = _background.color;
        _preferColor = _empty;
        _settings.SetActive(false);
        if (!_tutorialClosed)
        {
            _game.SetActive(false);
        }
        _game.SetActive(true);
    }

    private void Update()
    {

    }

    public void ChangeScore(float score, bool positive)
    {
        if (_textScore == null || _textScore.Text == null) return;
        float angle = Random.Range(10f, 20f) * (Random.Range(0, 1) > 0.5 ? 1 : -1);
        _textScore.Text.text = ((int)score).ToString();
        _textScore.Text.transform.localScale += (new Vector3(0.5f, 0.5f, 0.5f) * (positive ? 1 : -1));
        _textScore.Text.transform.eulerAngles += new Vector3(0, 0, angle);
        _textScore.Text.color = positive ? Color.yellow : Color.red;
    }

    public void ChangeWaitTime(int time)
    {
        _textTimer.Text.text = time.ToString();
        _textTimer.Text.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void ChangeWaitTimeText(string text)
    {
        _textTimer.Text.text = text;
        _textTimer.Text.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
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

    public void OnTutorialButtonClick()
    {
        _game.SetActive(true);
        RemoveTutorial();
    }
    private void RemoveTutorial()
    {
        _tutorial.SetActive(false);
        GameManager.Instance.Resume();
    }
}
