using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public Text ScoreText;
	int Score;

	// Use this for initialization
	void Start () {
		Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		ScoreText.text = "Score: " + Score;
	}

	public void ScorePlus(int point) {
		Score += point;
	}
}
