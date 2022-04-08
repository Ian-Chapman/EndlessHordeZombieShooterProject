using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.AI;

public class CountdownTimerComponent : MonoBehaviour
{
    public GameObject bystander;
    public TextMeshProUGUI timerText;
    public float timeLeft;

    public Animator animator;
    public BoxCollider healingTriggerBox;
    public NavMeshAgent navMeshAgent;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = timeLeft.ToString();
        
        if (timeLeft > 0)
        {
            navMeshAgent.speed = 0;
            navMeshAgent.angularSpeed = 0;
        }


        if (timeLeft <= 0)
        {
            //Destroy(bystander);
            animator.SetBool("isBecomeZombie", true);
            healingTriggerBox.enabled = false;
            StartCoroutine(DelayForAgony());
            Destroy(this.timerText);
        }

    }

    IEnumerator DelayForAgony()
    {
        yield return new WaitForSeconds(11.25f);
        navMeshAgent.speed = 1;
        navMeshAgent.angularSpeed = 120;
    }
}