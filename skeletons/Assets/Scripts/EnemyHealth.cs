    using UnityEngine;
    using System.Collections;
     
    /// <summary>
    ///
    /// Created by A.Roberts, June 9, 2013
    ///
    /// Attach to empty gameObject that is parent to both the green
    /// and red planes. Drag green bar into the "greenBar" public
    /// field.
    ///
    /// </summary>
     
    public class EnemyHealth : MonoBehaviour {
     
    public float initialGreenLength = 0.5f;
    public GameObject greenBar;
    public float curHealth;
    public float maxHealth;
	public float barOffset = 5f;
	     
    Vector3 greenPos;
     
    public CharacterStats healthScript;
     
     
    void Awake ()
    {
     
	    //loads enemy health value from healthScript
	    healthScript = transform.parent.gameObject.GetComponent<CharacterStats>();
	    curHealth = healthScript.health;
	    maxHealth = healthScript.maxHealth;
	     
	    //stores two health values, will come in later
    }
     
     
    void Update ()
    {
     
	    
		Vector3 newpos = Vector3.zero;
		newpos.x = (1 - curHealth/maxHealth) * barOffset;
		greenBar.transform.localPosition = newpos;
			    
	     
	    curHealth = healthScript.health;
	    maxHealth = healthScript.maxHealth;
	     
	    // C# conversion of Professor Snake's awesome code from Unity forum's
	    // http://answers.unity3d.com/questions/403008/help-with-enemy-health-bars.html
	     
	    Vector3 greenScale = greenBar.transform.localScale;
	    greenScale.x = (curHealth/maxHealth);
	    greenBar.transform.localScale = greenScale;
	     
	    //keeps bar facing camera
	    transform.LookAt(Camera.main.transform);
	    
		if (!healthScript.isAlive) this.gameObject.SetActive(false);
	    }
    }