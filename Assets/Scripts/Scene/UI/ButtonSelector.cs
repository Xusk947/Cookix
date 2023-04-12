using System.Collections;
using System.Collections.Generic;
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

    private Color _preferColor, _preferTextColor;
    private Color _baseColor, _baseTextColor;
    private float _preferSize, _speed;
    private void Start()
    {
        _layoutElement = GetComponent<LayoutElement>();
        _image = GetComponent<Image>();
        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _shadow = GetComponent<Shadow>();
        _shadow.enabled = false;

        _baseColor = _image.color - new Color(0, 0, 0, 0.35f);
        _image.color = _baseColor;
        _baseTextColor = _text.color;

        _preferColor = new Color();
        _preferSize = 1.0f;
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
        _preferColor = Color.yellow;
        _preferTextColor = Color.white;
        _preferSize = 1.2f;
        _speed = 10f;
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

}
