using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewTutorial : MonoBehaviour
{

    public CanvasGroup _canvasGroup;
    private bool _isRender;

    void Start()
    {
        _canvasGroup.alpha = 0;
    }

    void Update()
    {
        if (_canvasGroup.alpha != 1) return;
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            StartCoroutine(FadeOut());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isRender) return;
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeIn());
            _isRender = true;
        }
    }

    private IEnumerator FadeIn()
    {
        while(_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += 1f * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= 1f * Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
