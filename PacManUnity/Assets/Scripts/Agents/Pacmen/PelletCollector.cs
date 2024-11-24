using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletCollector : MonoBehaviour
{
    [SerializeField] private PelletHandler pelletHandler;

    // Update is called once per frame
    void Update()
    {
        Pellet p = pelletHandler.GetClosestPellet(transform.localPosition);
        if (p != null)
        {
            float dist = (transform.localPosition - p.transform.localPosition).sqrMagnitude;
            //Extremely slow way to do this, don't do this normally. Just want to avoid collision issues
            if (dist < 0.01f)
            {
                p.Eat();
            }
        }
    }
}
