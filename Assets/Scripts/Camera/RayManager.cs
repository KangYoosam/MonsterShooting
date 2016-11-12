using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class RayManager : MonoBehaviour {

	// Player
	public GameObject diveCamera;
	public GameObject reticle;
	public GameObject blood;

	// Gun
	public ShotGunScript shotGunScript;

	// Monster
	public GameObject Daemon;
	public GameObject Crawler;
	float monsterInterval;
	ArrayList appearableMonsters;

	MonsterScript monsterScript;
	ScoreController Score;

	// property
	private float gazedTime;
	private bool isDead = false;
	private float size;

	public Text notification;

	Transform[] positions;

	void Start () {
		GameObject positionsWrapper = GameObject.Find ("Cave/EmergePositions");

		// 子要素をすべて取得（自身のオブジェクトは除く）
		positions = positionsWrapper.transform.GetComponentsInChildren<Transform> ()
			.Where( c => positionsWrapper.gameObject != c.gameObject )
			.ToArray();

		Score = GameObject.Find ("MyselfEye/Dive_Camera/Gun").GetComponent<ScoreController> ();

		appearableMonsters = new ArrayList ();

		appearableMonsters.Add (Daemon);

		notification.gameObject.SetActive (false);

		Debug.Log (notification.text);
	}

	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (diveCamera.transform.position, diveCamera.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			reticle.transform.position = hit.point;

			size = 0.1f * hit.distance;

			if (hit.collider.tag == "Monster") {
				gazedTime += Time.deltaTime;

				size = size * gazedTime;

				// 発射
				if (gazedTime >= 1.4f) {
					shotGunScript.Fire (hit.collider.gameObject);
					gazedTime = 0;
				}
			} else {
				gazedTime = 0;
			}

			reticle.transform.localScale = new Vector2 (size, size);

			// 血飛沫を表示
			if (isDead) {
				blood.SetActive (true);
			}
		}

		AddAppearableMonster ();

		// モンスターを生成
		monsterInterval += Time.deltaTime;
		if (monsterInterval >= 2f) {
			GenerateMonster ();
		}
	}

	void AddAppearableMonster () {
		if (Score.isGreaterThan (50) && appearableMonsters.Count == 1) {
			notification.gameObject.SetActive (true);
			appearableMonsters.Add (Crawler);
		}
	}

	// モンスターを生成
	void GenerateMonster() {
		monsterInterval = 0.0f;

		Quaternion q = Quaternion.Euler(0, 0, 0);

		int number = Random.Range (0, positions.Length);

		GameObject nextMonster = appearableMonsters [Random.Range (0, appearableMonsters.Count)] as GameObject;

		GameObject monster = Instantiate (nextMonster,
			positions[number].transform.position,
			q
		) as GameObject;

		monster.transform.LookAt (diveCamera.transform.position);

		// MoveToCameraScriptのtargetプロパティにmonsterをアタッチ。
		MonsterScript monsterScript = monster.GetComponent<MonsterScript> ();
		monsterScript.target = diveCamera;
	}

	public void PlayerDead () {
		isDead = true;
	}
}
