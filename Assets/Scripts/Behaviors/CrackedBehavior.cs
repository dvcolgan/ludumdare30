using UnityEngine;
using System.Collections;

public class CrackedBehavior : MonoBehaviour
{
		void StepOff ()
		{
				GameObject hole = Instantiate (GameController.instance.holePrefab) as GameObject;
				hole.transform.parent = transform.parent;
				hole.transform.localPosition = transform.localPosition;

				Destroy (gameObject);
		}
}
