using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform obiectUrmarit;
    public float smooth = 0.125f;

    public Vector3 offset;
    void LateUpdate()
    {
        this.transform.position = obiectUrmarit.position + offset;
    }
}
