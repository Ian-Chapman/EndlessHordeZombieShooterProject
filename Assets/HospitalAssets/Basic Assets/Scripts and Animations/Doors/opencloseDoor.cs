﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class opencloseDoor : MonoBehaviour
{

	public Animator openandclose;
	public bool open;
	public Transform Player;

	void Start()
	{
		open = false;
	}

    private void Update()
    {
		ActivateDoor();
    }

    void ActivateDoor()
	{
		if (Player)
		{
			float dist = Vector3.Distance(Player.position, transform.position);
			if (dist < 2)
			{
				if (open == false)
				{
					StartCoroutine(opening());
				}
			}

			else 
			{
				if (open == true)
				{
					StartCoroutine(closing());
				}
			}
		}
	}

	IEnumerator opening()
	{
		print("you are opening the door");
		openandclose.Play("Opening");
		open = true;
		yield return new WaitForSeconds(.5f);
	}

	IEnumerator closing()
	{
		print("you are closing the door");
		openandclose.Play("Closing");
		open = false;
		yield return new WaitForSeconds(.5f);
	}


}
