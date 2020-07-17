using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPositioner : MonoBehaviour
{
    public float y;
    public float dist = 1.0f;
    public Transform eye;
    public Transform forward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(forward.position.x, y, forward.position.z);
        transform.rotation = Quaternion.LookRotation(forward.position- transform.position);
        transform.Rotate(Vector3.left, 90);
    }
}
