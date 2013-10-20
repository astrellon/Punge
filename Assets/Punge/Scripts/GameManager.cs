using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public int ScoreLeft = 0;
	public int ScoreRight = 0;
	private bool winner = false;
	public int WinningScore = 5;
	public Puck puck;
	private bool waitingToStart = true;
	// Use this for initialization
	void Start () {
		puck.gameManager = this;
	}
	
	void OnGUI () {
		GUI.Label(new Rect(10.0f, 10.0f, 100.0f, 20.0f), "Player 1: " + ScoreLeft);	
		GUI.Label(new Rect(10.0f, 30.0f, 100.0f, 20.0f), "Player 2: " + ScoreRight);
		if (waitingToStart) {
			GUI.Label(new Rect(300.0f, 150.0f, 200.0f, 20.0f), "Press Space");	
		}
		if (winner) {
			if (ScoreLeft == ScoreRight && ScoreLeft >= WinningScore) {
				GUI.Label(new Rect(300.0f, 100.0f, 200.0f, 20.0f), "It's a Tie");	
			}
			else if (ScoreLeft > ScoreRight) {
				GUI.Label(new Rect(300.0f, 100.0f, 200.0f, 20.0f), "Player 1 Wins!");
			}
			else {
				GUI.Label(new Rect(300.0f, 100.0f, 200.0f, 20.0f), "Player 2 Wins!");
			}
		}
	}
	
	public void ResetScores() {
		ScoreLeft = 0;
		ScoreRight = 0;
		winner = false;
	}
	
	public void HitGoal(bool left, int amount = 1) {
		if (left) {	
			ScoreRight += amount;
		}
		else {
			ScoreLeft += amount;
		}
		puck.Reset();
		waitingToStart = true;
		
		if (ScoreLeft >= WinningScore || ScoreRight >= WinningScore) {	
			winner = true;
		}
	}
	// Update is called once per frame
	void Update () {
		if (waitingToStart && Input.GetKeyDown(KeyCode.Space)) {
			if (winner) {
				ResetScores();	
			}
			waitingToStart = false;
			puck.Go();
		}
	}
}
