using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Powerup {
	
	public static Powerup FindPowerup(string name) {
		if (allPowerups.ContainsKey(name)) {
			return allPowerups[name];
		}
		return null;
	}
	public static void AddPowerup(Powerup powerup) {
		allPowerups[powerup.Name] = powerup;	
	}
	public static bool RemovePowerup(Powerup powerup) {
		return allPowerups.Remove(powerup.Name);
	}
	private static Dictionary<string, Powerup> allPowerups = new Dictionary<string, Powerup>();
	
	public string Name { get; protected set; }
	public float Duration { get; protected set; }
	public int MaxStackSize { get; protected set; }
	public Texture2D StatusBack { get; set; }
	public Color ComponentColour { get; set; }
	
	public GameObject PowerupPrefab { get; set; }
	
	public Powerup() {
		Name = "Unknown";
		Duration = 6.0f;
		MaxStackSize = 5;
		ComponentColour = new Color(0.9f, 0.9f, 0.9f, 1.0f);
	}
	
	public virtual void Activate(Player player, int stackSize) {
		
	}
	public virtual void Deactivate(Player player, int stackSize) {
		
	}
	
	public virtual void Activate(Puck puck) {
		
	}
	
}
