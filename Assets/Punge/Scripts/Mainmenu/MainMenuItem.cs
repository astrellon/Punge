using UnityEngine;
using System.Collections;

public class MainMenuItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnMouseOver () {
		this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
	}
	void OnMouseExit() {
		this.transform.localScale = new Vector3(1f, 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
