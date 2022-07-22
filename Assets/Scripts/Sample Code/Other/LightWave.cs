using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightWave : MonoBehaviour
{
    [SerializeField] InputActionAsset waveControls;
    InputAction pressed;

    public int active = 0;

    public float waveSpeed = 12;
    public float radius = 0;
    public float halfWidth = 2;
    public float halfWidthInversed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        var gameplayActionMap = waveControls.FindActionMap("XRI LeftHand");
        pressed = gameplayActionMap.FindAction("X");

        pressed.performed += OnWaveToggle;
        pressed.canceled += OnWaveToggle;
        pressed.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (active == 1) {
            radius += waveSpeed * Time.deltaTime;
        }
    }

    void OnWaveToggle(InputAction.CallbackContext context) {
        if (active == 1) {
            active = 0;
            radius = 0;
        } else {
            active = 1;
        }
    }
}
