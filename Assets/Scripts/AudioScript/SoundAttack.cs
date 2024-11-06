using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAttack : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            audioManager.PlaySFX(audioManager.actack);
        }
    }
}