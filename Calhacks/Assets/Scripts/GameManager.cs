using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int Player;

    public PlaceObject placeObject;
    public GameObject spawn;
    public GameObject goal;
    public GameObject spike;

    public GameObject ballUI;
    public GameObject placeUI;

    public TextMeshProUGUI playerText;

    public int state;

    public SphereController sphere;
    private bool first;

    // Start is called before the first frame update
    void Start()
    {
        Player = 1;
        placeObject = GetComponentInChildren<PlaceObject>();

        sphere.spawn = placeObject.Activate(spawn, false).transform;
        first = true;
        state = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Place()
    {
        if (!placeObject.placed)
            return;

        if (first)
        {
            SwitchPlayer();
            placeObject.Activate(goal, false);
            first = false;
        }
        else
        {
            Next();
        }
    }

    public void Next()
    {
        if (state == 0)
        {
            SwitchPlayer();
            sphere.Spawn();
        }
        else if (state == 1)
        {
            sphere.gameObject.SetActive(false);
            placeObject.Activate(spike, true);
            ballUI.SetActive(false);
            placeUI.SetActive(true);
            SwitchPlayer();
        }
        else if (state == 2)
        {
            placeObject.Activate(spike, true);
            SwitchPlayer();
        }
        else
        {
            placeObject.gameObject.SetActive(false);
            ballUI.SetActive(true);
            placeUI.SetActive(false);
            sphere.Spawn();
        }

        state = (state + 1) % 4;
    }

    public void SwitchPlayer()
    {
        Player = 3 - Player;

        playerText.text = "Player " + Player.ToString();
    }
}
