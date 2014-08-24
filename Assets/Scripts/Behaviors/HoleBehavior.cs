using UnityEngine;
using System.Collections;

public class HoleBehavior : MonoBehaviour
{
		void Fill ()
		{
				GameObject filledHole = Instantiate (GameController.instance.filledHolePrefab) as GameObject;
				filledHole.transform.parent = transform.parent;
				filledHole.transform.localPosition = transform.localPosition;
		
				Destroy (gameObject);
		}
}