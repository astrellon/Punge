using UnityEngine;
using System.Collections;

public class WidePaddle : Powerup {
	
	public WidePaddle() :
		base()
	{	
		Name = "WidePaddle";
	}
	public override void Activate(Player player, int stackSize) {
		player.Paddle.Length += 1.0f * stackSize;
		player.Paddle.UpdateSize();
	}
	public override void Deactivate(Player player, int stackSize) {
		player.Paddle.Length -= 1.0f * stackSize;
		player.Paddle.UpdateSize();
	}
	
}
