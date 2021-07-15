using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearText : MonoBehaviour
{
    //handle
    private AudioSource _audiosource;
    private Text _text;

    //config
    private float _BGMduration = 5f;
    private float _fadeDuration = 5f;

    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        _audiosource.PlayDelayed(_BGMduration);

        _text = GetComponent<Text>();

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float currentTime = 0f;
        while (currentTime < _fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime/_fadeDuration);
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
