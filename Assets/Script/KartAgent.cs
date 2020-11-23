using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class KartAgent : Agent
{
    private KartController _kartController;

    [Header("Collider With CheckpointManager attached")]
    public CheckpointManager checkpointManager;
    
    public override void Initialize()
    {
        _kartController = GetComponent<KartController>();
    }
    
    public override void OnEpisodeBegin()
    {
        checkpointManager.ResetCheckpoints();
        _kartController.Respawn();
        Debug.Log("Kart respawned & episode restarted");
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 distanceToNextCheckpoint = checkpointManager.nextCheckPointToReach.transform.position - transform.position;
        sensor.AddObservation(distanceToNextCheckpoint/20f);
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        var input = actions.ContinuousActions;
        
        _kartController.Steer(input[0]);
        _kartController.Throttle(Mathf.FloorToInt(input[1]));
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.ContinuousActions;
        
        actions[0] = Input.GetAxis("Horizontal");
        actions[1] = Input.GetAxis("Vertical");
    }
}
