using UnityEngine;
using System.Collections;

public class NavMeshManager : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
		NavMeshAgent agent = GetComponent<NavMeshAgent> ();
		agent.speed = 1;
		agent.destination = target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
