using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour {
	
	private Color OriginalColour;
	private float t = 1.0f;
	// Use this for initialization
	void Start () {
		OriginalColour = renderer.material.color;
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Puck") {	
			LightUp();	
		}
	}
	
	public void LightUp() {
		renderer.material.color = Color.white;
		t = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (t < 1.0f) {
			t += Time.deltaTime;
			renderer.material.color = Color.Lerp(Color.white, OriginalColour, t);
		}
	}
}
