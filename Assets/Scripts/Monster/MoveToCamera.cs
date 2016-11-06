using UnityEngine;
using System.Collections;

public class MoveToCamera : MonoBehaviour {

	public GameObject target;

	bool isActive = false;

	// Use this for initialization
	void Start () {
		NavMeshAgent agent = GetComponent<NavMeshAgent> ();
		agent.speed = 1;
		agent.SetDestination (target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		//
	}
}
