using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	//public int ScoreLeft = 0;
	//public int ScoreRight = 0;
	public Player player1 = new Player();
	public Player player2 = new Player();
	public Arena arena;
	private bool winner = false;
	public int WinningScore = 5;
	public Puck puck;
	private bool waitingToStart = true;
	// Use this for initialization
	void Start () {
		puck.gameManager = this;
		
		if (arena != null) {
			player1.paddle = arena.transform.FindChild("PaddleLeft").gameObject.GetComponent<Paddle>();	
			player2.paddle = arena.transform.FindChild("PaddleRight").gameObject.GetComponent<Paddle>();
		}
	}
	
	void OnGUI () {
		GUI.Label(new Rect(10.0f, 10.0f, 100.0f, 20.0f), "Player 1: " + player1.score);	
		GUI.Label(new Rect(10.0f, 30.0f, 100.0f, 20.0f), "Player 2: " + player2.score);
		if (waitingToStart) {
			GUI.Label(new Rect(300.0f, 150.0f, 200.0f, 20.0f), "Press Space");	
		}
		if (winner) {
			if (player1.score == player2.score && player1.score >= WinningScore) {
				GUI.Label(new Rect(300.0f, 100.0f, 200.0f, 20.0f), "It's a Tie");	
			}
			else if (player1.score > player2.score) {
				GUI.Label(new Rect(300.0f, 100.0f, 200.0f, 20.0f), "Player 1 Wins!");
			}
			else {
				GUI.Label(new Rect(300.0f, 100.0f, 200.0f, 20.0f), "Player 2 Wins!");
			}
		}
	}
	
	public void ResetScores() {
		player1.score = 0;
		player2.score = 0;
		winner = false;
	}
	
	public void HitGoal(bool left, int amount = 1) {
		if (left) {	
			player2.score += amount;
		}
		else {
			player1.score += amount;
		}
		puck.Reset();
		waitingToStart = true;
		
		if (player1.score >= WinningScore || player2.score >= WinningScore) {	
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
