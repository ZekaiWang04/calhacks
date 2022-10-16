using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceSphere : MonoBehaviour
{
    public GameObject sphere;
    private SphereController sphereController;
    public LayerMask layerMask;

    private Camera arCamera;
    private ARMeshManager arMeshManager;

    // Start is called before the first frame update
    void Start()
    {
        arCamera = GetComponentInChildren<Camera>();
        arMeshManager = GetComponentInChildren<ARMeshManager>();

        sphereController = sphere.GetComponent<SphereController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                var hasHit = Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, layerMask);

                if (hasHit)
                {
                    //Instantiate(sphere, hit.point, Quaternion.identity);
                    sphere.transform.position = hit.point + new Vector3(0, 0.25f, 0);
                    sphere.SetActive(true);
                    sphereController.ResetZero();
                }
            }
        }
    }
}
