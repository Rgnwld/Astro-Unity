using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundConfigurations : MonoBehaviour
{
    public AudioClip hitAudio, jumpAudio;
    AudioSource audSource;

    private void Awake()
    {
        audSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayerMovementV2.instance.moveAction += MoveSFX;
    }

    void MoveSFX(Vector2 axis)
    {
        if (Mathf.Abs(axis.x) > 0 || Mathf.Abs(axis.y) > 0)
        {
            audSource.clip = jumpAudio;
            audSource.Play();
        }
    }
}
