using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public bool end = false;

    // Update is called once per frame
    void Update()
    {
        if (end) {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "LHand" || col.gameObject.name == "RHand") {
            end = true;
        }
    }
}
