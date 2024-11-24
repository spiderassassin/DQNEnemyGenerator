using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic State Representation
public class State
{
    private string stateName;//The name of this state, set in the constructor

    protected HW3NavigationHandler hw3NavigationHandler;
    protected PacmanInfo pacmanInfo;
    protected PelletHandler pelletHandler;
    protected ScoreHandler scoreHandler;
    protected ObstacleHandler obstacleHandler;

    public string Name { get { return stateName; } }

    public State(string _stateName)
    {
        stateName = _stateName;
    }

    public State(string _stateName, HW3NavigationHandler _hw3NavigationHandler, PacmanInfo _pacmanInfo, PelletHandler _pelletHandler, ScoreHandler _scoreHandler, ObstacleHandler _obstacleHandler)
    {
        stateName = _stateName;
        hw3NavigationHandler = _hw3NavigationHandler;
        pacmanInfo = _pacmanInfo;
        pelletHandler = _pelletHandler;
        scoreHandler = _scoreHandler;
        obstacleHandler = _obstacleHandler;
    }

    //Handle each tick of behavior while in this state
    public virtual State Update(FSMAgent agent)
    {
        return this;
    }

    //Handle logic on entering this state
    public virtual void EnterState(FSMAgent agent) { }

    //Handle logic on exiting this state
    public virtual void ExitState(FSMAgent agent) { }
}
