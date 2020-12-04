using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValuesAsteroid;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public PlayerMovement movement;
	public Dead die;
	public bool dead;
	public float z;
	public int toSpawn;
	private Quaternion spawnRotation;
	private struct coord { public float x1; public float x2;}
	coord[] values = new coord[26]; 
	private int place = 0;
	public float coordX;
	public bool enemyDead = false;
	private float x;
	private Vector3 spawnPosition;

	private void Start ()
	{
		StartCoroutine (SpawnWaves ());

	}

	private void Update()
	{
		dead = die.dead;
		if (enemyDead == true) {
			RemovePos (coordX);
			enemyDead = false;
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (die.dead == false)
		{
			z = movement.player.position.z + 220;
			Debug.Log (z);
			toSpawn = Random.Range (0, hazards.Length);
			GameObject hazard = hazards[toSpawn];
			x = Random.Range (-spawnValuesAsteroid.x, spawnValuesAsteroid.x);
			if ((VerifPos (x) == 1 && toSpawn == 1) || toSpawn == 0) {
				spawnPosition = new Vector3 (x, spawnValuesAsteroid.y, z);
				if (VerifPos (x) == 1 && toSpawn == 1)
					AddPosition (x);
			}
			if (toSpawn == 1) {
				spawnRotation = Quaternion.Euler (0, 180, 0);
				waveWait = 3f;
			}
			else{
			 spawnRotation = Quaternion.identity;
				waveWait = 0.5f;}
			Instantiate (hazard, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (waveWait);
		}
	}

	public void AddPosition(float x)
	{
		place++;
		values[place].x1 = x - 20.0f;
		values [place].x2 = x + 20.0f;
	}

	public int VerifPos(float x)
	{
		for (int i = 1; i <= place; i++) {
			if (x >= values [i].x1 && x <= values [place].x2) {
				return 0;
				break;
			}
		}
		return 1;
	}

	public void RemovePos(float x)
	{
		for (int i = 1; i < place; i++) {
			if (x <= values [i].x1 && x >= values [place].x2)
				for (int j = i; j < place; j++) {
					values [i].x1 = values [i + 1].x1;
					values [i].x2 = values [i + 1].x2;
				}
			}
		place--;
	}
}