using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            var lightData = GetComponent<DarkLight>();
            Color c = new Color(0.5f, 0.8f, 0.5f, 1);
            lightData.color = c;
        }
    }
}
