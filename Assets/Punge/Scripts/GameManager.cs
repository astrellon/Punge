using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public static GameManager MainManager;
	
	public Player Player1 = new Player();
	public Player Player2 = new Player();
	public GameObject UI;
	public Arena Arena;
	private bool Winner = false;
	public int WinningScore = 5;
	public Puck Puck;
	private bool WaitingToStart = true;
	
	private float NextSpawnTime = 0.0f;
	
	// Prefabs
	public GameObject WidePaddle;
	public GameObject WidePaddleGUI;
	
	private GameObject PowerupParent;
	
	private GUIText PressStartGUI;
	private GUIText WinnerGUI;
	
	// Use this for initialization
	void Start () {
		Puck.gameManager = this;
		
		Powerup widePaddle = new WidePaddle();
		Powerup.AddPowerup(widePaddle);
		
		if (Arena != null) {
			Player1.Paddle = Arena.transform.FindChild("PaddleLeft").gameObject.GetComponent<Paddle>();
			Player1.Paddle.Owner = Player1;
			Player2.Paddle = Arena.transform.FindChild("PaddleRight").gameObject.GetComponent<Paddle>();
			Player2.Paddle.Owner = Player2;
			MakePowerupsParent();
		}
		
		if (UI != null) {
			Player1.ScoreDisplay = UI.transform.FindChild("Player1").GetComponent<GUIText>();
			Player2.ScoreDisplay = UI.transform.FindChild("Player2").GetComponent<GUIText>();
			PressStartGUI = UI.transform.FindChild("PressStart").GetComponent<GUIText>();
			PressStartGUI.enabled = false;
			WinnerGUI = UI.transform.FindChild("Winner").GetComponent<GUIText>();
			WinnerGUI.gameObject.SetActive(false);
			
			StatusComponent player1Status = UI.transform.FindChild("WidePaddleGUI1").GetComponent<StatusComponent>();
			player1Status.ForPlayer = Player1;
			player1Status.ForPowerup = widePaddle;
			
			StatusComponent player2Status = UI.transform.FindChild("WidePaddleGUI2").GetComponent<StatusComponent>();
			player2Status.ForPlayer = Player2;
			player2Status.ForPowerup = widePaddle;
		}
		
		MainManager = this;
		
		
	}
	
	public void SpawnPowerup(string type) {
		GameObject obj = (GameObject)Instantiate(WidePaddle, new Vector3(-4,0,0), transform.rotation);
		obj.transform.parent = PowerupParent.transform;
		PowerupComponent powerup = obj.GetComponent<PowerupComponent>();
		Vector3 newPos = new Vector3(0, 0, 0);
		newPos.x = (Random.value - 0.5f) * Arena.Width * 0.5f;
		newPos.y = (Random.value - 0.5f) * Arena.Height * 0.75f;
		obj.transform.position = newPos;
		powerup.Enable();
	}
	
	public void ResetScores() {
		Player1.Score = 0;
		Player2.Score = 0;
		Winner = false;
		WinnerGUI.gameObject.SetActive(false);
		UpdateScore();
	}
	
	public void HitGoal(bool left, int amount = 1) {
		if (left) {	
			Player2.Score += amount;
		}
		else {
			Player1.Score += amount;
		}
		
		UpdateScore();
		if (Player1.Score >= WinningScore || Player2.Score >= WinningScore) {	
			Winner = true;
		}
		UpdateWinner();
		WaitStart();
	}
	
	void UpdateScore() {
		Player1.ScoreDisplay.text = "Player 1: " + Player1.Score;
		Player2.ScoreDisplay.text = "Player 2: " + Player2.Score;
	}
	void UpdateWinner() {
		if (Winner) {
			WinnerGUI.gameObject.SetActive(true);
			
			if (Player1.Score == Player2.Score && Player1.Score >= WinningScore) {
				WinnerGUI.text = "It's a Tie";
			}
			else if (Player1.Score > Player2.Score) {
				WinnerGUI.text = "Player 1 Wins!";
			}
			else {
				WinnerGUI.text = "Player 2 Wins!";
			}
		}
		else {
			WinnerGUI.gameObject.SetActive(false);	
		}
	}
	
	float CalcNextSpawnTime() {
		return Time.time + Random.value * 2.0f + 1.0f;
	}
	public void StartGame() {
		NextSpawnTime = CalcNextSpawnTime();
		if (Winner) {
			ResetScores();	
		}
		WaitingToStart = false;
		Puck.Go();
		UpdateWinner();
	}
	public void WaitStart() {
		Puck.Reset();
		WaitingToStart = true;
		NextSpawnTime = 0.0f;
		Vector3 newPos = Player1.Paddle.transform.position;
		newPos.y = 0.0f;
		Player1.Paddle.transform.position = newPos;
		Player1.ResetPowerups();
		newPos = Player2.Paddle.transform.position;
		newPos.y = 0.0f;
		Player2.Paddle.transform.position = newPos;
		Player2.ResetPowerups();
		
		MakePowerupsParent();
	}
	
	void MakePowerupsParent() {
		if (PowerupParent != null) {
			Destroy(PowerupParent);
		}
		PowerupParent = new GameObject("Powerups");
		PowerupParent.transform.parent = Arena.transform;
	}
	// Update is called once per frame
	void Update () {
		if (WaitingToStart && Input.GetKeyDown(KeyCode.Space)) {
			StartGame ();
		}
		if (Time.time >= NextSpawnTime && NextSpawnTime > 0.0f) {
			SpawnPowerup("WidePaddle");
			NextSpawnTime = CalcNextSpawnTime();
		}
		if (!WaitingToStart) {
			Player1.Update();
			Player2.Update();
		}
		
		PressStartGUI.enabled = WaitingToStart;
	}
}
