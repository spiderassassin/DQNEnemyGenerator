using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeState : State
{
    public ClydeState() : base("Clyde") { }

    public override void EnterState(FSMAgent agent)
    {
        agent.SetTimer(20f);
    }

    public override void ExitState(FSMAgent agent)
    {
        base.ExitState(agent);
    }
     
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
            return new ScatterState(new Vector3(-1*obstacleHandler.XBound, -1*obstacleHandler.YBound), this);
        }

        //If Pacman ate a power pellet, go to Frightened State
        if (pelletHandler.JustEatenPowerPellet)
        {
            return new FrightenedState(this);
        }

        float dist = (pacmanLocation-agent.GetPosition()).magnitude;

        if(dist<1.6){
            //If we didn't return follow Pacman
            agent.SetTarget(new Vector3(-1*obstacleHandler.XBound, -1*obstacleHandler.YBound));
        }
        else{

            agent.SetTarget(pacmanLocation);
        }
        

        //Stay in this state
        return this;
    }
}
