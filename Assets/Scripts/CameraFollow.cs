using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private Transform target;

    private Vector3 basePosition;

    private void Awake()
    {
        basePosition = transform.position;
    }

    private void Update()
    {
        LerpToTarget();
    }

    public void MoveToTarget()
    {
        transform.position = TargetPosition();
    }

    private void LerpToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition(), followSpeed * Time.deltaTime);
    }

    private Vector3 TargetPosition()
    {
        return new Vector3(target.position.x, target.position.y, basePosition.z);
    }
}
