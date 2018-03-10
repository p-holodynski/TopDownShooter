using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Wave[] waves;
	public EnemyMovement enemy;

	LivingEntity playerEntity;
	Transform playerT;

	Wave currentWave;
	int currentWaveNumber;

	int enemiesRemainingToSpawn;
	int enemiesRemainingAlive;
	float nextSpawnTime;

	MapGenerator map;

	public bool devMode;


	public event System.Action<int> OnNewWave;

	void Start(){
		playerEntity = FindObjectOfType<PlayerMovement> ();
		playerT = playerEntity.transform;
		map = FindObjectOfType<MapGenerator> ();
		NextWave ();
	}

	public void Update(){
		if ((enemiesRemainingToSpawn > 0 || currentWave.infinite) && Time.time > nextSpawnTime) {
			enemiesRemainingToSpawn--;
			nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

			StartCoroutine (SpawnEnemy ());
		}

		if (devMode) {
			if (Input.GetKeyDown (KeyCode.Return)) {
				StopCoroutine ("SpawnEnemy");
				foreach (EnemyMovement enemy in FindObjectsOfType<EnemyMovement>()) {
					GameObject.Destroy (enemy.gameObject);
				}
				NextWave ();
			}
		}
	}

	IEnumerator SpawnEnemy(){
		float spawnDelay = 1;
		float tileFlashSpeed = 4;

		Transform randomTile = map.GetRandomOpenTile ();
		Material tileMat = randomTile.GetComponent<Renderer> ().material;
		Color initialColor = tileMat.color;
		Color flashColor = Color.red;
		float spawnTimer = 0;

		while (spawnTimer < spawnDelay) {
			tileMat.color = Color.Lerp (initialColor, flashColor, Mathf.PingPong (spawnTimer * tileFlashSpeed, 1));

			spawnTimer += Time.deltaTime;
			yield return null;
		}

		EnemyMovement spawnedEnemy = Instantiate (enemy, randomTile.position + Vector3.up, Quaternion.identity) as EnemyMovement;
		spawnedEnemy.OnDeath += OnEnemyDeath;
		spawnedEnemy.SetCharacteristics (currentWave.moveSpeed, currentWave.hitsToKillPlayer, currentWave.enemyHealth, currentWave.skinColor);
	}

	void OnEnemyDeath(){
		enemiesRemainingAlive--;

		if (enemiesRemainingAlive == 0) {
			NextWave ();
		}
	}

	void ResetPlayerPosition(){
		playerT.position = map.GetTileFromPosition (Vector3.zero).position + Vector3.up;
	}

	void NextWave(){
		currentWaveNumber++;
		//print ("Wave: " + currentWaveNumber);
		if (currentWaveNumber - 1 < waves.Length) {
			
			currentWave = waves [currentWaveNumber - 1];

			enemiesRemainingToSpawn = currentWave.enemyCount;
			enemiesRemainingAlive = enemiesRemainingToSpawn;

			if (OnNewWave != null) {
				OnNewWave (currentWaveNumber);
			}
			ResetPlayerPosition ();
		}
	}

	[System.Serializable]
	public class Wave {
		public bool infinite;
		public int enemyCount;
		public float timeBetweenSpawns;
		public float moveSpeed;
		public int hitsToKillPlayer;
		public float enemyHealth;
		public Color skinColor;
	}

}
