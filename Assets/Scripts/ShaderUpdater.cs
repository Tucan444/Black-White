using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class ShaderUpdater : MonoBehaviour
{
    public Material black;
    public Material white;
    private XROrigin xROrigin;
    // Start is called before the first frame update
    void Start()
    {
        xROrigin = GetComponent<XROrigin>();
        Vector3 v = xROrigin.CameraInOriginSpacePos;
        black.SetVector("PlayerPos", transform.position + new Vector3(v.x, 0, v.z));
        white.SetVector("PlayerPos", transform.position + new Vector3(v.x, 0, v.z));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = xROrigin.CameraInOriginSpacePos;
        black.SetVector("PlayerPos", transform.position + new Vector3(v.x, 0, v.z));
        white.SetVector("PlayerPos", transform.position + new Vector3(v.x, 0, v.z));
    }
}
