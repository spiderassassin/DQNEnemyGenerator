using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampState : State
{
    //Set name of this state
    public CampState():base("CampState"){ }

    public override State Update(FSMAgent agent)
    {
        //Handle Following Pacman
        Vector3 pacmanLocation = pacmanInfo.transform.localPosition;
        if (agent.CloseEnough(pacmanLocation))
        {
            scoreHandler.KillPacman();
        }

        //If timer complete, go to Scatter State
        if (agent.TimerComplete())
        {
            return new ScatterState(new Vector3(obstacleHandler.XBound, obstacleHandler.YBound), this);
        }

        //If Pacman ate a power pellet, go to Frightened State
        if (pelletHandler.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }

        //This has gone too far
        if(scoreHandler.Score>120){
           return new JustAheadTargetState(); 
        }

        Pellet p = pelletHandler.GetClosestPellet(pacmanLocation);

        if(p.powerPellet){
           agent.SetTarget(new Vector3(obstacleHandler.XBound, obstacleHandler.YBound)); 
        }
        else{
            agent.SetTarget(p.transform.localPosition); 
        }

        //Stay in this state
        return this;
    }

    //Upon entering state, set timer to enter Scatter State
    public override void EnterState(FSMAgent agent)
    {
        agent.SetTimer(20f);
        agent.SetSpeedModifierDouble();
    }

    public override void ExitState(FSMAgent agent){ 
        agent.SetSpeedModifierNormal();
    }
}
