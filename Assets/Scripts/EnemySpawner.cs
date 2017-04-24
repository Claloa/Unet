using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {
	public GameObject enemyPrefabs;
	public int numbersOfEnemies = 5;

	public override void OnStartServer(){
		
		for (int i = 0; i <= numbersOfEnemies; i++) {
			Vector3 position = new Vector3 (Random.Range (-6f, 6f), 0, Random.Range (-6f, 6f));
			Quaternion rotation = Quaternion.Euler (0, Random.Range (0, 360), 0);

			GameObject enemy = Instantiate (enemyPrefabs, position, rotation) as GameObject;

			NetworkServer.Spawn (enemy);
		
		}
	}

}
