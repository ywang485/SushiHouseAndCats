using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatComponentDatabase {

	public static readonly List<CatComponent> normalHeads = new List<CatComponent> () {
		new CatComponent ("normal-head-1", "CatComponents/normal-head-1")
	};

	public static readonly List<CatComponent> normalBodies = new List<CatComponent>() {
		new CatComponent("normal-body-1", "CatComponents/normal-body-1")
	};

	public static readonly List<CatComponent> normalEyes = new List<CatComponent>() {
		new CatComponent("normal-eyes-1", "CatComponents/normal-eyes-1")
	};

	public static readonly List<CatComponent> normalHeadMarks = new List<CatComponent>() {
		new CatComponent("normal-head-mark-1", "CatComponents/normal-head-mark-1")
	};

	public static readonly List<CatComponent> normalBackMarks = new List<CatComponent>() {
		new CatComponent("normal-back-mark-1", "CatComponents/normal-head-mark-1")
	};

	public static readonly List<CatComponent> normalBellyMarks = new List<CatComponent>() {
		new CatComponent("normal-belly-mark-1", "CatComponents/normal-belly-mark-1")
	};

	public static readonly List<CatComponent> normalTailMarks = new List<CatComponent>() {
		new CatComponent("normal-tail-mark-1", "CatComponents/normal-tail-mark-1")
	};

	public static readonly List<Color> normalBodyColors = new List<Color> () {
		new Color (1f, 1f, 1f),
		new Color (0.76f, 0.76f, 0.76f),
		new Color (0.51f, 0.51f, 0.51f),
		new Color (0.27f, 0.27f, 0.27f)
	};
}
