using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    public float MaxTimeToReachNextCheckpoint = 30f;
    public float TimeLeft = 30f;
    public KartAgent kartAgent;
    public Checkpoint nextCheckPointToReach;
    public Text timerLabel;
    
    private int CurrentCheckpointIndex;
    private List<Checkpoint> _checkpoints;
    
    void Start()
    {
        _checkpoints = FindObjectOfType<CheckpointCounter>().checkPoints;
        ResetCheckpoints();
    }

    public void ResetCheckpoints()
    {
        CurrentCheckpointIndex = 0;
        TimeLeft = MaxTimeToReachNextCheckpoint;
        
        SetNextCheckpoint();
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;
        timerLabel.text = "Time Left: " + TimeLeft;

        if (TimeLeft < 0f)
        {
            kartAgent.AddReward(-1.0f);
            kartAgent.EndEpisode();
        }
       
    }

    public void CheckPointReached(Checkpoint checkpoint)
    {
        if (nextCheckPointToReach != checkpoint) return;
        
        if (nextCheckPointToReach.gameObject.tag.Equals("Goal"))
        {
            kartAgent.AddReward(2.5f);
            kartAgent.EndEpisode();
        }
        else
        {
            CurrentCheckpointIndex++;
            kartAgent.AddReward(1.0f);
            SetNextCheckpoint();
        }
        
    }

    public void OnWallCollision(Wall wall)
    {
        kartAgent.AddReward(-0.5f);
    }


    private void SetNextCheckpoint()
    {
        
        TimeLeft = MaxTimeToReachNextCheckpoint;
        nextCheckPointToReach = _checkpoints[CurrentCheckpointIndex];
    }
    
}
