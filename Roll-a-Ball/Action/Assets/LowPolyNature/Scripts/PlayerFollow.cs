using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    // Rotation properties
    public float RotationsSpeed = 5.0f;
    public float CameraPitchMin = 1.5f;
    public float CameraPitchMax = 6.5f;

    // Constants
    private const int leftClick = 0;
    private const int rightClick = 1;
    private const int middleClick = 2;

    private int CameraMouseButton = rightClick;

    // Use this for initialization
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
            return;
        if (Input.GetMouseButton(CameraMouseButton))
        {
            float h = Input.GetAxis("Mouse X") * RotationsSpeed;
            float v = Input.GetAxis("Mouse Y") * RotationsSpeed;

            Quaternion camTurnAngle = Quaternion.AngleAxis(h, Vector3.up);

            Quaternion camTurnAngleY = Quaternion.AngleAxis(-v, transform.right);

            Vector3 newCameraOffset = camTurnAngle * camTurnAngleY * _cameraOffset;

            // Limit camera pitch
            if (newCameraOffset.y < CameraPitchMin || newCameraOffset.y > CameraPitchMax)
            {
                newCameraOffset = camTurnAngle * _cameraOffset;
            }

            _cameraOffset = newCameraOffset;
        }

        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        if (Input.GetMouseButton(CameraMouseButton))
            transform.LookAt(PlayerTransform);
        else
        {
            transform.position = PlayerTransform.TransformPoint(_cameraOffset);
            transform.rotation = PlayerTransform.rotation;
        }
    }
}
