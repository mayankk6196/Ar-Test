using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour {

	public Button bAttack, bBuild;

	float hz, vt;
	public float speed = 2;

	bool temp = false;
	Animator anim;

	bool attack = false;

	public float health = 100;

	bool isDead = false;
	public bool isPushing = false;
	bool wallPresent = false;
	public GameObject wall;
	GameObject tempWall;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		bAttack.onClick.AddListener (mAttack);
		bBuild.onClick.AddListener (mBuild);       
	}

	void mAttack(){
		if (attack) {
			anim.SetTrigger ("kick");
		} else {
			anim.SetTrigger ("slash");
		}
		attack = !attack;
	}

	void mBuild(){
		isPushing = !isPushing;
		if (!isPushing) {
			anim.SetBool ("pushing", false);
			//Destroy (tempWall.gameObject);
			tempWall.transform.parent = null;
			wallPresent = false;
		}
	}

	// Update is called once per frame
	void Update () {

		if (health > 0) {
			hz = Input.GetAxis ("Horizontal");
			vt = Input.GetAxis ("Vertical");

			////////////////////////////////////////
			if (hz != 0 || vt != 0) {
				if (temp == false) {
					Debug.Log ("move");
					anim.SetBool ("moving", true);
					temp = true;
				}
			} else {
				if (temp == true) {
					Debug.Log ("stop");
					anim.SetBool ("moving", false);
					temp = false;
				}
			}
			////////////////////////////////////////


			Vector3 targetDirection = new Vector3 (hz, 0f, vt);
			targetDirection = Camera.main.transform.TransformDirection (targetDirection);
			targetDirection.y = 0.0f;

			gameObject.transform.Translate (targetDirection.normalized * Time.deltaTime * speed, Space.World);

			if (targetDirection != Vector3.zero) {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (targetDirection), Time.deltaTime * 60);
			}
				


 

		} else {
			if (!isDead) {
				anim.SetTrigger ("die");
				isDead = true;
			}
		}
	}


	void OnTriggerEnter(Collider col){
		if (col.tag.Equals ("hand")) {
			anim.SetTrigger ("react");
			health -= 10;   
		}

		if (col.tag.Equals ("wall")) {
			if (isPushing) {
				col.GetComponent<MeshRenderer> ().enabled = true;
				col.GetComponent<Collider> ().enabled = true;
				col.GetComponent<wallBehaviour> ().health = 20;
				Destroy (tempWall);
				anim.SetBool ("pushing", false);
				isPushing = false;
				wallPresent = false;
			}
		}
	}


	void OnTriggerStay(Collider col){

		if (col.tag.Equals ("home")) {
			if (isPushing && !wallPresent) {
				tempWall = GameObject.Instantiate (wall, gameObject.transform.position + gameObject.transform.forward + Vector3.up * 0.65f, Quaternion.LookRotation (gameObject.transform.forward));
				Debug.Log ("built wall");
				tempWall.transform.parent = gameObject.transform;
 				anim.SetBool ("pushing", true);
				wallPresent = true;
			}
		}
	}

}
