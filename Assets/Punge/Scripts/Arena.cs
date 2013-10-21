using UnityEngine;
using System.Collections;

public class Arena : MonoBehaviour {
	
	public float Width = 22.0f;
	public float Height = 14.0f;
	public float PaddleOffset = 3.0f;
	private GameObject Left;
	private GameObject Right;
	private GameObject Top;
	private GameObject Bottom;
	private GameObject Background;
	private Paddle PaddleLeft;
	private Paddle PaddleRight;
	// Use this for initialization
	void Start () {
		Left = transform.FindChild("Left").gameObject;
		Right = transform.FindChild("Right").gameObject;
		Top = transform.FindChild("Top").gameObject;
		Bottom = transform.FindChild("Bottom").gameObject;
		Background = transform.FindChild("Background").gameObject;
		
		GameObject obj = transform.FindChild("PaddleLeft").gameObject;
		if (obj != null)
		{
			PaddleLeft = obj.GetComponent<Paddle>();
		}
		obj = transform.FindChild("PaddleRight").gameObject;
		if (obj != null)
		{
			PaddleRight = obj.GetComponent<Paddle>();
		}
		
		UpdateSize();
	}
	
	public void UpdateSize() {
		Vector3 xScale = new Vector3(Width - 1.0f, 1.0f, 1.0f);
		Vector3 yScale = new Vector3(1.0f, Height + 1.0f, 1.0f);
		Left.transform.localScale = yScale;
		Left.transform.position = new Vector3(Width / -2.0f, 0.0f, 0.0f);
		Right.transform.localScale = yScale;
		Right.transform.position = new Vector3(Width / 2.0f, 0.0f, 0.0f);
		Top.transform.localScale = xScale;
		Top.transform.position = new Vector3(0.0f, Height / 2.0f, 0.0f);
		Bottom.transform.localScale = xScale;
		Bottom.transform.position = new Vector3(0.0f, Height / -2.0f, 0.0f);
		Background.transform.localScale = new Vector3(Width / 10.0f, 1.0f, Height / 10.0f);
		PaddleLeft.transform.position = new Vector3(Width / -2.0f + PaddleOffset, 0.0f, 0.0f);
		PaddleRight.transform.position = new Vector3(Width / 2.0f - PaddleOffset, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
