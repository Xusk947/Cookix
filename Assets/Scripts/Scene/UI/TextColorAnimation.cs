using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TextColorAnimation : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI Text;
    [SerializeField]
    private Color _defaultColor = Color.white;
    [SerializeField]
    private Color _defaultTextColor = Color.black;
    [SerializeField]
    private float _speed = 10f;

    private Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
        Text = transform.GetComponentInChildren<TextMeshProUGUI>();    
    }

    void Update()
    {
        if (_image.color != _defaultColor) _image.color = Color.Lerp(_image.color, _defaultColor, Time.deltaTime * _speed);
        if (Text.color != _defaultTextColor) Text.color = Color.Lerp(Text.color, _defaultTextColor, Time.deltaTime * _speed);
        if (_image.transform.localScale.x > 1) _image.transform.localScale = Vector3.Lerp(_image.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * _speed);
        if (Text.transform.eulerAngles.z > 0) Text.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(Text.transform.eulerAngles.z, 0, Time.deltaTime * _speed));
    }
}
