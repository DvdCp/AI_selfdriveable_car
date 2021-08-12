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
    public float gravity = 9f;

    private void FixedUpdate()
    {
        //Follow Collider & Stabilization (applying offset for visual effect)
        transform.position = rectangle.transform.position + new Vector3(0f, -1.15f, 0f);
        
        //Acceleration
        rectangle.AddForce(kartModel.transform.forward * currentSpeed, ForceMode.Acceleration);

        //Gravity
        rectangle.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        //Steering
       transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);
       
    }
    
    public void Steer(float input)
    {
        int direction = input > 0 ? 1 : -1;  // Dx or Sx ?
        float amount = Mathf.Abs((input));
        rotate = (steering * direction) * amount;
    }

    public void Throttle(float input) // Method for AI actions
    {
        speed = acceleration * input;
        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); 
        speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); 
        rotate = 0f;
    }

    public void Respawn()
    {
        rectangle.transform.position = respawnPoint.position;
    }
    
}
