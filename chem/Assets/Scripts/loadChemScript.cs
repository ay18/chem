﻿using UnityEngine;
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
		loadFile ("Assets/sdf/caffeine.sdf");
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
			float Xmin = 0, Xmax = 0, Ymin = 0, Ymax = 0, Zmin = 0, Zmax = 0;

			StreamReader sr = new StreamReader (filePath);

			while (!sr.EndOfStream) {
				line = sr.ReadLine ();
				parsedLine = parseLine (line);

				// Line 4 contains number of atoms and bonds
				if (lineNum == 4) {
//					foreach (string s in parsedLine) {
//						Debug.Log ("parse line parsed string segment: " + s);
//					}
					atoms = int.Parse(parsedLine[0]);
					bonds = int.Parse(parsedLine[1]); // bond functionallity not implemented
				}

				// The next n=atoms lines contain coordinate information. Pass entire string into RenderAtomFromLine
				else if (lineNum > 4 && lineNum <= (4+atoms) ) {
					foreach (string s in parsedLine) {
						Debug.Log ("parse line parsed string segment: " + s);
					}
					updateDimensions(ref Xmin, ref Xmax, ref Ymin, ref Ymax, ref Zmin, ref Zmax, parsedLine);
					renderAtomFromLine(parsedLine);
				}
				lineNum++;
			}
			// Dimension updated at this point


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
		switch (e.symbol) {
		case "H":
			hAtom.GetComponent<Renderer> ().material.color = Color.blue;
			break;
		case "C":
			hAtom.GetComponent<Renderer> ().material.color = Color.white;
			break;
		case "O":
			hAtom.GetComponent<Renderer> ().material.color = Color.red;
			break;
		case "N":
			hAtom.GetComponent<Renderer> ().material.color = Color.yellow;
			break;
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

	private void updateDimensions (ref float Xmin, ref float Xmax, ref float Ymin, ref float Ymax, ref float Zmin, ref float Zmax, string[] parsedLine) 
	{
		Xmin = Mathf.Min (Xmin, float.Parse (parsedLine [0]));
		Xmax = Mathf.Max (Xmax, float.Parse (parsedLine [0]));
		Ymin = Mathf.Min (Ymin, float.Parse (parsedLine [1]));
		Ymax = Mathf.Max (Ymax, float.Parse (parsedLine [1]));
		Zmin = Mathf.Min (Zmin, float.Parse (parsedLine [2]));
		Zmax = Mathf.Max (Zmax, float.Parse (parsedLine [2]));
	}

}
