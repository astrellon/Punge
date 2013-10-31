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
	private List<Puck> pucks = new List<Puck>();
	private bool WaitingToStart = true;
	
	private float NextSpawnTime = 0.0f;
	
	private GameObject PowerupParent;
	private GameObject PuckParent;
	
	private GUIText PressStartGUI;
	private GUIText WinnerGUI;
	
	private GameObject PuckPrefab;
	
	// Use this for initialization
	void Start () {
		PuckPrefab = (GameObject)Resources.LoadAssetAtPath(@"Assets\Punge\Prefabs\Puck.prefab", typeof(GameObject));
		
		Powerup widePaddle = new WidePaddle();
		Powerup.AddPowerup(widePaddle);
		Powerup.AddPowerup(new Multipuck());
		
		WinningScore = OptionValues.WinningScore;
		
		if (Arena != null) {
			Player1.Paddle = Arena.transform.FindChild("PaddleLeft").gameObject.GetComponent<Paddle>();
			Player1.Paddle.Owner = Player1;
			Player2.Paddle = Arena.transform.FindChild("PaddleRight").gameObject.GetComponent<Paddle>();
			Player2.Paddle.Owner = Player2;
			MakePowerupsParent();
			MakePuckParent();
			SpawnPuck(0.0f, 0.0f).Reset();
			
			Arena.Width = OptionValues.ArenaWidth;
			Arena.Height = OptionValues.ArenaHeight;
			Arena.UpdateSize();
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
			
			player1Status.UpdateTextures();
			player2Status.UpdateTextures();
		}
		
		// Pucks ignore hitting other pucks.
		// This is because there's a good chance that after a collision that one may end up with a weird
		// speed or angle that makes further play hard and unpredictable.
		// Should look at it again later and perhaps force a minimum speed after a collision,
		// although then you could end up with a system that always has energy being put into it.
		Physics.IgnoreLayerCollision(10, 10);
		
		MainManager = this;	
	}
	
	public void SpawnPowerup(string type) {
		Powerup powerup = Powerup.FindPowerup(type);
		if (powerup == null) {
			Debug.Log("Unable to find powerup " + type);
			return;	
		}
		Vector3 newPos = new Vector3(0, 0, 0);
		newPos.x = (Random.value - 0.5f) * Arena.Width * 0.5f;
		newPos.y = (Random.value - 0.5f) * Arena.Height * 0.75f;
		GameObject obj = (GameObject)Instantiate(powerup.PowerupPrefab, newPos, transform.rotation);
		obj.renderer.material.color = powerup.ComponentColour;
		obj.transform.parent = PowerupParent.transform;
		PowerupComponent powerupComp = obj.GetComponent<PowerupComponent>();
		powerupComp.PowerupName = type;
		obj.transform.position = newPos;
		powerupComp.Enable();
	}
	public Puck SpawnPuck(float x, float y) {
		GameObject puck = (GameObject)Instantiate(PuckPrefab, new Vector3(x, y, 0.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
		puck.transform.parent = PuckParent.transform;
		Puck puckComp = puck.GetComponent<Puck>();
		puckComp.Manager = this;
		pucks.Add(puckComp);
		
		return puckComp;
	}
	
	public void ResetScores() {
		Player1.Score = 0;
		Player2.Score = 0;
		Winner = false;
		WinnerGUI.gameObject.SetActive(false);
		UpdateScore();
	}
	
	public void HitGoal(Puck puck, bool left, int amount = 1) {
		if (left) {	
			Player2.Score += amount;
		}
		else {
			Player1.Score += amount;
		}
		
		UpdateScore();
		if (Player1.Score >= WinningScore || Player2.Score >= WinningScore) {	
			Winner = true;
			UpdateWinner();
			WaitStart();
		}
		pucks.Remove(puck);
		Destroy(puck.gameObject);
		if (pucks.Count == 0) {
			WaitStart();
		}
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
		pucks[0].Go(false);
		UpdateWinner();
	}
	public void WaitStart() {
		WaitingToStart = true;
		NextSpawnTime = 0.0f;
		
		foreach (Puck puck in pucks) {
			Destroy (puck.gameObject);	
		}
		pucks.Clear ();
		
		Vector3 newPos = Player1.Paddle.transform.position;
		newPos.y = 0.0f;
		Player1.Paddle.transform.position = newPos;
		Player1.ResetPowerups();
		
		newPos = Player2.Paddle.transform.position;
		newPos.y = 0.0f;
		Player2.Paddle.transform.position = newPos;
		Player2.ResetPowerups();
		
		MakePowerupsParent();
		MakePuckParent();
		SpawnPuck(0.0f, 0.0f).Reset();
	}
	
	void MakePowerupsParent() {
		if (PowerupParent != null) {
			Destroy(PowerupParent);
		}
		PowerupParent = new GameObject("Powerups");
		PowerupParent.transform.parent = Arena.transform;
	}
	void MakePuckParent() {
		if (PuckParent != null) {
			Destroy(PuckParent);
		}
		PuckParent = new GameObject("Pucks");
		PuckParent.transform.parent = Arena.transform;
	}
	// Update is called once per frame
	void Update () {
		if (WaitingToStart && Input.GetKeyDown(KeyCode.Space)) {
			StartGame();
		}
		if (Time.time >= NextSpawnTime && NextSpawnTime > 0.0f) {
			//SpawnPowerup("WidePaddle");
			SpawnPowerup("Multipuck");
			NextSpawnTime = CalcNextSpawnTime();
		}
		if (!WaitingToStart) {
			Player1.Update();
			Player2.Update();
		}
		
		if (Input.GetKeyDown(KeyCode.A)) {
			SpawnPuck(0.0f, 0.0f).Go(true);	
		}
		
		PressStartGUI.enabled = WaitingToStart;
	}
}
