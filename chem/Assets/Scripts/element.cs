using UnityEngine;
using System.Collections;


// Template for element definitions
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
	private float distanceScale = 0.75f;


	public Element (string[] parsedLine) {
		this.symbol = parsedLine [3].ToUpper();
		this.x = float.Parse(parsedLine [0]) * distanceScale;
		this.y = float.Parse(parsedLine [1]) * distanceScale;
		this.z = float.Parse(parsedLine [2]) * distanceScale;
		
		// Define element characteristics here
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
			
		case "F":
			this.name = "Fluorine";
			this.radii = 42;
			break;
			
		case "NA":
			this.name = "Sodium";
			this.radii = 190;
			break;
			
		case "MG":
			this.name = "Magnesium";
			this.radii = 145;
			break;
			
		case "SI":
			this.name = "Silicon";
			this.radii = 111;
			break;
			
		case "P":
			this.name = "Phosphorus";
			this.radii = 98;
			break;
			
		case "S":
			this.name = "Sulfur";
			this.radii = 88;
			break;
			
		case "CL":
			this.name = "Chlorine";
			this.radii = 79;
			break;
			
		case "CA":
			this.name = "Calcium";
			this.radii = 194;
			break;
			
		case "FE":
			this.name = "Iron";
			this.radii = 156;
			break;
			
		case "ZN":
			this.name = "Zinc";
			this.radii = 142;
			break;
			
		case "BR":
			this.name = "Bromine";
			this.radii = 94;
			break;
			
		case "CD":
			this.name = "cadmium";
			this.radii = 161;
			break;
			
		case "I":
			this.name = "Iodine";
			this.radii = 115;
			break;
			
		default:
			this.name = "Unknown";
			this.radii = 75;
			break;
		}

		this.radii = radii*scaleFactor;
	}


}

