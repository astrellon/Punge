using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {
	
	public Slider ArenaWidth;
	public Slider ArenaHeight;
	public Slider WinningScore;

	// Use this for initialization
	void Start () {
		if (ArenaWidth != null) {
			ArenaWidth.SliderValue = OptionValues.ArenaWidth;
			ArenaWidth.Changed += (sender, e) => 
			{
				OptionValues.ArenaWidth = e.Value;
			};
		}
		if (ArenaHeight != null) {
			ArenaHeight.SliderValue = OptionValues.ArenaHeight;
			ArenaHeight.Changed += (sender, e) => 
			{
				OptionValues.ArenaHeight = e.Value;
			};
		}
		if (WinningScore != null) {
			WinningScore.SliderValue = OptionValues.WinningScore;
			WinningScore.Changed += (sender, e) => 
			{
				OptionValues.WinningScore = (int)Mathf.Round (e.Value);
			};
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
