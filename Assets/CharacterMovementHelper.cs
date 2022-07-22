using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterMovementHelper : MonoBehaviour
{
    [SerializeField] InputActionAsset controls;
    public float jumpForce = 200;
    public float speed = 0.1f;
    public bool infiniteJumps = false;
    InputAction jump;
    private XROrigin xROrigin;
    private CharacterController characterController;
    private CharacterControllerDriver driver;

    float velocity = 0;
    bool canJump = false;
    Vector3 v;

    // Start is called before the first frame update
    void Start()
    {
        xROrigin = GetComponent<XROrigin>();
        characterController = GetComponent<CharacterController>();
        driver = GetComponent<CharacterControllerDriver>();
        Rigidbody body = characterController.GetComponent<Rigidbody>();
        var gameplayActionMap = controls.FindActionMap("XRI RightHand");
        jump = gameplayActionMap.FindAction("B");

        jump.performed += OnJump;
        jump.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCharacterController();

        v = Vector3.up * jumpForce * velocity * Time.deltaTime;
        characterController.Move(v);

        if (characterController.isGrounded) {
            velocity = 0;
            canJump = true;
        }
        
        velocity = Math.Max(-2, velocity - (speed * Time.deltaTime));
    }

    protected virtual void UpdateCharacterController()
        {
            if (xROrigin == null || characterController == null)
                return;
            
            var height = Mathf.Clamp(xROrigin.CameraInOriginSpaceHeight, driver.minHeight, driver.maxHeight);

            Vector3 center = xROrigin.CameraInOriginSpacePos;
            center.y = height / 2f + characterController.skinWidth;

            characterController.height = height;
            characterController.center = center;
        }

    void OnJump(InputAction.CallbackContext context) {
        if (canJump){
            velocity = 1;
            canJump = false;
        }
        if (infiniteJumps) {
            velocity = 1;
        }
    }
}
