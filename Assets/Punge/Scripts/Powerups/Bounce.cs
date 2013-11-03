using UnityEngine;
using System.Collections;

public class Bounce : Powerup {
	
	public Bounce() :
		base()
	{	
		Name = "Bounce";
		Duration = -1.0f;
		ComponentColour = new Color(0.15f, 0.2f, 0.65f, 1.0f);
		StatusBack = (Texture2D)Resources.LoadAssetAtPath(@"Assets\Punge\Textures\Bounce.png", typeof(Texture2D));
		PowerupPrefab = (GameObject)Resources.LoadAssetAtPath(@"Assets\Punge\Prefabs\WidePaddle.prefab", typeof(GameObject));
	}
	/*public override void Activate(Player player, int stackSize) {
		player.Paddle.Length += 1.0f * stackSize;
		player.Paddle.UpdateSize();
	}
	public override void Deactivate(Player player, int stackSize) {
		player.Paddle.Length -= 1.0f * stackSize;
		player.Paddle.UpdateSize();
	}*/
	
}
