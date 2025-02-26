using UnityEngine;

public class AiHearing : MonoBehaviour
{
    public Monster4 M4Agent;
    public Transform ears;
    public Transform objective;

    public float investigationTime = 2.0f;
    public float investigationTimer = 0.0f;
    private bool hearing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        investigationTimer = investigationTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (hearing)
        {
            investigationTimer -= Time.deltaTime;

            if (investigationTimer <= 0.0f)
            {
                if (objective != null)
                {
                    M4Agent.HostileSpotted(objective);
                }
                else
                {
                    M4Agent.StartInvestigate();
                }
            }
            else
            {
                M4Agent.StartInvestigate();
            }
        }
        else
        {
            M4Agent.EndInvestigate();
            investigationTimer = investigationTime;
        }
    }

    private void FixedUpdate()
    {
        hearing = false;
    }

    public void OnTriggerStay(Collider other)
    {
        //First check: Did I get you in the hearing sphere?
        if (other.tag == "PlayerSound")
        {
            objective = other.transform;
            hearing = true;
            M4Agent.SetHearingPlayer(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerSound")
        {
            hearing = false;
            M4Agent.SetHearingPlayer(false);
        }
    }
}

