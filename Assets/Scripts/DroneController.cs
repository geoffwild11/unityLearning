using UnityEngine;
using System.Collections;

public class DroneController : MonoBehaviour {

	public float moveSpeed;
	public float turnSpeed;
	private Vector3 moveDirection;

	// Use this for initialization
	void Start () {
		moveDirection = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {

		System.Console.WriteLine ("I am in update");
		/*
			Since you’ll be using the drone’s current position a few 
			times in this method, you copy the position to a local variable.	
		 */
		Vector3 currentPosition = transform.position;

		/*
			Then you check to ensure the Fire1 button is currently pressed, 
			because you don’t want to calculate a new direction for the drone 
			otherwise. See the upcoming note for more information about 
			Input and Fire1.
		 */
		if (Input.GetButton ("Fire1")) {
						/*
				Using the scene’s main (and in this case, its only) Camera, 
				you convert the current mouse position to a world coordinate. 
				With an orthographic projection, the z value in the position 
				passed to ScreenToWorldPoint has no effect on the resulting 
				x and y values, so here it’s safe to pass the mouse 
				position directly.
			 */
						Vector3 moveToward = Camera.main.ScreenToWorldPoint (Input.mousePosition);

						/*
				You calculate the direction to move by subtracting the drone’s 
				current position from the target location. Because you don’t want 
				the drone changing its position along the z-axis, you set 
				moveDirection‘s z value to 0, meaning, “Move zero units along 
				the z-axis.” Calling Normalize ensures moveDirection has a length 
				of 1 (also known as “unit length”). Unit length vectors are 
				convenient because you can multiply them by a scalar value (like 
				moveSpeed) to make a vector pointing in the same direction, but 
				a certain length (like a moveSpeed-long vector pointing from the 
				drone in the direction toward the mouse cursor). 
				You’ll use this next.
			 */
						moveDirection = moveToward - currentPosition;
						moveDirection.z = 0;
						moveDirection.Normalize ();

						// Start the drone moving
						Vector3 target = moveDirection * moveSpeed + currentPosition;
						transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime);
			System.Console.WriteLine("I am Here");
		}

		float targetAngle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation =
			Quaternion.Slerp (transform.rotation,
			                 Quaternion.Euler (0, 0, targetAngle),
			                 turnSpeed * Time.deltaTime);

	}
}