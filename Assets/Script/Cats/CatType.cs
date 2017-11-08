public class CatType {

	private string id;
	private string description;
	private string animator;
	private int maxHp;
	private float speed;
	private bool active;

	public CatType(string id, string description, string animator) {
		this.id = id;
		this.description = description;
		this.animator = animator;
		this.maxHp = 0;
		this.speed = 1f;
		this.active = true;
	}

	public CatType(string id, string description, string animator, int maxHp, float speed) {
		this.id = id;
		this.description = description;
		this.animator = animator;
		this.maxHp = maxHp;
		this.speed = speed;
		this.active = true;
	}

	public string getId() {
		return id;
	}

	public string getDescription() {
		return description;
	}

	public string getAnimator() {
		return animator;
	}

	public int getMaxHp() {
		return maxHp;
	}

	public float getSpeed() {
		return speed;
	}

	public void setActive(bool active) {
		this.active = active;
	}

	public bool getActive() {
		return this.active;
	}
}
