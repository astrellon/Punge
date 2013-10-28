using UnityEngine;
using System.Collections;

public class PowerupComponent : MonoBehaviour {
	
	public bool PowerupEnabled { get; protected set; }
	public string PowerupName;
	
	public Vector3 Direction;
	
	// Use this for initialization
	void Start () {
	}
	
	public void Enable() {
		Direction.x = (Random.value > 0.5f ? 4.0f : -4.0f);
		this.rigidbody.velocity = Direction;
		this.rigidbody.angularVelocity = new Vector3(0.0f, 2f, 0.0f);
		PowerupEnabled = true;
	}
	public void Disable() {
		if (rigidbody != null) {
			rigidbody.velocity = new Vector3(0, 0, 0);
		}
		PowerupEnabled = false;
	}
	
	void OnTriggerEnter(Collider collider) {
		string t = collider.gameObject.tag;
		if (t == "Puck") {
			Puck puck = collider.gameObject.GetComponent<Puck>();
			if (puck == null) {
				return;
			}
			if (puck.LastHitLeft) {
				ActivatePowerup(GameManager.MainManager.Player1, 1);
			}
			else {
				ActivatePowerup(GameManager.MainManager.Player2, 1);	
			}
		}
		else if (t == "PaddleLeft") {
			ActivatePowerup(GameManager.MainManager.Player1, 1);
		}
		else if (t == "PaddleRight") {
			ActivatePowerup(GameManager.MainManager.Player2, 1);
		}
		else if (t == "Border" || t == "GoalLeft" || t == "GoalRight") {
			Destroy(gameObject);
		}
	}
	
	public void ActivatePowerup(Player byPlayer, int stackSize) {
		if (!PowerupEnabled) {
			return;
		}
		PowerupEnabled = false;
		Powerup powerup = Powerup.FindPowerup(PowerupName);
		byPlayer.AddPowerup(powerup);
		
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
