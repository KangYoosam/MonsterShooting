using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayManager : MonoBehaviour {

	// Player
	public GameObject diveCamera;
	public GameObject reticle;

	// Gun
	public ShotGunScript shotGunScript;

	// Monster
	public GameObject monster_daemon;
	float monsterInterval;

	// property
	private float gazedTime;

	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (diveCamera.transform.position, diveCamera.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			reticle.transform.position = hit.point;

			if (hit.collider.tag == "Monster") {
				gazedTime += Time.deltaTime;

				// 発射
				if(gazedTime >= 2.2){
					Destroy(hit.collider.gameObject);
					shotGunScript.Fire ();
				}
			}
		}

		// モンスターを生成
		monsterInterval += Time.deltaTime;
		if (monsterInterval >= 0.1f) {
			GenerateMonster ();
		}
	}

	// モンスターを生成
	void GenerateMonster() {
		monsterInterval = 0.0f;

		Quaternion q = Quaternion.Euler(0, 0, 0);

		Instantiate (monster_daemon,
			new Vector3(Random.Range(-25, -21),
				transform.position.y,
				transform.position.z
			),
			q
		);
	}
}
