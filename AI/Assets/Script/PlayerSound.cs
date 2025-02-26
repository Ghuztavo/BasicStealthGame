using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] private SphereCollider soundCollider;
    private float soundRadius = 0.0f;
    [SerializeField] private float notMoving;
    [SerializeField] private float movingCrouch;
    [SerializeField] private float movingWalk;
    [SerializeField] private float movingRun;
    [SerializeField] private float jumping;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (soundCollider != null)
        {
            if (player.isMoving)
            {
                if (player.isRunning)
                {
                    soundRadius = movingRun;
                }
                else if (player.isCrouching)
                {
                    soundRadius = movingCrouch;
                }
                else
                {
                    soundRadius = movingWalk;
                }
            }
            else
            {
                soundRadius = notMoving;
            }
        }

        soundCollider.radius = soundRadius;
    }
}
