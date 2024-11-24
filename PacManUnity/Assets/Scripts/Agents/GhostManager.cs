using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance;

    public GameObject[] ghosts;//The ghosts in the order that they will appear
    private int ghostIndex = 0;

    private List<FSMAgent> ghostsInPlay;
    public FSMAgent[] GhostsInPlay { get { return ghostsInPlay.ToArray(); } }
    private List<Vector3> originalGhostPositions;

    private float ghostSpawner;
    public float ghostSpawnerMax = 3;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        ghostsInPlay = new List<FSMAgent>();
        originalGhostPositions = new List<Vector3>();
        ghostSpawner = ghostSpawnerMax/3f;
    }

    void Update()
    {
        if (ghostIndex < ghosts.Length)
        {
            InstantiateNextGhost();
        }
    }

    private void InstantiateNextGhost()
    {
        ghostSpawner = ghostSpawnerMax;
        GameObject ghost1 = GameObject.Instantiate(ghosts[ghostIndex]);
        ghostIndex += 1;

        ghostsInPlay.Add(ghost1.GetComponent<FSMAgent>());
        originalGhostPositions.Add(ghost1.GetComponent<FSMAgent>().GetPosition());
    }

    public FSMAgent GetClosestGhost(Vector3 position)
    {
        float minDist = 1000;
        FSMAgent closest = null;
        foreach (FSMAgent ghost in ghostsInPlay)
        {
            float dist = (ghost.GetPosition() - position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                closest = ghost;
            }
        }
        return closest;
    }

    public void ResetGhosts()
    {
        for (int i = 0; i < ghostsInPlay.Count; i++)
        {
            ghostsInPlay[i].SetPosition(originalGhostPositions[i]);
            // Reset FSMAgent.
            ghostsInPlay[i].Initialize();
        }
    }
}
