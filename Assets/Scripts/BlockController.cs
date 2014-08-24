using UnityEngine;
using System.Collections;

public class BlockController : MoveableObject
{


		void PushTo (Vector3 location)
		{
				if (CanMoveIntoSquare (location.x, location.z)) {
						MoveToSquare (location);
				}
        
		}
}