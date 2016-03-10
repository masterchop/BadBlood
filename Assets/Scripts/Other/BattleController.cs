﻿using UnityEngine;
using System.Collections;

public class BattleController : MonoBehaviour {

	public int roundTime = 100;
	private float lastTimeUpdate = 0;
	private bool battleStarted = false;
	private bool battleEnded;

	public Fighter player1;
	public Fighter player2;
	public BannerController banner;
	public AudioSource musicPlayer;
	public AudioClip backgroundMusic;
	
	void Start () {
		banner.showFight ();
	}

	private void expireTime(){
		if (player1.healthPercent > player2.healthPercent) {
			player2.health = 0;
		} else {
			player1.health = 0;
		}
	}

	void Update () {

		if (!battleStarted && !banner.isAnimating) {
			battleStarted = true;

			player1.enable = true;
			player2.enable = true;

			//GameUtils.playSound(backgroundMusic, musicPlayer);
		}

		if (battleStarted && !battleEnded) {
			if (roundTime > 0 && Time.time - lastTimeUpdate > 1) {
				roundTime--;
				lastTimeUpdate = Time.time;
				if (roundTime == 0){
					expireTime();
				}
			}

			if (player1.healthPercent <= 0) {
				banner.showYouLose ();
				battleEnded = true;

			} else if (player2.healthPercent <= 0) {
				banner.showYouWin ();
				battleEnded = true;
			}
		}

		//kepping a minimum distance i.e 0.4
		/*float distance = Mathf.Abs(player1.transform.position.x - player2.transform.position.x);
		Debug.Log (distance);	
		if (distance < 0.4f) {
			player1.transform.position = (player1.transform.position - player2.transform.position).normalized
				* 0.4f + new Vector3(player2.transform.position.x,0,0);
		}*/

	}
}
