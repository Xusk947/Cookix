using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InFinishUI : MonoBehaviour
{

    private Canvas _canvas;
    private RectTransform _buttonGroup, _scoreGroup;
    private TextMeshProUGUI _textScore;
    private List<ButtonSelector> _buttons;

    private int _state;
    // Begin Animation Variables
    private float _defaultWidth, _defaultPos;
    // Score Animation Variables
    private int _currentScore = 0;
    private float _speed = 1f;
    // Button Show Animation;
    private float _buttonTimer = 0.25f;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _buttonGroup = transform.Find("ButtonGroup").GetComponent<RectTransform>();
        _scoreGroup = transform.Find("ScoreGroup").GetComponent<RectTransform>();
        _textScore = _scoreGroup.Find("ScoreVar").GetComponent<TextMeshProUGUI>();

        _defaultWidth = _scoreGroup.sizeDelta.x;
        _defaultPos = _scoreGroup.position.x;

        _scoreGroup.sizeDelta = new Vector2(0, _scoreGroup.sizeDelta.y);
        _scoreGroup.position = new Vector2(0, _scoreGroup.position.y);

        _buttons = new List<ButtonSelector>();
        for(int i = 0; i < _buttonGroup.childCount; i++)
        {
            ButtonSelector button = _buttonGroup.GetChild(i).GetComponent<ButtonSelector>();
            button.SetColor(new Color());
            button.gameObject.SetActive(false);
            _buttons.Add(button);
        }
    }

    private void Update()
    {
        _textScore.color = Color.Lerp(_textScore.color, Color.white, Time.deltaTime);

        switch(_state)
        {
            case 0:
                BeginAnimations();
                break;
            case 1:
                ScoreAnimation();
                break;
            case 2:
                ShowButtons();
                break;
        }
    }

    private void BeginAnimations()
    {
        bool finished = true;
        if (_scoreGroup.sizeDelta.x < _defaultWidth - 1)
        {
            finished = false;
            _scoreGroup.sizeDelta = new Vector2(Mathf.Lerp(_scoreGroup.sizeDelta.x, _defaultWidth, Time.deltaTime * 10f), _scoreGroup.sizeDelta.y);
        }
        if (_scoreGroup.position.x < _defaultPos - 1)
        {
            finished = false;
            _scoreGroup.position = new Vector2(Mathf.Lerp(_scoreGroup.position.x, _defaultPos, Time.deltaTime * 10f), _scoreGroup.position.y);
        }
        if (finished) _state++;
    }

    private void ScoreAnimation()
    {
        _speed += 0.01f;

        if (LevelData.Instance == null) new LevelData();

        if (_currentScore != LevelData.Instance.Score)
        {
            int _lastScore = _currentScore;
            _currentScore = ((int) Mathf.Lerp(_currentScore, LevelData.Instance.Score, Time.deltaTime * _speed));
            _textScore.text = _currentScore.ToString();
            if (_lastScore != _currentScore)
            {
                _textScore.transform.localScale += new Vector3(0.025f, 0.025f, 0.025f);
            }
        }
        if (_textScore.transform.localScale.x > 1)
        {
            float scale = Mathf.Lerp(_textScore.transform.localScale.x, 0.9f, Time.deltaTime * 10f);
            _textScore.transform.localScale = new Vector3(scale, scale, scale);
            return;
        }
        _textScore.text = LevelData.Instance.Score.ToString();
        _state++;
    }

    public void OnNextButton()
    {
        SceneManager.LoadScene(LevelData.Instance.SceneID + 1);
    }

    public void OnExitButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(LevelData.Instance.SceneID);
    }

    private void ShowButtons()
    {
        foreach (ButtonSelector button in _buttons)
        {
            button.gameObject.SetActive(true);
        }

        _buttonTimer -= Time.deltaTime;
        if (_buttons.Count > 0)
        {
            if (_buttonTimer > 0) return;
            _buttonTimer = 0.25f;
            ButtonSelector button = _buttons.First();
            button.HighLight(2.5f);
            button.transform.localScale *= 1.2f;
            button.ChangePreferColor(button.GetDefaultColor(), button.GetDefaultTextColor());
            _buttons.Remove(button);
            return;
        }
        _state++;
    }
}
