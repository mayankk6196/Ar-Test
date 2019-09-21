using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    float hz, vz;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        hz = Input.GetAxis("Horizontal");
        vz = Input.GetAxis("Vertical");
        if (hz != 0 || vz != 0)
        {
            anim.SetBool("New Bool", true);
        }
        else
        {
            anim.SetBool("New Bool", false);
        }

        gameObject.transform.Translate(new Vector3(hz, 0, vz)*Time.deltaTime*3);
	}
}
