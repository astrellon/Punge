using UnityEngine;
using System.Collections;

public class Puck : MonoBehaviour {
	
	public float Speed = 20.0f;
	public GameManager gameManager;
	public bool Active = false;
	
	public bool lastHitLeft { get; private set; }

	// Use this for initialization
	void Start () {
		lastHitLeft = false;
	}
	
	float dot(Vector3 v1, Vector3 v2) {
		return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;	
	}
	void OnCollisionEnter(Collision collision) {
		float dotted = dot(collision.relativeVelocity.normalized, new Vector3(0, 1.0f, 0));
		// Check if the new velocity is too verticle, if it is add a random horizontal velocity.
		// Horizontal velocities shouldn't be a problem as there are paddles.
		if (Mathf.Abs(Mathf.Abs(dotted) - 1) < 0.005) {
			Vector3 newVelo = this.rigidbody.velocity;
			newVelo.x += Random.value > 0.5 ? 4.0f : -4.0f;
			newVelo.Normalize();
			newVelo.Scale(new Vector3(Speed, Speed, 0.0f));
			this.rigidbody.velocity = newVelo;
		}
		
		if (collision.gameObject.tag == "PaddleLeft") {
			Paddle paddle = collision.gameObject.GetComponent<Paddle>();
			if (paddle != null) {
				AdjustAngle(paddle.CalcPuckBounce(this, collision.contacts[0]));	
			}
			lastHitLeft = true;	
		}
		else if (collision.gameObject.tag == "PaddleRight") {
			Paddle paddle = collision.gameObject.GetComponent<Paddle>();
			if (paddle != null) {
				AdjustAngle(paddle.CalcPuckBounce(this, collision.contacts[0]));	
			}
			lastHitLeft = false;	
		}
		else if (collision.gameObject.tag == "GoalLeft") {
			gameManager.HitGoal(true);
		}
		else if (collision.gameObject.tag == "GoalRight") {
			gameManager.HitGoal(false);	
		}
	}
	
	public void Reset() {
		this.transform.position = new Vector3(0, 0, 0);
		this.rigidbody.velocity = new Vector3(0, 0, 0);
		Active = false;
	}
	public void Go() {
		float angle = Random.value * 90.0f;
		angle += Mathf.Round(Random.value * 4.0f) * 90.0f;
		angle *= Mathf.PI / 180.0f;
		Vector3 startVelocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
		startVelocity.x = 1.0f;
		startVelocity.y = 0.0f;
		startVelocity.Normalize();
		startVelocity.Scale(new Vector3(Speed, Speed, 0.0f));
		rigidbody.velocity = startVelocity;
		Active = true;
	}
	
	public void AdjustAngle(float angle) {
		Quaternion rotation = Quaternion.Euler(0, 0, angle);
		rigidbody.velocity = rotation * rigidbody.velocity;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
