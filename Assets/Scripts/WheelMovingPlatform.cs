using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovingPlatform : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed;
    void Update()
    {
        transform.RotateAround(this.transform.parent.position, Vector3.back, rotationSpeed);
        transform.up = Vector3.up;
    }
}
