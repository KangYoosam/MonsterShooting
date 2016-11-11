using UnityEngine;
using System.Collections;

public class ShotGunScript : MonoBehaviour {

	Animator animator;

	public GameObject explosion;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}

	public void Fire(GameObject monster) {
		animator.SetTrigger ("Gazed");
		Destroy(monster);

		Instantiate (explosion, monster.transform.position, Quaternion.identity);

		ScoreController scoreController = GameObject.Find ("Gun").GetComponent<ScoreController> ();
		scoreController.ScorePlus (monster.GetComponent<MonsterScript> ().score);
	}
}
