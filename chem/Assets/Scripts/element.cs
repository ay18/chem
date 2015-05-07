using UnityEngine;
using System.Collections;

public class Element
{

	public string name { get; set; }
	public string symbol { get; set; }
	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }
	public float radii { get; set; }
	// color maybe?

	private float scaleFactor = 0.02f;
	private float distanceScale = 0.7f;


	public Element (string[] parsedLine) {
		this.symbol = parsedLine [3].ToUpper();
		this.x = float.Parse(parsedLine [0]) * distanceScale;
		this.y = float.Parse(parsedLine [1]) * distanceScale;
		this.z = float.Parse(parsedLine [2]) * distanceScale;
		
		// DEFINE ELEMENTS HERE
		// switch statement based on chemical symbol
		switch (this.symbol) {
		case "H":
			this.name = "Hydrogren";
			this.radii = 53;
			break;
			
		case "C":
			this.name = "Carbon";
			this.radii = 67;
			break;
			
		case "N":
			this.name = "Nitrogen";
			this.radii = 56;
			break;
			
		case "O":
			this.name = "Oxygen";
			this.radii = 48;
			break;
		}

		this.radii = radii*scaleFactor;
	}


}

