using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class PlaceObject : MonoBehaviour
{
    public LayerMask layerMask;

    public GameObject obj;
    public bool canPlaceVertical;
    public bool placed;

    private Camera arCamera;
    private ARMeshManager arMeshManager;

    void Start()
    {
        arCamera = Camera.main;//GetComponentInChildren<Camera>();
        arMeshManager = GetComponentInChildren<ARMeshManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                var hasHit = Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, layerMask);

                if (hasHit)
                {
                    if (!canPlaceVertical && Vector3.Angle(Vector3.up, hit.normal) > 15)
                    {

                    }
                    else
                    {
                        //GameObject newObj = Instantiate(obj);
                        //Instantiate(sphere, hit.point, Quaternion.identity);
                        obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                        obj.transform.position = hit.point;
                        obj.SetActive(true);
                        placed = true;
                    }
                }
            }
        }

        //if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        //{
        //    Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
        //    var hasHit = Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, layerMask);

        //    if (hasHit)
        //    {
        //        if (!canPlaceVertical && Vector3.Angle(Vector3.up, hit.normal) > 15)
        //        {

        //        }
        //        else
        //        {
        //            //GameObject newObj = Instantiate(obj);
        //            //Instantiate(sphere, hit.point, Quaternion.identity);
        //            obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        //            obj.transform.position = hit.point;
        //            obj.SetActive(true);
        //            placed = true;
        //        }
        //    }
        //}
    }

    public GameObject Activate(GameObject obj, bool canPlaceVertical)
    {
        this.obj = Instantiate(obj);
        this.obj.SetActive(false);
        this.canPlaceVertical = canPlaceVertical;
        gameObject.SetActive(true);
        placed = false;

        return this.obj;
    }
}
