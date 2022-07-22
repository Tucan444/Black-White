using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject PointLight;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        var pointData = PointLight.GetComponent<PointLight>();
        material.SetVector("OrbPos", PointLight.transform.position);
    }
}
