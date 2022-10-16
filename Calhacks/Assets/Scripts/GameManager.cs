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

    public GameObject ballUI;
    public GameObject placeUI;

    public TextMeshProUGUI playerText;

    public SphereController sphere;
    private bool first;

    // Start is called before the first frame update
    void Start()
    {
        Player = 1;
        placeObject = GetComponentInChildren<PlaceObject>();

        sphere.spawn = placeObject.Activate(spawn, false).transform;
        first = true;
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
            placeObject.gameObject.SetActive(false);
            ballUI.SetActive(true);
            placeUI.SetActive(false);
            SwitchPlayer();
            sphere.Spawn();
        }
    }

    public void SwitchPlayer()
    {
        Player = 3 - Player;

        playerText.text = "Player " + Player.ToString();
    }
}
