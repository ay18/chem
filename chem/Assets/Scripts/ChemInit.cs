using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class ChemInit : MonoBehaviour
{
	
	public Transform AtomTransform;

	// Use this for initialization
	void Start ()
	{	
		Debug.Log ("loadChemScript called");
		loadFile ("Assets/sdf/atp.sdf");
	}
	
//	// Update is called once per frame
//	void Update () {s
//	
//	}

	private bool loadFile (string filePath)
	{
		try {

			// Variables
			string line;
			string[] parsedLine;
			int atoms = 0;
			int bonds = 0;
			int lineNum = 1;
			// molecule dimensions, min and max for x y z
			float[] molDim = new float[6];
			// game object to group atoms
			GameObject atomContainer = GameObject.Find("/AtomContainer");
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
					// molDim contains min and max for x y z
					updateDimensions(ref molDim[0], ref molDim[1], ref molDim[2], ref molDim[3], ref molDim[4], ref molDim[5], parsedLine);
					renderAtomFromLine(parsedLine, atomContainer);
				}
				lineNum++;
			}

			// Dimension updated at this point, initialize camera controller
			CameraControl camCtrl = new CameraControl(molDim);
			camCtrl.adjustCameraPosition();

			return true;
		} catch (IOException e) {
			Debug.Log (e.Message);
			return false;
		}
	}

	// lineType specifies the part of an xyz file being read in.
	// In SDF files, first three lines not important
	private bool renderAtomFromLine (string[] parsedLine, GameObject container)
	{
		Element e = new Element(parsedLine);

		// this part is kind of weird
		// atom game objects created from a transform, instead of other way around
		Transform at = Instantiate (AtomTransform, new Vector3 (e.x, e.y, e.z), Quaternion.identity) as Transform;
		GameObject atom = at.gameObject;
		atom.name = e.name;
		at.parent = container.transform;
		at.localScale = new Vector3 (e.radii, e.radii, e.radii);

		atom.GetComponent<Rigidbody> ().useGravity = false;
		switch (e.symbol) {
		case "H":
			atom.GetComponent<Renderer> ().material.color = Color.blue;
			break;
		case "C":
			atom.GetComponent<Renderer> ().material.color = Color.white;
			break;
		case "O":
			atom.GetComponent<Renderer> ().material.color = Color.red;
			break;
		case "N":
			atom.GetComponent<Renderer> ().material.color = Color.yellow;
			break;
		case "P":
			atom.GetComponent<Renderer> ().material.color = Color.grey;
			break;
		}

		return true;
	}

	// Parses a line and returns an atom coordinates struct7849
	private string[] parseLine (string line)
	{
		string[] parsedLine;
		parsedLine = line.Split (new char[0], System.StringSplitOptions.RemoveEmptyEntries);
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
