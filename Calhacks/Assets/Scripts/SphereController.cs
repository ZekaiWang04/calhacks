using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float gravity;

    private Rigidbody rb;
    public Quaternion zeroQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetZero();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion quat = GyroToUnity(Input.gyro.attitude);
        rb.AddForce(quat * zeroQuaternion * (gravity * Vector3.down), ForceMode.Force);
    }

    public void ResetZero()
    {
        zeroQuaternion = Quaternion.Inverse(GyroToUnity(Input.gyro.attitude));
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
