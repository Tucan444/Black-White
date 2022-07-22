using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CastleSceneSounds : MonoBehaviour
{
    public AudioSource trigger;
    public AudioSource eerie;
    bool played = false;

    private void OnTriggerEnter(Collider other)
    {
        if (played == false) {
            trigger.Play();
            eerie.Play();
            played = true;
        }
    }
}
