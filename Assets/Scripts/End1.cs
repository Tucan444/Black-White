using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class End1 : MonoBehaviour
{
    public bool end = false;
    public GameObject bgS;
    public float ttf = 1;
    AudioSource bg;
    float duration = 0;
    float initV;

    void Start() {
        bg = bgS.GetComponent<AudioSource>();
        initV = bg.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (end) {
            duration += Time.deltaTime;
            bg.volume -= initV * (Time.deltaTime / ttf);
            if (duration > ttf) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        end = true;
    }
}
