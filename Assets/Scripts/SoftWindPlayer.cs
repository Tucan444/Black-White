using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoftWindPlayer : MonoBehaviour
{
    public AudioSource aso;

    float soundTime = 36;
    float waitTime = 1;
    float elapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = Random.Range(0, 60);
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > waitTime) {
            aso.Play();
            elapsed = 0;
            waitTime = soundTime + Random.Range(0, 60);
        }
    }
}
