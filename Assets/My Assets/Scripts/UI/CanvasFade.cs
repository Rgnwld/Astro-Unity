using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFade : MonoBehaviour
{
    public static CanvasFade instance;
    Image imgRef;
    [SerializeField] float fadeVelocity = 1.0f;
    [SerializeField][Range(0, 1)] float defaultStartFadeState = 0.0f;
    bool canFade = false;

    private void Awake()
    {
        instance = this;
        imgRef = GetComponent<Image>();
        SetFadeState(defaultStartFadeState);
        SwitchFadeState(false);
    }

    private void Update()
    {
        if (canFade)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    public void SwitchFadeState(bool fadeState)
    {
        canFade = fadeState;
    }

    void SetFadeState(float state)
    {
        imgRef.material.SetFloat("_FadePercentage", state);
    }

    float GetFadeState()
    {
        return imgRef.material.GetFloat("_FadePercentage");
    }

    void FadeIn()
    {
        if (imgRef.material.GetFloat("_FadePercentage") < 1)
        {
            SetFadeState(GetFadeState() + fadeVelocity * Time.deltaTime);
        }
    }

    void FadeOut()
    {
        if (imgRef.material.GetFloat("_FadePercentage") > 0)
        {
            SetFadeState(GetFadeState() - fadeVelocity * Time.deltaTime);
        }
    }
}
