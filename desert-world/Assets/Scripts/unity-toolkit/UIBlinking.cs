using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlinking : MonoBehaviour {
    public float speed = 5f;
    public float darker = 0.5f;

    public bool IsBlinking;
    public bool InheritBlinkingFromParent;

    private Image image;

    private bool _isBlinking;

    private Color imageStartColor;


    private void Awake () {
        image = GetComponentInChildren<Image>();
    }

    public void Set (bool enabled) {
        this.IsBlinking = enabled;
    }

    private void OnDisable () {
        Set(false);
        Update();
    }

    private void Update () {
        if (!gameObject.activeInHierarchy) return;

        if (InheritBlinkingFromParent) {
            if (transform.parent != null) {
                var b = transform.parent.GetComponentInParent<UIBlinking>();
                IsBlinking = b != null && b.IsBlinking;
            }
            else {
                IsBlinking = false;
            }
        }

        if (_isBlinking != IsBlinking) {
            _isBlinking = IsBlinking;
            StopAllCoroutines();
            if (!IsBlinking) {
                if (image != null) image.color = imageStartColor;
            } else {
                if (image != null) imageStartColor = image.color;
                StartCoroutine(CoBlink());
            }
        }
    }

    IEnumerator CoBlink () {
        float t0 = Time.time;
        while (true) {
            yield return null;
            if (image != null) {
                image.color = AnimColor(imageStartColor, Time.time - t0);
            }
        }
    }

    private Color AnimColor (Color color, float f) {
        float ff = 1f - (Mathf.Cos(f * speed) + 1f) / 2f;
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        v = Mathf.Lerp(v, v * darker, ff);
        return Color.HSVToRGB(h, s, v);
    }
}
