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
	public Vector3 direction;

	// Use this for initialization
	void Start () {
	
	}
	
	/*void OnTriggerEnter(Collision collision) {
		Debug.Log ("Powerup: " + collision.gameObject.tag);
	}*/
	
	public void ActivatePowerup(Player byPlayer) {
		forPlayer = byPlayer;
		byPlayer.paddle.Length *= 1.5f;
		active = true;
	}
	public void DeactivatePowerup() {
		active = false;
		forPlayer.paddle.Length *= (1.0f / 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(direction);
	}
}
