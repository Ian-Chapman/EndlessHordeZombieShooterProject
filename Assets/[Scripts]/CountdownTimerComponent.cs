using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class CountdownTimerComponent : MonoBehaviour
{
    public GameObject bystander;
    public TextMeshProUGUI timerText;
    public float timeLeft;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "" + Math.Round(timeLeft, 2);
        if (timeLeft <= 0)
        {
            //Destroy(bystander);
            animator.SetBool("isBecomeZombie", true);
        }
    }
}