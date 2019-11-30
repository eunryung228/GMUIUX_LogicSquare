using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Image img_fade;
    public float fadeTime = 1.5f;

    float start;
    float end;
    float time = 0f;
    bool isPlaying = false;


    public void OutStartFadeIAnimation()
    {
        if (isPlaying)
            return;

        start = 1f;
        end = 0f;
        StartCoroutine(fade());
    }

    public void InStartFadeIAnimation()
    {
        if (isPlaying)
            return;

        start = 0f;
        end = 1f;
        StartCoroutine(fade());
    }

    IEnumerator fade()
    {
        isPlaying = true;
        Color color = img_fade.color;
        time = 0f;
        color.a = Mathf.Lerp(start, end, time);

        while (color.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(start, end, time);
            img_fade.color = color;
            yield return null;
        }
        isPlaying = false;
    }

    IEnumerator fadeOut()
    {
        isPlaying = true;
        Color color = img_fade.color;
        time = 0f;
        color.a = Mathf.Lerp(start, end, time);

        while (color.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(start, end, time);
            img_fade.color = color;
            yield return null;
        }
        isPlaying = false;
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
