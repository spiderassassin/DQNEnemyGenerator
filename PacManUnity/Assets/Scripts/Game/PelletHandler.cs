using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletHandler : MonoBehaviour
{
    public GameObject protoPellet, protoPowerPellet;

    private Dictionary<string, Pellet> locationToPellets;

    private bool justEatenPowerPellet;
    public bool JustEatenPowerPellet { get { return justEatenPowerPellet; } }
    private float justEatenPowerPelletTimer;

    private int numPellets;
    public int NumPellets { get { return numPellets; } }

    [SerializeField] private ScoreHandler scoreHandler;

    void Start()
    {

    }

    void Update()
    {
        if (justEatenPowerPelletTimer > 0)
        {
            justEatenPowerPelletTimer -= Time.deltaTime;
            if (justEatenPowerPelletTimer <= 0)
            {
                justEatenPowerPellet = false;
            }
        }
        
    }

    public void AddPellet(Vector3 location, bool powerPellet = false)
    {
        if (locationToPellets == null)
        {
            locationToPellets = new Dictionary<string, Pellet>();
        }

        if (!locationToPellets.ContainsKey(location.ToString()))
        {
            GameObject newPelletObj = null;

            if (powerPellet)
            {
                newPelletObj = GameObject.Instantiate(protoPowerPellet);
            }
            else {
                newPelletObj = GameObject.Instantiate(protoPellet);
            }

            // Assign the required managers to the pellet.
            newPelletObj.GetComponent<Pellet>().pelletHandler = this;
            newPelletObj.GetComponent<Pellet>().scoreHandler = scoreHandler;
            
            // Position ghost locally within the environment that instantiated it.
            newPelletObj.transform.parent = this.transform.parent;
            newPelletObj.transform.localPosition = location;
            locationToPellets.Add(location.ToString(), newPelletObj.GetComponent<Pellet>());
            numPellets++;
        }
    }

    public void RemovePellet(Vector3 location)
    {
        if (locationToPellets.ContainsKey(location.ToString()))
        {
            if (locationToPellets[location.ToString()].powerPellet)
            {
                justEatenPowerPellet = true;
                justEatenPowerPelletTimer = 0.1f;
            }

            locationToPellets.Remove(location.ToString());
            numPellets--;
        }
    }

    public Pellet GetClosestPellet(Vector3 location)
    {
        float minDist = 1000;
        Pellet closest = null;
        foreach (KeyValuePair<string, Pellet> kvp in locationToPellets)
        {
            float dist = (kvp.Value.transform.localPosition - location).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                closest = kvp.Value;
            }
        }
        return closest;
    }

    public Vector3[] GetPelletPositions()
    {
        Vector3[] pellets = new Vector3[locationToPellets.Count];
        foreach (KeyValuePair<string, Pellet> kvp in locationToPellets)
        {
            pellets[pellets.Length-1] = kvp.Value.transform.localPosition;
        }

        return pellets;
    }
}
