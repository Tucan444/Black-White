using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLight : MonoBehaviour
{
    public Color color = new Color(0, 0, 0, 0);
    public GameObject PointLight;
    public GameObject FlashLight;
    public GameObject LightWave;
    public GameObject Tetrahedron;

    Material material;
    Vector3 direction_vector;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        material.SetColor("ObjectColor", color);

        var pointData = PointLight.GetComponent<PointLight>();
        material.SetColor("_Color", pointData.color);
        material.SetVector("_LightPos", PointLight.transform.position);
        material.SetFloat("_Power", pointData.power);

        var flashData = FlashLight.GetComponent<FlashLight>();
        material.SetFloat("FlashPower", flashData.flashPower);
        material.SetVector("FlashPos", FlashLight.transform.position);
        direction_vector = FlashLight.transform.rotation * Vector3.forward;
        material.SetVector("FlashDirection", direction_vector);
        material.SetFloat("FlashLimit", flashData.flashLimit);

        var waveData = LightWave.GetComponent<LightWave>();
        material.SetFloat("WaveHalfWidthInversed", waveData.halfWidthInversed);
        material.SetVector("WaveCenter", LightWave.transform.position);
        material.SetFloat("WaveRadius", waveData.radius);
        material.SetInt("WaveActive", waveData.active);

        var tetrahedronData = Tetrahedron.GetComponent<Tetrahedron>();  // object name is component name
        material.SetFloat("Ambiance", tetrahedronData.Ambiance);
        material.SetVector("DirectionalLight", tetrahedronData.directional_light);
        material.SetVector("P0", tetrahedronData.p0);
        material.SetVector("P1", tetrahedronData.p1);
        material.SetVector("P2", tetrahedronData.p2);
        material.SetVector("P3", tetrahedronData.p3);
        material.SetVector("N0", tetrahedronData.n0);
        material.SetVector("N1", tetrahedronData.n1);
        material.SetVector("N2", tetrahedronData.n2);
        material.SetVector("N3", tetrahedronData.n3);

        GetComponent<MeshRenderer> ().material = material;
    }
}
