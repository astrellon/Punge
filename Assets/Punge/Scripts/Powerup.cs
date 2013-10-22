using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	
	public float duration = 10.0f;
	public bool active 
	{
		get;
		set;
	}
	public bool powerupEnabled { get; protected set; }
	public Player forPlayer { get; protected set; }
	
	protected float startTime;
	public Vector3 direction;
	
	// Use this for initialization
	void Start () {
	}
	
	public void Enable() {
		direction.x = (Random.value > 0.5f ? 4.0f : -4.0f);
		this.rigidbody.velocity = direction;
		powerupEnabled = true;
	}
	public void Disable() {
		Debug.Log ("This?: " + this);
		if (rigidbody != null) {
			rigidbody.velocity = new Vector3(0, 0, 0);
		}
		powerupEnabled = false;
	}
	
	void OnTriggerEnter(Collider collider) {
		if (!powerupEnabled) {
			return;	
		}
		
		string t = collider.gameObject.tag;
		if (t == "Puck") {
			Puck puck = collider.gameObject.GetComponent<Puck>();
			if (puck == null) {
				return;
			}
			if (puck.lastHitLeft) {
				ActivatePowerup(GameManager.MainManager.player1);
			}
			else {
				ActivatePowerup(GameManager.MainManager.player2);	
			}
		}
		else if (t == "PaddleLeft") {
			ActivatePowerup(GameManager.MainManager.player1);	
		}
		else if (t == "PaddleRight") {
			ActivatePowerup(GameManager.MainManager.player2);	
		}
		else if (t == "Border" || t == "GoalLeft" || t == "GoalRight") {
			GameManager.MainManager.RemoveActivePowerup(this);
			Destroy(gameObject);
		}
	}
	
	public void ActivatePowerup(Player byPlayer) {
		if (active) {
			return;
		}
		forPlayer = byPlayer;
		byPlayer.paddle.Length += 1.0f;
		byPlayer.paddle.UpdateSize();
		active = true;
		renderer.enabled = false;
		startTime = Time.time;
		
		GameManager.MainManager.AddActivePowerup(this);
	}
	
	public void DeactivatePowerup(bool timeout) {
		Debug.Log ("Deactive? " + active);
		if (!active) {
			return;
		}
		active = false;
		forPlayer.paddle.Length -= 1.0f;
		forPlayer.paddle.UpdateSize();
		if (timeout) {
			GameManager.MainManager.RemoveActivePowerup(this);	
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (active && Time.time - startTime >= duration) {
			DeactivatePowerup(true);
			GameManager.MainManager.RemoveActivePowerup(this);
			Destroy(gameObject);
		}
	}
}
