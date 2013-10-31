using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {
	
	public Slider ArenaWidth;
	public Slider ArenaHeight;
	public Slider WinningScore;
	
	public Keychoose P1Up;
	public Keychoose P1Down;
	public Keychoose P2Up;
	public Keychoose P2Down;

	// Use this for initialization
	void Start () {
		OptionValues.KeyBindings["P1Up"] = KeyCode.A;
		OptionValues.KeyBindings["P1Down"] = KeyCode.Z;
		OptionValues.KeyBindings["P2Up"] = KeyCode.UpArrow;
		OptionValues.KeyBindings["P2Down"] = KeyCode.DownArrow;
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
		
		if (P1Up != null) {
			P1Up.Changed += (sender, e) =>
			{
				OptionValues.KeyBindings["P1Up"] = e.Code;
			};
		}
		if (P1Down != null) {
			P1Down.Changed += (sender, e) =>
			{
				OptionValues.KeyBindings["P1Down"] = e.Code;
			};
		}
		if (P2Up != null) {
			P2Up.Changed += (sender, e) =>
			{
				OptionValues.KeyBindings["P2Up"] = e.Code;
			};
		}
		if (P2Down != null) {
			P2Down.Changed += (sender, e) =>
			{
				OptionValues.KeyBindings["P2Down"] = e.Code;
			};
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
