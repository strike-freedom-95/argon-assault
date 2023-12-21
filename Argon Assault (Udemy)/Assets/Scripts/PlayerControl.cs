using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // [SerializeField] InputAction movement;

    [Header("General Settings")]
    [Tooltip("To control the speed of the ship")]
    [SerializeField] float moveSpeed = 1.0f;
    [Tooltip("To prevent the ship from going out of camera view in X-axis")]
    [SerializeField] float xRange = 5f;
    [Tooltip("To prevent the ship from going out of camera view in Y-axis")]
    [SerializeField] float yRange = 5f;
    [SerializeField] float yOffset = 4f;

    [Header("PYR Configurations")]
    // [Tooltip("To configure pitch value")]
    // [SerializeField] float pitch = 1f;
    // [Tooltip("To configure yaw value")]
    // [SerializeField] float yaw = 1f;
    // [Tooltip("To configure roll value")]
    // [SerializeField] float roll = 1f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    [Header("Laser Configurations")]
    [Tooltip("To Add/Remove Lasers of Player Ship")]
    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;

    private void OnEnable()
    {
        // movement.Enable();
    }

    private void OnDisable()
    {
        // movement.Disable();   
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        processUserInput();
    }

    private void processUserInput()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    public void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            // laser.SetActive(true);
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = isActive;
        }
    }

    private void ProcessTranslation()
    {
        // float horizontalThrow = Input.GetAxis("Horizontal");
        // float verticalThrow = Input.GetAxis("Vertical");
        // xThrow = movement.ReadValue<Vector2>().x;
        // yThrow = movement.ReadValue<Vector2>().y;

        xThrow = Input.GetAxis("Horizontal");
        yThrow = -Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * moveSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * moveSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        // float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange + yOffset);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, 16f);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float rollDueToControlThrow = xThrow * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = rollDueToControlThrow;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
