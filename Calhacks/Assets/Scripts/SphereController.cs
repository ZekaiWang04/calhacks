using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float gravity;

    public GameObject vectorObject;
    public GameObject testObject;
    public DisplayGyroscope displayGyroscope;

    private Rigidbody rb;
    private Camera cam;
    public Quaternion zeroQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.current;
        ResetZero();
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion quat = Quaternion.Euler(-90, 0, 0) * GyroToUnity(Input.gyro.attitude) * Quaternion.Euler(90, 0, 0);
        //Quaternion quat = GyroToUnity(Input.gyro.attitude) * zeroQuaternion;
        //transform.localRotation = quat;
        //displayGyroscope.UpdateText(quat.eulerAngles.ToString() + Camera.current.transform.rotation.eulerAngles.ToString());
        //rb.AddForce(quat * zeroQuaternion * (gravity * Vector3.down), ForceMode.Force);

        //vectorObject.transform.rotation = quat * zeroQuaternion;
        //vectorObject.transform.position = transform.position;

        //displayGyroscope.UpdateText((quat * zeroQuaternion).eulerAngles.ToString());
        //testObject.transform.position = transform.position + quat * zeroQuaternion * (0.1f * Vector3.down);
        //testObject.transform.position = transform.position + 0.1f * Camera.current.transform.forward;
        Vector3 dir = zeroQuaternion * cam.transform.forward;
        //Vector3 dir = testObject.transform.InverseTransformDirection(cam.transform.forward);
        //rb.AddForce(gravity * new Vector3(-cam.transform.forward.x, cam.transform.forward.y, -cam.transform.forward.z), ForceMode.Force);
        rb.AddForce(gravity * new Vector3(-dir.x, dir.y, -dir.z), ForceMode.Force);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    Quaternion quat = GyroToUnity(Input.gyro.attitude);
    //    Vector3 delta_angle = Quaternion.Inverse(quat).eulerAngles - zeroQuaternion.eulerAngles;
    //    Vector3 force = new Vector3(Mathf.Sin(delta_angle[0]), -1.0f, Mathf.Sin(delta_angle[1]));
    //    rb.velocity += force * Time.deltaTime;

    //}

    public void ResetZero()
    {
        //zeroQuaternion = Quaternion.Inverse(GyroToUnity(Input.gyro.attitude));
        zeroQuaternion.SetFromToRotation(cam.transform.forward, Vector3.down);
        //zeroQuaternion = Quaternion.identity;
        //testObject.transform.rotation = cam.transform.rotation;
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
