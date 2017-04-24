using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {


	public GameObject  bulletPrefab;
	public Transform bulletSpawn;
	// Update is called once per frame
	void Update () {

		if (isLocalPlayer == false) {
			return;
		}
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		transform.Rotate (Vector3.up * h * 120 * Time.deltaTime);
		transform.Translate (Vector3.forward * v * 3 * Time.deltaTime);

		if (Input.GetKeyDown (KeyCode.Space)) {
			Cmdfire ();
		}

	}

	public override void OnStartLocalPlayer(){
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	[Command]
	void Cmdfire(){
		GameObject bullet = Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 10;
		Destroy (bullet, 2);

		NetworkServer.Spawn (bullet);
	}
}
