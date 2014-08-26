using UnityEngine;
using System.Collections;

public class GateBehavior : MonoBehaviour
{

		void Open ()
		{
				Destroy (GetComponent<CollidableBehavior> ());
				Vector3 pos = transform.position;
				pos.y -= 0.8f;
				transform.position = pos;
		}

	
		void Close ()
		{
				Destroy (GetComponent<CollidableBehavior> ());
				Vector3 pos = transform.position;
				pos.y += 0.8f;
				transform.position = pos;
				gameObject.AddComponent ("CollidableBehavior");
		}
}
