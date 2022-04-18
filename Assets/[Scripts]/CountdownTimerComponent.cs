using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.AI;

public class CountdownTimerComponent : MonoBehaviour
{
    public GameObject bystander;
    public TextMeshProUGUI timerText;
    public float timeLeft;

    public Animator animator;
    public BoxCollider healingTriggerBox;
    public NavMeshAgent navMeshAgent;

    GameUIController gameUIController;

    MovementComponent movementComponent;
   
    public bool isCured = false;

    private void Start()
    {
        gameUIController = GameObject.Find("UIController").GetComponent<GameUIController>();
        gameUIController.cureButton.SetActive(false);
        movementComponent = GameObject.Find("Player").GetComponent<MovementComponent>();

    }

    // Update is called once per frame
    void Update()
    {
        BystanderStatus();
    }

    IEnumerator DelayForAgony()
    {
        yield return new WaitForSeconds(11.25f);
        navMeshAgent.speed = 1;
        navMeshAgent.angularSpeed = 120;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            movementComponent.aimSensitivity = 0;
            gameUIController.cureButton.SetActive(true);
            Debug.Log("Player hit trigger");
            other.gameObject.GetComponent<PlayerController>().characterOverlap = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            movementComponent.aimSensitivity = 4;
            gameUIController.cureButton.SetActive(false);
            other.gameObject.GetComponent<PlayerController>().characterOverlap = null;
        }
    }

    private void BystanderStatus()
    {

        if (!isCured)
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
                //CureComponent.isZombie = true;
                animator.SetBool("isBecomeZombie", true);
                this.healingTriggerBox.enabled = false;
                StartCoroutine(DelayForAgony());
                Destroy(timerText);
                gameUIController.healthyPatientsRemaining++;
                gameUIController.healthyText.text = gameUIController.healthyPatientsRemaining.ToString();
            }
        }

    }

}