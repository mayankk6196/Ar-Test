using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutantManager : MonoBehaviour {

	Animator anim;
	public GameObject target;

	public GameObject currTarget;

	public bool move = true;
	public float speed = 1;

	public int health = 20;

	bool dead = false;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("home");
		anim = GetComponent<Animator> ();
	}

	void Update(){
		if (health > 0) {
			if (target != null && move) {
				gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, target.transform.position, Time.deltaTime * speed);
				gameObject.transform.rotation = Quaternion.LookRotation (target.transform.position - gameObject.transform.position);
				
			}

			if (currTarget == null) {
				if (!move) {
					anim.SetBool ("attack", false);
					move = true;			
				}
			}
		} else {
			if (!dead) {
				anim.SetTrigger ("die");
				dead = true;
			}
		}
	}


	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag.Equals ("wall")) {
			Debug.Log ("C");
			currTarget = col.gameObject;
			anim.SetBool ("attack", true);
			move = false;


		}

		if (col.gameObject.tag.Equals ("home")) {
			Debug.Log ("D");
			currTarget = col.gameObject;
			anim.SetBool ("attack", true);
			move = false;
		}
	}



	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag.Equals ("axe")) {
			health -= 20;
		}
	}

}
