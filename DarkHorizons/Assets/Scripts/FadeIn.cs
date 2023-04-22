using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{

    public float fadeTime = 0.8f;

    void Start()
    {
        StartCoroutine(Fade(GetComponent<SpriteRenderer>()));
    }

    IEnumerator Fade(SpriteRenderer _sprite)
    {

        Color tmpColor = _sprite.color;
        while (tmpColor.a < 1f)
        {

            tmpColor.a += Time.deltaTime / fadeTime;
            _sprite.color = tmpColor;
            if (tmpColor.a >= 1f)
            {

                tmpColor.a = 1f;

            }

            yield return null;

        }

        _sprite.color = tmpColor;

    }
}
