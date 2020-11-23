using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CheckpointManager>() != null)
        {
            Debug.Log(other.gameObject.name+" Arrived!!!");
            Debug.Log(this.gameObject.name+ "REACHED!!!!");
            other.GetComponent<CheckpointManager>().CheckPointReached(this);
        }
    }
}