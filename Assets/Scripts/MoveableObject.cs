using UnityEngine;
using System.Collections;

public class MoveableObject : MonoBehaviour
{
	
		protected GameObject otherEntities;

		protected void Start ()
		{
				otherEntities = (GameObject)this.transform.parent.gameObject;
		}

		int pageWidth = 10;
		int pageHeight = 12;

		public void MoveToSquare (Vector3 location)
		{
				Vector3 pos = transform.position;
				pos.x = location.x;
				pos.z = location.z;
				transform.position = pos;
		}
    
		public bool CanMoveIntoSquare (float x, float z)
		{
				// Check that there are no obstacles in the way
				foreach (Transform child in otherEntities.transform) {
						GameObject obstacle = child.gameObject;
						if (obstacle.transform.position.x == x && obstacle.transform.position.z == z) {
								return false;
						}
				}
		
				// Check that we are not off the page
				if (x > pageWidth - 1 || x < 0 || z > pageHeight - 1 || z < 0) {
						return false;
				}
		
				return true;
		}
}
