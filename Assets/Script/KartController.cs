using UnityEngine;

public class KartController : MonoBehaviour
{
    private float speed, currentSpeed;
    private float rotate, currentRotate;
    
    public Transform kartModel;
    public Rigidbody rectangle;
    public Transform respawnPoint;
    
    [Header("Parameters")]
    public float acceleration = 30f;
    public float steering = 50f;
    public float gravity = 10f;

    private void FixedUpdate()
    {
        //Follow Collider & Stabilization
        transform.position = rectangle.transform.position + new Vector3(0f, 0.20f, 0f);
        
        //Forward Acceleration
        rectangle.AddForce(kartModel.transform.forward * currentSpeed, ForceMode.Acceleration);

        //Gravity
        rectangle.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        //Steering
       transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);
       
    }
    
    public void Steer(float dir)
    {
        int direction = dir > 0 ? 1 : -1;  // Dx or Sx ?
        float amount = Mathf.Abs((dir));
        rotate = (steering * direction) * amount;
    }

    public void Throttle(int input) // Method for AI actions
    {
        speed = acceleration * input;
        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;
    }

    public void Respawn()
    {
        rectangle.transform.position = respawnPoint.position;
    }
    
}
