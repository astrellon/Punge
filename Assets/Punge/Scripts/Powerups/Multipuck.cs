using UnityEngine;
using System.Collections;

public class Multipuck : Powerup {

	public Multipuck() :
		base()
	{	
		Name = "Multipuck";
		ComponentColour = new Color(0.1f, 0.5f, 0.1f, 1.0f);
		StatusBack = (Texture2D)Resources.LoadAssetAtPath(@"Assets\Punge\Textures\MultipuckSmall.png", typeof(Texture2D));
		PowerupPrefab = (GameObject)Resources.LoadAssetAtPath(@"Assets\Punge\Prefabs\WidePaddle.prefab", typeof(GameObject));
	}
	public override void Activate(Player player, int stackSize) {
		GameManager.MainManager.SpawnPuck(0.0f, 0.0f).Go(true);	
	}
}
