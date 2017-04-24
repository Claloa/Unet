using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;
	[SyncVar(hook="OnChangeHealth")]
	public int currentHealth = maxHealth;
	public Slider healthSlider;
	public bool destoryOnDeath = false;

	private NetworkStartPosition[] spawnPoints;

	void Start(){
		
		if (isLocalPlayer) {
			spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
		}
	}

	public void TakeDamage(int damage){

		if (isServer == false)
			return;

		currentHealth -= damage;

		if (currentHealth <= 0) {

			if (destoryOnDeath) {
				Destroy (this.gameObject);
				return;
			}
			currentHealth = 0;
			RpcRespawn ();

		}

		healthSlider.value = currentHealth / (float)maxHealth;
	
	}

	void OnChangeHealth(int health){
		healthSlider.value = health / (float)maxHealth;
	}

	[ClientRpc]

	void RpcRespawn(){
		if (isLocalPlayer == false)
			return;
		Vector3 spawnPosition = Vector3.zero;

		spawnPosition = spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position;
		transform.position = spawnPosition;
	}
}
