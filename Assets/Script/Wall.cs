using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<CheckpointManager>() != null)
        {
            Debug.Log("Hitting the wall !!!");
            other.gameObject.GetComponent<CheckpointManager>().OnWallCollision(this);
        }
    }
}
