using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navControl : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    bool isWalking = true;
    private Animator animator;
    public GameObject lookTarget;
    public float speed = 2.5f;
    float rotateSpeed = 1f;

    float mySliderValue;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = speed;
        animator.speed = agent.speed * 0.5f;
    }

    void OnGUI()
    {
        // Adding a slider to change the speed of the animator
        //GUI.Label(new Rect(0, 25, 40, 60), "Speed");

        //mySliderValue = GUI.HorizontalSlider(new Rect(45, 25, 200, 60), mySliderValue, 0.0F, 1.0F);
        //animator.speed = mySliderValue;
        // Changing the agent's speed based on the slider value
        //agent.speed = mySliderValue * 1.5f;
    }

    void Update()
    {
        if (isWalking)
        {
            agent.destination = target.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }
        RotateTowardsTarget();
    }

    void RotateTowardsTarget()
    {
        float stepSize = rotateSpeed * Time.deltaTime;

        Vector3 targetDir = lookTarget.transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            isWalking = false;
            animator.SetTrigger("ATTACK");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Target")
        {
            isWalking = true;
            animator.SetTrigger("WALK");
        }
    }
}
