public class Ingredient {

	private string id;
	private int price;
	private string description;
	private string spritePath_s;
	private string spritePath_b;

	public Ingredient(string id, int price, string description, string spritePathS, string spritePathB) {
		this.id = id;
		this.price = price;
		this.description = description;
		this.spritePath_b = spritePathB;
		this.spritePath_s = spritePathS;
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

	public string getSpritePathB() {
		return spritePath_b;
	}

	public string getSpritePathS() {
		return spritePath_s;
	}
}
