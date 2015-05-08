using UnityEngine;
using System.Collections;
using Leap;

public class CameraControl : MonoBehaviour {

	private GameObject cam;

	private float Xmin, Xmax, Ymin, Ymax, Zmin, Zmax;
	private Controller c;

	public CameraControl (float[] moleculeDimension) {
		c = new Controller ();
		cam = GameObject.Find ("/Core/Main Camera");
		Xmin = moleculeDimension [0];
		Xmax = moleculeDimension [1];
		Ymin = moleculeDimension [2];
		Ymax = moleculeDimension [3];
		Zmin = moleculeDimension [4];
		Zmax = moleculeDimension [5];
	}

	public void adjustCameraPosition() {
		// if the camera transform exists;
		if (cam.transform) {
			Debug.Log("X: "+Xmin+" "+Xmax+" Y: "+Ymin+" "+Ymax+" Z: "+Zmin+" "+Zmax);
			Vector3 newPosition = new Vector3(0,0,Zmin-10);
			cam.transform.position = newPosition;
		}
	}

	public void trackFrame() {
		Frame currentFrame = c.Frame ();
		Frame previousFrame = c.Frame (1);
		trackHandMovement (currentFrame, previousFrame);
	}

	void trackHandMovement (Frame cf, Frame pf) {
		HandList hands = cf.Hands;
		Hand firstHand = hands [0];
		if (firstHand.IsValid) {
			Debug.Log(firstHand.Translation(pf));
		}
	}

}
