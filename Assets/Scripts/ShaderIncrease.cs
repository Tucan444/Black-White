using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class ShaderIncrease : MonoBehaviour
{
    public Material black;
    public float maxAE = 4;
    public float ae = -1;
    [SerializeField][Range(0, 1)] public float sped = 0.5f;
    private XROrigin xROrigin;
    // Start is called before the first frame update
    void Start()
    {
        xROrigin = GetComponent<XROrigin>();
        Vector3 v = xROrigin.CameraInOriginSpacePos;
        black.SetVector("PlayerPos", transform.position + new Vector3(-v.z, 0, v.x));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = xROrigin.CameraInOriginSpacePos;
        black.SetVector("PlayerPos", transform.position + new Vector3(-v.z, 0, v.x));
        ae += Time.deltaTime * sped;
        ae = Mathf.Min(ae, maxAE);
        black.SetFloat("AreaEffect", ae);
    }
}
