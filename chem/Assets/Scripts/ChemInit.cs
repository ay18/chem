using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading;

// Initializer and main controller
public class ChemInit : MonoBehaviour
{
	
	public Transform AtomTransform;
	float [] molDim = null;
	private CameraController camCtrl = null;
	private bool fileLoaded = false;
	private GameObject atomContainer = null;
	[SerializeField] private InputField input;
	[SerializeField] private Text uiText;

	// Use this for initialization
	void Start ()
	{	
		Debug.Log ("loadChemScript called");
		molDim = new float[6] {0,0,0,0,0,0};
		camCtrl = new CameraController(molDim);
		atomContainer = GameObject.Find("/AtomContainer");
		input.onEndEdit.AddListener (attemptLoadMolecule);
		//fileLoaded = loadFile ("Assets/Resources/sdf/atp.sdf");
	}
	
	// Update is called once per frame
	void Update () {
		if (fileLoaded) {
			camCtrl.trackFrame ();
			camCtrl.cameraOrbit ();
		}
	}

	private void attemptLoadMolecule(string s) {
		foreach (Transform child in atomContainer.transform) {
			GameObject.Destroy(child.gameObject);
		}
		Debug.Log ("submit: " + s);
		fileLoaded = loadFile ("Assets/Resources/sdf/"+s);
	}

	private bool loadFile (string filePath)
	{
		try {
			// Variables
			string line;
			string[] parsedLine;
			int atoms = 0;
			int bonds = 0;
			int lineNum = 1;
			Dictionary<string,int> atomCounter = new Dictionary<string, int>();	
			// molecule dimensions, min and max for x y z
			// game object to group atoms

//			FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader (filePath);
//			StreamReader sr = new StreamReader(fs);
			while (!sr.EndOfStream) {
				line = sr.ReadLine ();
				parsedLine = parseLine (line);

				// Line 4 contains number of atoms and bonds
				if (lineNum == 4) {
					atoms = int.Parse(parsedLine[0]);
					bonds = int.Parse(parsedLine[1]); // bond functionallity not implemented
				}

				// The next n=atoms lines contain coordinate information. Pass entire string into RenderAtomFromLine
				else if (lineNum > 4 && lineNum <= (4+atoms) ) {
					// molDim contains min and max for x y z
					Thread t1 = new Thread(()=>updateDimensions(ref molDim[0], ref molDim[1], ref molDim[2], ref molDim[3], ref molDim[4], ref molDim[5], parsedLine));
					t1.Start();
					//updateDimensions(ref molDim[0], ref molDim[1], ref molDim[2], ref molDim[3], ref molDim[4], ref molDim[5], parsedLine);
					renderAtomFromLine(parsedLine, atomContainer, atomCounter);
				}
				lineNum++;
			}

			// Get atom count from dictionary
			string updatedUIText = "";
			foreach (string key in atomCounter.Keys) {
				updatedUIText += key + " " + atomCounter[key] + "\n";
			}
			uiText.text = updatedUIText;

			// Dimension updated at this point, initialize camera controller
			camCtrl.adjustCameraPosition();

			return true;
		} catch (IOException e) {
			Debug.Log (e.Message);
			return false;
		}
	}

	// lineType specifies the part of an xyz file being read in.
	// In SDF files, first three lines not important
	private bool renderAtomFromLine (string[] parsedLine, GameObject container, Dictionary<string, int> atomCounter)
	{
		Shader atomShader;
		Element e = new Element(parsedLine);

		// this part is kind of weird
		// atom game objects created from a transform, instead of other way around
		Transform at = Instantiate (AtomTransform, new Vector3 (e.x, e.y, e.z), Quaternion.identity) as Transform;
		GameObject atom = at.gameObject;
		atom.name = e.name;
		at.parent = container.transform;
		at.localScale = new Vector3 (e.radii, e.radii, e.radii);

		if (atomCounter.ContainsKey (e.name)) {
			atomCounter[e.name]++;
		} else {
			atomCounter.Add(e.name, 1);
		}

		atom.GetComponent<Rigidbody> ().useGravity = false;
		switch (e.symbol) {
		case "H":
			atomShader = Shader.Find("Custom/hydrogenShader");
			break;
		case "C":
			atomShader = Shader.Find("Custom/carbonShader");
			break;
		case "N":
			atomShader = Shader.Find("Custom/nitrogenShader");
			break;
		case "O":
			atomShader = Shader.Find("Custom/oxygenShader");
			break;
		case "F":
			atomShader = Shader.Find("Custom/fluorineShader");
			break;
		case "NA":
			atomShader = Shader.Find("Custom/sodiumShader");
			break;
		case "MG":
			atomShader = Shader.Find("Custom/magnesiumShader");
			break;
		case "SI":
			atomShader = Shader.Find("Custom/siliconShader");
			break;
		case "P":
			atomShader = Shader.Find("Custom/phosphorousShader");
			break;
		case "S":
			atomShader = Shader.Find("Custom/sulfurShader");
			break;
		case "CL":
			atomShader = Shader.Find("Custom/chlorineShader");
			break;
		case "CA":
			atomShader = Shader.Find("Custom/calciumShader");
			break;
		case "FE":
			atomShader = Shader.Find("Custom/ironShader");
			break;
		case "ZN":
			atomShader = Shader.Find("Custom/zincShader");
			break;
		case "BR":
			atomShader = Shader.Find("Custom/bromineShader");
			break;
		case "CD":
			atomShader = Shader.Find("Custom/cadmiumShader");
			break;
		case "I":
			atomShader = Shader.Find("Custom/iodineShader");
			break;
		default:
			atomShader = Shader.Find("Custom/unknownShader");
			break;
		}
		atom.GetComponent<Renderer>().material.shader = atomShader;

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
