using UnityEngine;

public class AiSight : MonoBehaviour
{
    public Monster3 M3Agent;
    public Transform eyes;
    public Transform objective;

    public float investigationTime = 2.0f;
    public float investigationTimer = 0.0f;
    private bool playerInSight = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        investigationTimer = investigationTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInSight)
        {
            investigationTimer -= Time.deltaTime;

            if (investigationTimer <= 0.0f)
            {
                if (objective != null)
                {
                    M3Agent.HostileSpotted(objective);
                }
                else
                {
                    M3Agent.StartInvestigate();
                }
            }
            else
            {
                M3Agent.StartInvestigate();
            }
        }
        else
        {
            M3Agent.EndInvestigate();
            investigationTimer = investigationTime;
        }
    }

    private void FixedUpdate()
    {
        playerInSight = false;
    }

    public void OnTriggerStay(Collider other)
    {
        //First check: Did I get you in sight cone?
        if(other.tag == "Player")
        {
            //second check: is there no obstruction in my sight?
            RaycastHit hit;
            if(Physics.Linecast(eyes.position, other.transform.position, out hit))
            {
                //something obstructing my view
                Debug.DrawLine(transform.position, hit.point, Color.blue, 1f);
                if (hit.transform.tag == "Player")
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red, 1f);
                    objective = hit.transform;
                    playerInSight = true;
                    M3Agent.SetPlayerInSight(true);
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInSight = false;
            M3Agent.SetPlayerInSight(false);
        }
    }
}

