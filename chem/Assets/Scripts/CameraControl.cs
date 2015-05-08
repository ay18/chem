using UnityEngine;
using System.Collections;
using Leap;

public class CameraControl : MonoBehaviour {

	private float Xmin, Xmax, Ymin, Ymax, Zmin, Zmax;
	private Controller c;

	public CameraControl (float[] moleculeDimension) {
		c = new Controller ();
		Xmin = moleculeDimension [0];
		Xmax = moleculeDimension [1];
		Ymin = moleculeDimension [2];
		Ymax = moleculeDimension [3];
		Zmin = moleculeDimension [4];
		Zmax = moleculeDimension [5];
	}

	void trackFrame() {
		Frame currentFrame = c.Frame ();
		Frame previousFrame = c.Frame (1);
		leapHandMovement (currentFrame, previousFrame);
	}

	void leapHandMovement(Frame cf, Frame pf) {
		HandList hands = cf.Hands;
		Hand firstHand = hands [0];
		if (firstHand.IsValid) {
			Debug.Log(firstHand.Translation(pf));
		}
	}

}
