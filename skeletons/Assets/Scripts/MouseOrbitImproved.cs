using UnityEngine;
using System.Collections;
 
[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
/*
 * Third person mouse look camera controls.
 * Heavily modified version of the script by Veli Vainio at http://wiki.unity3d.com/index.php/MouseOrbitImproved
 */
public class MouseOrbitImproved : MonoBehaviour {
 
    public Transform target;
	public float defaultDistance = 5.0f; //default camera distance at the start of the game
    private float distance = 5.0f;	//Current distance from the followed object
    public float xSpeed = 120.0f;	//horizontal camera movement speed
    public float ySpeed = 120.0f;	//vertical camera movement speed
 
    public float yMinLimit = -20f;	//Minimum camera height (degrees)
    public float yMaxLimit = 80f;	//Maximum camera height (degrees)
 
    public float distanceMin = 0.5f;	//Minimum distance from the target
    public float distanceMax = 15f;	//Maximum distance from the target
	
	public Vector3 targetLookAtOffset;	//Camera offset - offsets camera position after all other calculations
	public float damping = 5.0f;	//Movement speed damping
 
    float x = 0.0f;
    float y = 0.0f;
	
	public float mouseSensitivity = 4.0f;	//Overall camera movement speed
	
 
	// Use this for initialization
	void Awake () {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
 
        // Make the rigid body not change rotation
        if (rigidbody)
            rigidbody.freezeRotation = true;
	}
 
    void LateUpdate () {
	    if (target) {
			//calculate new rotation
	        x += Input.GetAxis("Mouse X") * xSpeed * distance * mouseSensitivity * Time.deltaTime;
	        y -= Input.GetAxis("Mouse Y") * ySpeed * mouseSensitivity * Time.deltaTime;
	 
	        y = ClampAngle(y, yMinLimit, yMaxLimit);
	 
	        Quaternion rotation = Quaternion.Euler(y, x, 0);
			
			transform.rotation = rotation;
	 		
			//Scroll wheel zoom
	        defaultDistance = Mathf.Clamp(defaultDistance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
			
			//Ideal target position
			Vector3 targetPosition = rotation * new Vector3(0.0f, 0.0f, -defaultDistance) + target.position;
			targetPosition += rotation * targetLookAtOffset;
				
	        RaycastHit hit;
			
			//Check that the target is visible from the new position
			Vector3[] origins = new Vector3[3];
			//ideal position
			origins[0] = targetPosition;
			//top left corner
			origins[1] = Camera.main.ScreenToWorldPoint(new Vector3(0,0, -defaultDistance));
			origins[1] += rotation * targetLookAtOffset;
			//top right corners
			origins[2] = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0, -defaultDistance));
			origins[2] += rotation * targetLookAtOffset;
			
			//bottom corners disabled to allow better behavior when looking up
			//origins[3] = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height, -defaultDistance));
			//origins[4] = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height, -defaultDistance));
			
			distance = defaultDistance;
			
			//get the furthest point where the target is visible from all angles
			for (int i = 0; i < origins.Length; i++){
		        if (Physics.Linecast (target.position, origins[i], out hit) && hit.transform.gameObject.layer != 8/*ignore enemies & other dynamic objects*/) {
		            if (hit.distance < distance) distance = hit.distance;
		        }
			}
			//If too close, zoom out to prevent clipping through player, even if we clip through a wall instead
			if (distance < distanceMin) distance = distanceMin;
			
	        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			//Set camera position behind target
	        Vector3 position = rotation * negDistance + target.position;
			//offset camera
			position += rotation * targetLookAtOffset;
	 
	        
	        transform.position = position;
	 
	    }
 
}
 
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
 
 
}