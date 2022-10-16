using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int Player;

    public PlaceObject placeObject;
    public GameObject spawn;
    public GameObject goal;
    public GameObject spikePrefab;
    public GameObject fanPrefab;
    public GameObject bumperPrefab;
    public GameObject activePrefab;

    public GameObject spike;
    public GameObject fan;
    public GameObject bumper;

    public GameObject ballUI;
    public GameObject placeUI;
    public GameObject selectionUI;

    public TextMeshProUGUI playerText;

    public int state;

    public SphereController sphere;
    private bool first;

    // Start is called before the first frame update
    void Start()
    {
        Player = 1;
        placeObject = GetComponentInChildren<PlaceObject>();

        first = true;
        state = 3;

        spike = Instantiate(spikePrefab);
        fan = Instantiate(fanPrefab);
        bumper = Instantiate(bumperPrefab);
        spike.SetActive(false);
        fan.SetActive(false);
        bumper.SetActive(false);
    }

    public void Place()
    {
        if (!placeObject.placed || placeObject.obj == null)
            return;

        if (first)
        {
            SwitchPlayer();
            if (activePrefab == goal)
            {
                selectionUI.transform.GetChild(1).gameObject.SetActive(false);
                placeObject.obj = null;
                ResetAlpha();
            }
            else
            {
                selectionUI.transform.GetChild(0).gameObject.SetActive(false);
                placeObject.obj = null;
                ResetAlpha();
            }
            first = false;
        }
        else
        {
            if (activePrefab == spikePrefab)
            {
                spike = Instantiate(spikePrefab);
                spike.SetActive(false);
                placeObject.obj = null;
                ResetAlpha();
            }
            else if (activePrefab == fanPrefab)
            {
                fan = Instantiate(fanPrefab);
                fan.SetActive(false);
                placeObject.obj = null;
                ResetAlpha();
            }
            else if (activePrefab == bumperPrefab)
            {
                bumper = Instantiate(bumperPrefab);
                bumper.SetActive(false);
                placeObject.obj = null;
                ResetAlpha();
            }
            else
            {
                selectionUI.transform.GetChild(0).gameObject.SetActive(false);
                selectionUI.transform.GetChild(1).gameObject.SetActive(false);
                for (int i = 2; i < 5; i++)
                {
                    selectionUI.transform.GetChild(i).gameObject.SetActive(true);
                }
                placeObject.obj = null;
                ResetAlpha();
            }
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
            ballUI.SetActive(false);
            placeUI.SetActive(true);
            SwitchPlayer();
        }
        else if (state == 2)
        {
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

    public void ResetAlpha()
    {
        foreach (Transform child in selectionUI.transform)
        {
            Color temp2 = child.GetComponent<Image>().color;
            temp2.a = 0.4f;
            child.GetComponent<Image>().color = temp2;
        }
    }

    public void SwitchObstacle(int x)
    {
        ResetAlpha();

        Color temp = selectionUI.transform.GetChild(x).GetComponent<Image>().color;
        temp.a = 0.9f;
        selectionUI.transform.GetChild(x).GetComponent<Image>().color = temp;

        if (x == 0)
        {
            placeObject.Activate(spawn, false);
            activePrefab = spawn;
        }
        else if (x == 1)
        {
            placeObject.Activate(goal, false);
            activePrefab = goal;
        }
        else if (x == 2)
        {
            placeObject.Activate(spike, true);
            activePrefab = spikePrefab;
        }
        else if (x == 3)
        {
            placeObject.Activate(fan, false);
            activePrefab = fanPrefab;
        }
        else
        {
            placeObject.Activate(bumper, false);
            activePrefab = bumperPrefab;
        }
    }
}
