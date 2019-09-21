using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiator : MonoBehaviour {

	public GameObject mutant;
	public GameObject[] spawnPoints;

	public int limit = 4;
	public static int activeMutants = 0;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("instantiate", 2, 5);
	}

	void instantiate(){
		if (activeMutants < limit) {
			GameObject.Instantiate (mutant, spawnPoints [Random.Range (0, spawnPoints.Length - 1)].transform.position, Quaternion.identity);
			activeMutants++;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
