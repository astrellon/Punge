using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public static GameManager MainManager;
	
	public Player player1 = new Player();
	public Player player2 = new Player();
	public Arena arena;
	private bool winner = false;
	public int WinningScore = 5;
	public Puck puck;
	private bool waitingToStart = true;
	
	private float nextSpawnTime = 0.0f;
	
	public GameObject widePaddle;
	
	private GameObject powerupParent;
	
	protected List<Powerup> activePowerups = new List<Powerup>();
	// Use this for initialization
	void Start () {
		puck.gameManager = this;
		
		if (arena != null) {
			player1.paddle = arena.transform.FindChild("PaddleLeft").gameObject.GetComponent<Paddle>();
			player2.paddle = arena.transform.FindChild("PaddleRight").gameObject.GetComponent<Paddle>();
			MakePowerupsParent();
		}
		
		MainManager = this;
	}
	
	public void AddActivePowerup(Powerup powerup) {
		if (!activePowerups.Contains(powerup)) {
			activePowerups.Add(powerup);	
		}
	}
	public void RemoveActivePowerup(Powerup powerup) {
		activePowerups.Remove(powerup);
	}
	
	public void SpawnPowerup(string type) {
		GameObject obj = (GameObject)Instantiate(widePaddle, new Vector3(-4,0,0), transform.rotation);
		obj.transform.parent = powerupParent.transform;
		Powerup powerup = obj.GetComponent<Powerup>();
		Vector3 newPos = new Vector3(0, 0, 0);
		newPos.x = (Random.value - 0.5f) * arena.Width * 0.5f;
		newPos.y = (Random.value - 0.5f) * arena.Height * 0.75f;
		obj.transform.position = newPos;
		powerup.Enable();
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
		
		
		if (player1.score >= WinningScore || player2.score >= WinningScore) {	
			winner = true;
		}
		
		WaitStart();
	}
	
	float CalcNextSpawnTime() {
		return Time.time + Random.value * 2.0f + 1.0f;
	}
	public void StartGame() {
		nextSpawnTime = CalcNextSpawnTime();
		if (winner) {
			ResetScores();	
		}
		waitingToStart = false;
		puck.Go();
	}
	public void WaitStart() {
		puck.Reset();
		waitingToStart = true;
		nextSpawnTime = 0.0f;
		
		foreach (Powerup powerup in activePowerups) {
			powerup.DeactivatePowerup(false);
			powerup.Disable();
		}
		
		activePowerups.Clear();
		MakePowerupsParent();
	}
	
	void MakePowerupsParent() {
		if (powerupParent != null) {
			Destroy(powerupParent);
		}
		powerupParent = new GameObject("Powerups");
		powerupParent.transform.parent = arena.transform;
	}
	// Update is called once per frame
	void Update () {
		if (waitingToStart && Input.GetKeyDown(KeyCode.Space)) {
			StartGame ();
		}
		if (Time.time >= nextSpawnTime && nextSpawnTime > 0.0f) {
			SpawnPowerup("ASD");
			nextSpawnTime = CalcNextSpawnTime();
		}
	}
}
