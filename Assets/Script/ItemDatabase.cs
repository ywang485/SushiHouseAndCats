using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase {

	private static readonly Dictionary<string, InventoryItem> items = new Dictionary<string, InventoryItem> {
		{"bags-of-tomatos", new InventoryItem("bags-of-tomatos", 100, "Cats hate tomato", "Sprites/Items/bags-of-tomatos", 10, "Prefabs/Weapons/Tomato")},
		{"cat-biscuit", new InventoryItem("cat-biscuit", 100, "Smells like fish", "Sprites/Items/cat-biscuit", 1, "Prefabs/PetProducts/CatBiscuit")},
		{"treadmill", new InventoryItem("treadmill", 300, "Workout for moving speed", "Sprites/treadmill")},
		{"mirror", new InventoryItem("mirror", 200, "Practice smile for more affinity", "Sprites/dartboard")},
		{"dartboard", new InventoryItem("dartboard", 200, "Play darts for item throwing speed", "Sprites/dartboard")},
		{"feather-duster", new InventoryItem("feather-duster", 100, "Attack + 10", "Sprites/Items/feather-duster", 50, "Prefabs/Weapons/FeatherDuster")}
	};

	public static InventoryItem getItem(string id) {
		if (items.ContainsKey(id)) {
			return items[id];
		}
		return null;
	}
}
