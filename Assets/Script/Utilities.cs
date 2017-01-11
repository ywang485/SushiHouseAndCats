using UnityEngine;
using System.Collections;

public class Utilities {

	public static Vector2 coordinateTransform(Vector2 original) {
		Quaternion rotation = Quaternion.Euler (0, 0, -45);
		Vector3 tmp = new Vector3 (original.x, original.y, 0);
		tmp = rotation * tmp;
		return new Vector2 (tmp.x, tmp.y);
	}

	public static Vector2 coordinateReverseTransform(Vector2 original) {
		Quaternion rotation = Quaternion.Euler (0, 0, 45);
		Vector3 tmp = new Vector3 (original.x, original.y, 0);
		tmp = rotation * tmp;
		return new Vector2 (tmp.x, tmp.y);
	}

	public static Vector2 getDirection(Vector2 src, Vector2 dest) {
		Vector2 diff = coordinateTransform(dest) - coordinateTransform(src);
		if(Mathf.Abs(diff.x) > Mathf.Abs(diff.y)) {
			return new Vector2(diff.x, 0);
		} else {
			return new Vector2(0, diff.y);
		}
	}
}