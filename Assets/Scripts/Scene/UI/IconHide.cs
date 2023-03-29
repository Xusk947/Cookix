using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHide : MonoBehaviour
{
    [SerializeField]
    private float _disappearTime = 10f;
    private float _lifeTime;
    [SerializeField]
    private Vector3 _moveDirection = Vector3.up;

    private Outline _outline;
    private Image _image;
    private bool _startDisappear;
    private void Start()
    {
        _outline = GetComponent<Outline>();
        _image = GetComponent<Image>();
        // Hide icon on Start for Showing animation
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
        _outline.effectColor = new Color(_outline.effectColor.r, _outline.effectColor.g, _outline.effectColor.b, 0);

        _lifeTime = _disappearTime;
    }

    private void Update()
    {
        transform.position += _moveDirection * Time.deltaTime;
        _lifeTime -= Time.deltaTime;
        // Alpha time changes by deltaTime
        float alpha = _image.color.a + Time.deltaTime;
        if (_lifeTime < 0f && alpha > 0.9f || _startDisappear)
        {
            // Start disappear when reach maximum alpha level 0.9f
            alpha = _image.color.a - Time.deltaTime;
            _startDisappear = true;
        };

        // Set alpha color
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
        _outline.effectColor = new Color(_outline.effectColor.r, _outline.effectColor.g, _outline.effectColor.b, alpha);

        // Destroy it self when fully dissapear
        if (alpha < 0f)
        {
            Destroy(gameObject);
        }
    }
}
