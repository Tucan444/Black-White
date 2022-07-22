using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tetrahedron : MonoBehaviour
{
    [SerializeField] InputActionAsset waveControls;

    InputAction pressed;

    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 directional_light = new Vector3(1, -1, 1);
    public float Ambiance = 0.8f;
    public float size = 10;
    public float tolerance = 0.1f;

    public Vector3 n0;
    public Vector3 n1;
    public Vector3 n2;
    public Vector3 n3;
    // Start is called before the first frame update
    void Start()
    {
        directional_light = directional_light.normalized;
        Generate();

        var gameplayActionMap = waveControls.FindActionMap("XRI RightHand");
        pressed = gameplayActionMap.FindAction("A");

        pressed.performed += Pressed;
        pressed.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate() {
        float a = 1 - tolerance;
        float r = size * a;
        float minD = Math.Max(0, 1.8f - (2 * tolerance));
        p0 = new Vector3(UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size));
        p1 = new Vector3(UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size));
        p2 = new Vector3(UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size));
        p3 = new Vector3(UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size));

        if (p0.magnitude < r) {
            p0 = p0.normalized * r;
        }
        if (p1.magnitude < r) {
            p1 = p1.normalized * r;
        }
        if (p2.magnitude < r) {
            p2 = p2.normalized * r;
        }
        if (p3.magnitude < r) {
            p3 = p3.normalized * r;
        }

        while  ((p3 - p2).magnitude < minD) {
            p3 = new Vector3(UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size));
            if (p3.magnitude < r) {
                p3 = p3.normalized * r;
            }
        }
        while  ((p0 - p1).magnitude < minD) {
            p0 = new Vector3(UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size), UnityEngine.Random.Range(-size, size));
            if (p0.magnitude < r) {
                p0 = p0.normalized * r;
            }
        }

        if (Vector3.Dot((p3 - p0).normalized, Vector3.Cross(p1 - p0, p2 - p0).normalized) > 0) {
            n0 = Vector3.Cross(p1 - p0, p2 - p0).normalized;
            n1 = Vector3.Cross(p0 - p1, p3 - p1).normalized;
            n2 = Vector3.Cross(p1 - p2, p3 - p2).normalized;
            n3 = Vector3.Cross(p0 - p3, p2 - p3).normalized;
        } else {
            n0 = -Vector3.Cross(p1 - p0, p2 - p0).normalized;
            n1 = -Vector3.Cross(p0 - p1, p3 - p1).normalized;
            n2 = -Vector3.Cross(p1 - p2, p3 - p2).normalized;
            n3 = -Vector3.Cross(p0 - p3, p2 - p3).normalized;
        }

        p0 += transform.position;
        p1 += transform.position;
        p2 += transform.position;
        p3 += transform.position;
    }

    void Pressed(InputAction.CallbackContext context) {
        Generate();
    }
}
