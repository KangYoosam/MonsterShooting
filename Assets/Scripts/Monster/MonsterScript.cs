using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {

	public GameObject target;

	public int score;

	Animator animator;

	NavMeshAgent agent;

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
		}
	}
}
