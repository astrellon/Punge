using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
	
	public class PowerupStack {
		public float StartTime { get; set; }
		public float Duration { get; set; }
		public int Size { get; set; }
		public Powerup Powerup { get; set; }
		
		public PowerupStack() {
			StartTime = 0.0f;
			Duration = 0.0f;
			Size = 0;
			
			Powerup = null;
		}
		
		public void UpdateDuration(float duration) {
			if (Duration < duration) {	
				Duration = duration;	
			}
			ResetTimer();
		}
		public void ResetTimer() {
			StartTime = Time.time;
		}
	}
	
	public int Score { get; set; }
	public GUIText ScoreDisplay { get; set; }
	public Paddle Paddle { get; set; }
	public Dictionary<string, PowerupStack> PowerupStacks { get; private set; }
	public string PlayerId { get; set; }
	
	public Player() {
		Score = 0;
		Paddle = null;
		PowerupStacks = new Dictionary<string, PowerupStack>();
	}

	public void Reset() {
		Score = 0;
	}
	
	public void AddPowerup(Powerup powerup) {
		PowerupStack stack;
		if (!PowerupStacks.ContainsKey(powerup.Name)) {	
			PowerupStacks[powerup.Name] = new PowerupStack();
		}
		stack = PowerupStacks[powerup.Name];
		
		stack.UpdateDuration(powerup.Duration);
		stack.Powerup = powerup;
		if (stack.Size < powerup.MaxStackSize) {
			stack.Size++;
			powerup.Activate(this, 1);
		}
	}
	
	public void ResetPowerups() {
		foreach (KeyValuePair<string, PowerupStack> pair in PowerupStacks) {
			if (pair.Value.Powerup != null) {
				pair.Value.Powerup.Deactivate(this, pair.Value.Size);	
			}
		}
		PowerupStacks.Clear();
	}
	
	public void Update() {
		float time = Time.time;
		foreach (KeyValuePair<string, PowerupStack> pair in PowerupStacks) {
			PowerupStack stack = pair.Value;
			if (stack.StartTime > 0 && stack.Duration > 0 && time >= stack.StartTime + stack.Duration) {
				stack.Powerup.Deactivate(this, stack.Size);
				stack.StartTime = 0;
				stack.Size = 0;
				stack.Powerup = null;
			}
		}
		if (PowerupStacks.ContainsKey("Bounce")) {
			if (PlayerId == "1" && Input.GetKeyDown(KeyCode.Q)) {
				Bounce bounce = (Bounce)Powerup.FindPowerup("Bounce");
				bounce.DoBounce(true);
			}
			else if (PlayerId == "2" && Input.GetKeyDown(KeyCode.W)) {
				
			}
		}
	}
}
