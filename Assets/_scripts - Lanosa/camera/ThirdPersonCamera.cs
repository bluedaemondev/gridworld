using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float cameraDistance;
    [SerializeField]
    private float angle;
    [SerializeField]
    private float cameraHeight;
    [SerializeField]
    private float targetHeightOffset;
    [SerializeField]
    private string rotationAxis = "Mouse X";
    [SerializeField] private string rotationAxisVert = "Mouse Y";

    [SerializeField] private float angleClampVert = 30;
    [SerializeField]
    private float rotationSpeed = 90;

    private void LateUpdate()
    {
        angle += Input.GetAxis(rotationAxis) * rotationSpeed * Time.deltaTime;

        transform.position = target.position +
                             Vector3.right * Mathf.Cos(angle * Mathf.Deg2Rad) * cameraDistance +
                             Vector3.forward * Mathf.Sin(angle * Mathf.Deg2Rad) * cameraDistance +
                             Vector3.up * cameraHeight;

        transform.LookAt(target.position + Vector3.up * targetHeightOffset);
    }
}
