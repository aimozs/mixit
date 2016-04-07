using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recipe : MonoBehaviour {

	public string cocktailName;
	public bool includeInBuild = true;
	public Color contentColor;

	public int tried = 0;
	public int success = 0;

	public List<Item> glassware = new List<Item>();
	public List<Item> preparation = new List<Item>();
	public List<Item> spirit = new List<Item>();
	public List<Item> liqueur = new List<Item>();
	public List<Item> mixer = new List<Item>();
	public List<Item> presentation = new List<Item>();


}
