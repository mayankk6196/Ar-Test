using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class builder : MonoBehaviour {

	public float health = 20;

	public ParticleSystem explosion;
	bool explodeOnce = false;
	public GameObject attacker; 

	// Update is called once per frame
	void Update () {

		if (health <= 0) {
			if (!explodeOnce) {
				explode ();
				explodeOnce = true;
				//GetComponent<MeshRenderer> ().enabled = false;
				//GetComponent<Collider> ().enabled = false;
				health = 0;
				Destroy (this.gameObject);
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
	}

	void OnCollisionEnter(Collision col){
		
		if (col.gameObject.tag.Equals ("mutant")) {
			attacker = col.gameObject;
		}
	}


}
