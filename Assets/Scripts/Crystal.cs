using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crystal : MonoBehaviour
{
    public Material black;
    public Material pureBlack;
    public Material pureWhite;
    public GameObject ground;
    public GameObject icos;
    public float areaEffectBlack = 5;
    public float areaIncrement = 1;
    public float timeIncrement = 0.05f;

    public GameObject bgS;
    public float ttf = 1;

    AudioSource bg;
    float initV;
    public bool ending = false;

    public int end = 400;
    int counter = 0;

    public bool started = false;
    bool initialization = true;
    float duration = 0;
    float ld = 0;

    int n = 0;

    Renderer[] icoTriangles;
    Renderer temp;
    // Start is called before the first frame update
    void Start()
    {
        icoTriangles = icos.GetComponentsInChildren<Renderer>();
        for (int i = 0; i <icoTriangles.Length; i++) {
            icoTriangles[i].material = pureBlack;
        }

        for (int i = 0; i < icoTriangles.Length; i++) {
             int rnd = Random.Range(0, icoTriangles.Length);
             temp = icoTriangles[rnd];
             icoTriangles[rnd] = icoTriangles[i];
             icoTriangles[i] = temp;
        }

        bg = bgS.GetComponent<AudioSource>();
        initV = bg.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (started) {

            if (initialization) {
                var ren = ground.GetComponent<Renderer>();
                ren.material = black;
            }

            duration += Time.deltaTime;
            if (duration - ld > timeIncrement) {
                counter++;

                ld = duration;

                areaEffectBlack += areaIncrement;
                black.SetFloat("AreaEffect", areaEffectBlack);

                if (n < icoTriangles.Length) {
                    icoTriangles[n].material = pureWhite;
                    n += 1;
                }

                if (counter > end) {
                    ending = true;
                    started = false;
                    duration = 0;
                }
            }
        }
        
        if (ending) {
            duration += Time.deltaTime;
            bg.volume -= initV * (Time.deltaTime / ttf);
            if (duration > ttf) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        started = true;
    }
}
