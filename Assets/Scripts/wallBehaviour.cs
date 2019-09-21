using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallBehaviour : MonoBehaviour {

	public float health = 20;

	public ParticleSystem explosion;
	bool explodeOnce = false;
	public GameObject attacker; 


	public GameObject parentWall;


	// Update is called once per frame
	void Update () {

		if (health <= 0) {
			if (!explodeOnce) {
				explode ();
				explodeOnce = true;
				GetComponent<MeshRenderer> ().enabled = false;
				GetComponent<Collider> ().enabled = false;
				health = 0;

			}
		}
	}

	void explode(){
		if (!gameObject.tag.Equals ("Respawn")) {
			GameObject.Instantiate (explosion, gameObject.transform.position + Vector3.up, Quaternion.LookRotation (Vector3.up));
		}
		if (attacker != null) {
			attacker.GetComponent<mutantManager> ().currTarget = null;
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.tag.Equals ("hand")) {
			Debug.Log ("*******************************************");
			health -= 10;
		}

		if (col.tag.Equals ("Respawn") && !gameObject.tag.Equals("wall")) {
			parentWall.GetComponent<MeshRenderer> ().enabled = true;
			parentWall.GetComponent<Collider> ().enabled = true;
		}

	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.tag.Equals ("wall")) {
			Debug.Log ("here###########");

			col.gameObject.GetComponent<MeshRenderer> ().enabled = true;
			col.gameObject.GetComponent<Collider> ().enabled = true;
			col.gameObject.GetComponent<wallBehaviour> ().health = 20;
		}

		if (col.gameObject.tag.Equals ("mutant")) {
			attacker = col.gameObject;
		}
	}


}
