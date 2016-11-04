using UnityEngine;
using System.Collections;

public class ShotGun : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			print ("fire!");
			animator.SetTrigger ("Gazed");
		}
	}
}
