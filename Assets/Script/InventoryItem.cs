using UnityEngine;
using System.Collections;

public class InventoryItem {

	private string id;
	private int price;
	private string description;
	private string spritePath;

	// Weapon
	private int attack = 0;

	private string prefabPath;

	// For general items
	public InventoryItem(string id, int price, string description, string spritePath) {
		this.id = id;
		this.price = price;
		this.description = description;
		this.spritePath = spritePath;
	}

	// For weapons
	public InventoryItem(string id, int price, string description, string spritePath, int attack, string prefabPath) {
		this.id = id;
		this.price = price;
		this.description = description;
		this.spritePath = spritePath;
		this.attack = attack;
		this.prefabPath = prefabPath;
	}

	public string getId() {
		return id;
	}

	public int getPrice() {
		return price;
	}

	public string getDescription () {
		return description;
	}

	public string getSpritePath() {
		return spritePath;
	}

	public int getAttack() {
		return attack;
	}

	public string getPrefabPath() {
		return prefabPath;
	}
}
