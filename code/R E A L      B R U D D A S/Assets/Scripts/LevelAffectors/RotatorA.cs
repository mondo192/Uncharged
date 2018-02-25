using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorA : MonoBehaviour
{
    public float speed = 20f;

    private void Awake()
    {
        
    }
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
