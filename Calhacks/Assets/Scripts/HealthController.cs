using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int p1Health;
    public int p2Health;
    public GameObject[] p1Objects;
    public GameObject[] p2Objects;

    // Start is called before the first frame update
    void Start()
    {
        p1Health = 3;
        p2Health = 3;
    }

    public bool TakeDamage()
    {
        if (GameManager.Player == 1)
        {
            p1Health--;
            p1Objects[p1Health].SetActive(false);

            return p1Health == 0;
        }
        else
        {
            p2Health--;
            p2Objects[p2Health].SetActive(false);

            return p2Health == 0;
        }
    }
}
