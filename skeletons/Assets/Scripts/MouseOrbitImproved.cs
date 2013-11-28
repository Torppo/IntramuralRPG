using UnityEngine;
using System.Collections;
 
[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour {
 
    public Transform target;
	public float defaultDistance = 5.0f;
    private float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
 
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
 
    public float distanceMin = .5f;
    public float distanceMax = 15f;
	
	public Vector3 targetLookAtOffset;
	public float damping = 5.0f;
 
    float x = 0.0f;
    float y = 0.0f;
	
	public float mouseSensitivity = 4.0f;
	
 
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
	        x += Input.GetAxis("Mouse X") * xSpeed * distance * mouseSensitivity * Time.deltaTime;
	        y -= Input.GetAxis("Mouse Y") * ySpeed * mouseSensitivity * Time.deltaTime;
	 
	        y = ClampAngle(y, yMinLimit, yMaxLimit);
	 
	        Quaternion rotation = Quaternion.Euler(y, x, 0);
			
			transform.rotation = rotation;
	 
	        defaultDistance = Mathf.Clamp(defaultDistance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
				
			Vector3 targetPosition = rotation * new Vector3(0.0f, 0.0f, -defaultDistance) + target.position;
			targetPosition += rotation * targetLookAtOffset;
				
	        RaycastHit hit;
			
			Vector3[] origins = new Vector3[3];
			origins[0] = targetPosition;
			origins[1] = Camera.main.ScreenToWorldPoint(new Vector3(0,0, -defaultDistance));
			origins[2] = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0, -defaultDistance));
			//origins[3] = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height, -defaultDistance));
			//origins[4] = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height, -defaultDistance));
			
			distance = defaultDistance;
			
			for (int i = 0; i < origins.Length; i++){
		        if (Physics.Linecast (target.position, origins[i], out hit) && hit.transform.gameObject.layer != 8) {
		            if (hit.distance < distance) distance = hit.distance;
		        }
			}
			
			if (distance < distanceMin) distance = distanceMin;
			
	        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
	        Vector3 position = rotation * negDistance + target.position;
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