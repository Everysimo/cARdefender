using UnityEngine;

public class MoveObjectWithVelocity : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 velocity;
    

    // Update is called once per frame
   

    private void FixedUpdate()
    {
        Vector3 movement = velocity * Time.deltaTime;
        transform.position += movement;
    }
}
