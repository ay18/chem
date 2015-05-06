using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;



public class loadChemScript : MonoBehaviour {

	public struct atomCoordinates {
		public string symbol;
		public double x, y, z;
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("loadChemScript called");
		Load ("Assets/xyz/caffeine.xyz");
	}
	
//	// Update is called once per frame
//	void Update () {s
//	
//	}

	private bool Load(string filePath) {
		try {
			string line;
			int lineType = 0;

			StreamReader sr = new StreamReader(filePath);

			while (!sr.EndOfStream) {
				line = sr.ReadLine();
				Debug.Log ("line: " + line + "\nline type: " + lineType);
				RenderAtomFromLine(line, lineType);
				if(lineType<2) lineType++;
			}

			return true;
		}

		catch (IOException e)
		{
			Debug.Log(e.Message);
			return false;
		}
	}

	// lineType specifies the part of an xyz file being read in.
	// 0, the first line, specifies atom count
	// 1, the second line, specifies the chemical name
	// 2, the third line and beyond, specifies each atom's coordinates
	private bool RenderAtomFromLine(string line, int lineType) {
		switch (lineType) {
		case 0:
			Debug.Log ("RenderAtom lineType "+lineType);

			break;

		case 1:
			Debug.Log ("RenderAtom lineType "+lineType);

			break;

		case 2:
			Debug.Log ("RenderAtom lineType "+lineType);
			atomCoordinates coords = createAtomCoordinates(line);
			Debug.Log ("Atom symbol: " + coords.symbol + " Atom coordinates: " + coords.x + " " + coords.y + " " + coords.z);
			break;

		//no default
		}

		return true;
	}

	// Parses a line and returns an atom coordinates struct
	private atomCoordinates createAtomCoordinates(string line) {
		string[] parsedCoordinates;
		parsedCoordinates = line.Split(new char[0], System.StringSplitOptions.RemoveEmptyEntries);
//		foreach (string s in parsedCoordinates) {
//			Debug.Log ("createAtomCoordinates parsed string segment: " + s);
//		}
		if (parsedCoordinates.Length != 4) {
			throw new System.InvalidOperationException("xyz input file error. coordinates should be (symbol, x, y, z).");
		}
		return new atomCoordinates () {symbol = parsedCoordinates[0], x = double.Parse(parsedCoordinates[1]), y = double.Parse(parsedCoordinates[2]), z = double.Parse(parsedCoordinates[3])};
	}
}
