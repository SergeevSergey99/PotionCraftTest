using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Camera))]
public class AspectRatioController : MonoBehaviour
{
    [SerializeField] private float _targetAspect = 16f / 9f;
    [SerializeField] private Camera _cam;

    void Start()
    {
        _cam = GetComponent<Camera>();
        UpdateAspectRatio();
    }

    void UpdateAspectRatio()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / _targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = _cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            _cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = _cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            _cam.rect = rect;
        }
    }

    void Update()
    {
        if (_cam.pixelRect.width != Screen.width || _cam.pixelRect.height != Screen.height)
        {
            UpdateAspectRatio();
        }
    }
    void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }
    private void OnValidate()
    {
        if (_cam == null)
            _cam = GetComponent<Camera>();
    }
}