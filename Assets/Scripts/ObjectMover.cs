using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float Speed;
    
    private void Update()
    {
        transform.position += Vector3.back * Speed * Time.deltaTime;        
    }

    public void SetSpeed(float moveSpeed)
    {
        Speed = moveSpeed;
    }
}
