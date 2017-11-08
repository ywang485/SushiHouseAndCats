using UnityEngine;
using System.Collections;

public class CatComponent {

	private string id;
	private string resourcePath;

	public CatComponent(string id, string resourcePath) {
		this.id = id;
		this.resourcePath = resourcePath;
	}

	public string getId() {
		return id;
	}
		
	public string getResourcePath() {
		return resourcePath;
	}
}
