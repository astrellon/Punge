using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	
	public float duration = 10.0f;
	public bool active 
	{
		get;
		set;
	}
	protected Player forPlayer = null;
	
	protected float startTime;
	public Vector3 direction;
	
	// Use this for initialization
	void Start () {
		direction.x = (Random.value > 0.5f ? 4.0f : -4.0f);
		this.rigidbody.velocity = direction;
	}
	
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Puck") {
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
		else if (collider.gameObject.tag == "PaddleLeft") {
			ActivatePowerup(GameManager.MainManager.player1);	
		}
		else if (collider.gameObject.tag == "PaddleRight") {
			ActivatePowerup(GameManager.MainManager.player2);	
		}
		else {
			Destroy(gameObject);
		}
	}
	
	public void ActivatePowerup(Player byPlayer) {
		if (active) {
			return;
		}
		forPlayer = byPlayer;
		byPlayer.paddle.Length *= 1.5f;
		byPlayer.paddle.UpdateSize();
		active = true;
		renderer.enabled = false;
		rigidbody.velocity = new Vector3(0,0,0);
		startTime = Time.time;
	}
	public void DeactivatePowerup() {
		if (!active) {
			return;
		}
		active = false;
		forPlayer.paddle.Length *= (1.0f / 1.5f);
		forPlayer.paddle.UpdateSize();
	}
	
	// Update is called once per frame
	void Update () {
		if (active && Time.time - startTime >= duration) {
			DeactivatePowerup();
			Destroy(gameObject);
		}
	}
}
