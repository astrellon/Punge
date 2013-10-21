using UnityEngine;
using System.Collections;

public class Player {
	
	public int score
	{
		get;
		set;
	}
	public Paddle paddle
	{
		get;
		set;
	}
	
	public Player() {
		score = 0;
		paddle = null;
	}

	public void Reset() {
		score = 0;
	}
}
