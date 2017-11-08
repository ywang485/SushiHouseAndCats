using System.Collections.Generic;

public class SushiType {

	private string id;
	private int price;
	private string description;
	private string spritePath_s;
	private string spritePath_b;
	private readonly Dictionary<string, int> ingredients;

	public SushiType(string id, int price, string description, string spritePath_s, string spritePath_b, Dictionary<string,int> ingredients) {
		this.id = id;
		this.description = description;
		this.spritePath_s = spritePath_s;
		this.spritePath_b = spritePath_b;
		this.ingredients = ingredients;
		this.price = price;
	}

	public string getId() {
		return id;
	}

	public int getPrice() {
		return price;
	}

	public string getDescription() {
		return description;
	}

	public string getSpritePath_s() {
		return spritePath_s;
	}

	public string getSpritePath_b() {
		return spritePath_b;
	}

	public Dictionary<string, int> getIngredients() {
		return ingredients;
	}
}
