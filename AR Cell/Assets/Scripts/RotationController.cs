using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public Vector3 rotationVector;

    private void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime);
    }
}
