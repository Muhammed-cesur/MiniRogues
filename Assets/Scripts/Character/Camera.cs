using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

     public Transform CameraTarget;
    public Vector3 Offset;
    public float Smothtime = 0.03f;
    private Vector3 Velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraTarget != null) 
        {
        Vector3 targetPostion = CameraTarget.position+Offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPostion, ref Velocity, Smothtime);
        }
    }
}
