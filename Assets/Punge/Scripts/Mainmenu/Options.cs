using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {
	
	public Slider ArenaWidth;
	public Slider ArenaHeight;

	// Use this for initialization
	void Start () {
		if (ArenaWidth != null) {
			ArenaWidth.Changed += (sender, e) => 
			{
				OptionValues.ArenaWidth = e.Value;
			};
		}
		if (ArenaHeight != null) {
			ArenaHeight.Changed += (sender, e) => 
			{
				OptionValues.ArenaHeight = e.Value;
			};
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
