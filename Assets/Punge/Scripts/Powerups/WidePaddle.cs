using UnityEngine;
using System.Collections;

public class WidePaddle : Powerup {
	
	public WidePaddle() :
		base()
	{	
		Name = "WidePaddle";
		ComponentColour = new Color(0.5f, 0.1f, 0.1f, 1.0f);
		StatusBack = (Texture2D)Resources.LoadAssetAtPath(@"Assets\Punge\Textures\WidePaddleSmall.png", typeof(Texture2D));
		PowerupPrefab = (GameObject)Resources.LoadAssetAtPath(@"Assets\Punge\Prefabs\WidePaddle.prefab", typeof(GameObject));
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
