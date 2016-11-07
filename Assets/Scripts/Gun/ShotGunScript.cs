using UnityEngine;
using System.Collections;

public class ShotGunScript : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire(GameObject monster) {
		animator.SetTrigger ("Gazed");
		Destroy(monster);

		ScoreController scoreController = GameObject.Find ("Gun").GetComponent<ScoreController> ();
		scoreController.ScorePlus (monster.GetComponent<MonsterScript> ().score);
	}

	public void GameOver() {
		
		Destroy (this.gameObject);
	}
}
