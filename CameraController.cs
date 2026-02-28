using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject fpCamera;
    public GameObject orbitCamera;
    public GameObject playerMesh;
    public Transform playerTransform;

    [Header("Orbit Settings")]
    public float sensitivity = 3f;
    public Vector3 defaultThirdPersonOffset = new Vector3(0.5f, 1.5f, -3f); // "Over the shoulder"

    private bool isFirstPerson = true;
    private float rotationX = 0f;
    private float rotationY = 0f;
    private Vector3 currentOrbitOffset;

    void Start()
    {
        ResetToOverShoulder();
        UpdateCameraState();
    }

    void Update()
    {
        // Toggle view with 'T'
        if (Input.GetKeyDown(KeyCode.T))
        {
            isFirstPerson = !isFirstPerson;
            UpdateCameraState();
        }

        if (!isFirstPerson)
        {
            // Reset to default over-the-shoulder with 'R'
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetToOverShoulder();
            }

            // Orbit logic: Click + Drag
            if (Input.GetMouseButton(0))
            {
                rotationX += Input.GetAxis("Mouse X") * sensitivity;
                rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
                rotationY = Mathf.Clamp(rotationY, -30f, 60f); // Limit vertical tilt
            }
        }
    }

    void LateUpdate()
    {
        if (!isFirstPerson)
        {
            // Apply orbit rotation around player
            Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
            orbitCamera.transform.position = playerTransform.position + (rotation * currentOrbitOffset);
            orbitCamera.transform.LookAt(playerTransform.position + Vector3.up * 1.5f);
        }
    }

    void UpdateCameraState()
    {
        fpCamera.SetActive(isFirstPerson);
        orbitCamera.SetActive(!isFirstPerson);
        // Ensure mesh visibility matches mode
        playerMesh.SetActive(!isFirstPerson);
    }

    void ResetToOverShoulder()
    {
        currentOrbitOffset = defaultThirdPersonOffset;
        rotationX = playerTransform.eulerAngles.y;
        rotationY = 10f; // Slight downward tilt
    }
}