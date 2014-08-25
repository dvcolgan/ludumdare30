using UnityEngine;
using System.Collections;

public class GateBehavior : MonoBehaviour
{

		void Open ()
		{
				Destroy (GetComponent<CollidableBehavior> ());
				iTween.MoveBy (gameObject, iTween.Hash ("z", -0.8f, "easeType", "easeOut", "time", .1f));
		}

	
		void Close ()
		{
				Destroy (GetComponent<CollidableBehavior> ());
				iTween.MoveBy (gameObject, iTween.Hash ("z", 0.8f, "easeType", "easeOut", "time", .1f));
				gameObject.AddComponent ("CollidableBehavior");
		}
}
