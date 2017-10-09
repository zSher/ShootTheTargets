using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterAnimationFinishes : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!anim.GetBool("Play"))
        {
            Destroy(this.gameObject);
        }
	}
}
