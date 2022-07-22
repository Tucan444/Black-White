using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LightPathFollow : MonoBehaviour
{
    public PathCreator path;
    public float speed = 3f;
    public float accelRange = 2;
    public float normalization = 0.2f;
    float dist = 0;
    float defSped;

    void Start() {
        defSped = speed;
    }

    // Update is called once per frame
    void Update()
    {
        dist += speed * Time.deltaTime;
        float y = transform.position.y;
        transform.position = path.path.GetPointAtDistance(dist);
        speed = Mathf.Min(Mathf.Max(defSped-accelRange, speed - (transform.position.y - y)), defSped + (accelRange * 2));
        speed = speed + (normalization * Time.deltaTime * (defSped - speed));
    }
}
