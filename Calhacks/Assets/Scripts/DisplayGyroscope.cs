using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayGyroscope : MonoBehaviour
{
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion quat = GyroToUnity(Input.gyro.attitude);
        //Vector3 rot = quat.eulerAngles;
        //text.text = rot.x.ToString() + " " + rot.y.ToString() + " " + rot.z.ToString();
    }

    public void UpdateText(string newText)
    {
        text.text = newText;
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
