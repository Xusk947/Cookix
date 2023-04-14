using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(LayoutElement))]
[RequireComponent(typeof(Shadow))]
[RequireComponent(typeof(EventTrigger))]
public class ButtonSelector : MonoBehaviour
{
    private LayoutElement _layoutElement;
    private Image _image;
    private TextMeshProUGUI _text;
    private Shadow _shadow;

    [SerializeField]
    private Color _backgroundColorToSet = Color.yellow;
    [SerializeField]
    private Color _textColorToSet = Color.white;
    [SerializeField]
    private Color _preferColor, _preferTextColor;
    private Color _baseColor, _baseTextColor;
    private float _preferSize, _speed = 10f;
    private void Awake()
    {
        _layoutElement = GetComponent<LayoutElement>();
        _image = GetComponent<Image>();
        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _shadow = GetComponent<Shadow>();
        _shadow.enabled = false;

        _baseColor = _image.color - new Color(0, 0, 0, 0.35f);
        _image.color = _baseColor;
        _baseTextColor = _text.color;

        _preferColor = _baseColor;
        _preferTextColor = _baseTextColor;
        _preferSize = 1.0f;
    }
    public Color GetDefaultColor()
    {
        return _baseColor;
    }

    public Color GetDefaultTextColor()
    {
        return _baseTextColor;
    }
    public void SetColor(Color color)
    {
        _image.color = color;
        _text.color = color;
    }

    public void ChangePreferColor(Color color, Color textColor)
    {
        _preferColor = color;
        _preferTextColor = textColor;
    }

    private void Update()
    {
        _image.color = Color.Lerp(_image.color, _preferColor, _speed * Time.unscaledDeltaTime);
        _text.color = Color.Lerp(_text.color, _preferTextColor, _speed * Time.unscaledDeltaTime);

        float calculatedSize = Mathf.Lerp(transform.localScale.x, _preferSize, 10f * Time.unscaledDeltaTime);
        transform.localScale = new Vector3(calculatedSize, calculatedSize, calculatedSize);
    }

    public void OnPointEntter()
    {
        _layoutElement.layoutPriority = 2;
        _shadow.enabled = true;
        _preferSize = 1.2f;
        _preferColor = _backgroundColorToSet;
        _preferTextColor = _textColorToSet;
        _speed = 10f;
        HighLight();
    }

    public void OnPointExit()
    {
        _layoutElement.layoutPriority = 1;
        _shadow.enabled = false;
        _preferColor = _baseColor;
        _preferTextColor = _baseTextColor;
        _preferSize = 1.0f;
        _speed = 50f;
    }

    public void HighLight(float speed = 10f)
    {
        _image.color = _backgroundColorToSet;
        _text.color = _textColorToSet;
        _speed = speed;
    }

}
