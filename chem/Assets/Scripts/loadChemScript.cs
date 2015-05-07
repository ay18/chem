using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class loadChemScript : MonoBehaviour
{
	
	public Transform Atom;
	
	// Use this for initialization
	void Start ()
	{	
		Debug.Log ("loadChemScript called");
		loadFile ("Assets/sdf/aspirin.sdf");
	}
	
//	// Update is called once per frame
//	void Update () {s
//	
//	}

	private bool loadFile (string filePath)
	{
		try {
			string line;
			string[] parsedLine;
			int atoms = 0;
			int bonds = 0;
			int lineNum = 1;

			StreamReader sr = new StreamReader (filePath);

			while (!sr.EndOfStream) {
				line = sr.ReadLine ();
//				Debug.Log ("line: " + line + "\nline type: " + lineNum);
				parsedLine = parseLine (line);

				// Line 4 contains number of atoms and bonds
				if (lineNum == 4) {
					foreach (string s in parsedLine) {
						Debug.Log ("parse line parsed string segment: " + s);
					}
					atoms = int.Parse(parsedLine[0]);
					bonds = int.Parse(parsedLine[1]); // bond functionallity not implemented
				}

				// The next n=atoms lines contain coordinate information. Pass entire string into RenderAtomFromLine
				else if (lineNum > 4 && lineNum <= (4+atoms) ) {
					Debug.Log ("line num " + lineNum);
					renderAtomFromLine(parsedLine);
				}

				lineNum++;
			}

			return true;
		} catch (IOException e) {
			Debug.Log (e.Message);
			return false;
		}
	}

	// lineType specifies the part of an xyz file being read in.
	// In SDF files, first three lines not important
	private bool renderAtomFromLine (string[] parsedLine)
	{
		Element e = new Element(parsedLine);

		Transform h = Instantiate (Atom, new Vector3 (e.x, e.y, e.z), Quaternion.identity) as Transform;
		GameObject hAtom = h.gameObject;
		hAtom.GetComponent<Rigidbody> ().useGravity = false;
		if (e.symbol == "H") {
			hAtom.GetComponent<Renderer> ().material.color = Color.blue;
		} else if (e.symbol == "C") {
			hAtom.GetComponent<Renderer> ().material.color = Color.white;
		} else if (e.symbol == "O") {	
			hAtom.GetComponent<Renderer> ().material.color = Color.red;
		} else if (e.symbol == "N") {
			hAtom.GetComponent<Renderer> ().material.color = Color.yellow;
		}

		return true;
	}

	// Parses a line and returns an atom coordinates struct7849
	private string[] parseLine (string line)
	{
		string[] parsedLine;
		parsedLine = line.Split (new char[0], System.StringSplitOptions.RemoveEmptyEntries);
		//				foreach (string s in parsedLine) {
//					Debug.Log ("parse line parsed string segment: " + s);
//				}
		return parsedLine;
	}
}
