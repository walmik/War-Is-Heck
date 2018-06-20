﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int health;
	public SpriteRenderer healthBar;
	float damageTaken = 0f;
	Animator anim;
	TroopClass tc; 

	void Start() {
		tc = gameObject.GetComponent<TroopClass>();
	}

	public void TakeDamage(int damage, GameObject attacker=null) {
		if(gameObject.GetComponent<TroopClass> ()==null) 
			Death();
		health -= damage;
		damageTaken += damage;
		gameObject.GetComponent<TroopClass> ().anim.SetTrigger ("GetHit");

		if (healthBar != null) {
			healthBar.color = Color.Lerp(Color.white,Color.red,damageTaken/100);
		}

		if (health <= 0) {
			Death ();
		}

		// On non-player controlled characters, for now they just attack anybody that hits them
		if(!tc.pc) {
			tc.SetCombatTarget(attacker);
		}
	}

	void Death() {
		gameObject.GetComponent<TroopClass> ().UnmarkSelected ();
		ProgUtils.RecursiveDestruction (gameObject);
		Destroy (this);
	}
}
