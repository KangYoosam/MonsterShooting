using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {

	public GameObject target;

	public int score;

	Animator animator;
	AnimatorStateInfo animInfo;

	NavMeshAgent agent;

	GameObject gun;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = 1;
		agent.SetDestination (target.transform.position);

		animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter (Collider coll) {
		if (coll.tag == "Player") {
			agent.Stop ();
			animator.Play ("attack");

			for (int i = 0; i < Time.deltaTime; i++) {
				// 現在動いているAnimatorがattackだったら
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("attackEnd")) {
					gun = GameObject.Find ("Gun");
					Destroy (gun);
				};	
			}
		}
	}

	void PlayerDamaged () {
		print ("kang");
	}
}
