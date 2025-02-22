using UnityEngine;

public class CameraFollower : Follower
{
    private void FixedUpdate()
    {
        Move();
    }
}
