using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public bool powerPellet = false;

    public PelletHandler pelletHandler;
    public ScoreHandler scoreHandler;

    public void Eat() {
        pelletHandler.RemovePellet(transform.localPosition);
        scoreHandler.UpdateScore();
        Destroy(gameObject);
    }

}
