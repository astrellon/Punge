using UnityEngine;
using System.Collections;

public class Tween : MonoBehaviour {
	
	public Vector3 StartValue;
	public Vector3 EndValue;
	public float Duration = 1.0f;
	protected float endTime = 0.0f;
	protected bool tweenActive = false;
	
	public void SetActive(bool active, bool autoSetStartValue = false) {
		tweenActive = active;
		endTime = Time.time + Duration;
		if (autoSetStartValue) {
			StartValue = transform.localPosition;	
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (tweenActive) {
			float t = 1.0f - ((endTime - Time.time) / Duration);
			if (t < 0.0f) {
				t = 0.0f;
			}
			if (t > 1.0f) {
				t = 1.0f;
				tweenActive = false;
			}
			Vector3 temp = new Vector3();
			temp.x = EaseInOut(StartValue.x, EndValue.x, t);
			temp.y = EaseInOut(StartValue.y, EndValue.y, t);
			temp.z = EaseInOut(StartValue.z, EndValue.z, t);
			transform.localPosition = temp;
		}
	}
	
	float EaseInOut(float start, float end, float t) {
		float temp = 1.0f - (1.0f + Mathf.Cos(Mathf.PI * t)) * 0.5f;
		return Lerp (start, end, temp);
	}
	
	float Lerp(float start, float end, float t) {
		return 	start + (end - start) * t;
	}
}
