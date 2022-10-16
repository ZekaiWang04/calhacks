using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int health;
    public GameObject[] p1Objects;



    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDamage()
    {
        health--;
        p1Objects[health].SetActive(false);

        return health == 0;
    }

    public void ResetHealth()
    {
        health = 3;
        foreach (GameObject obj in p1Objects)
        {
            obj.SetActive(true);
        }
    }
}
