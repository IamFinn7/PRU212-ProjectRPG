using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundEffect;

    public AudioClip musicClip;
    // Start is called before the first frame update
    void Start()
    {
        soundEffect.clip = musicClip;
        soundEffect.Play();
    }

}
