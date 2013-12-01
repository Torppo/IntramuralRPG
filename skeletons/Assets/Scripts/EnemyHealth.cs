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
     
	/*
	 * Draws health bars over enemies, heavily modified from a script by A.Roberts
	 */
    public class EnemyHealth : MonoBehaviour {
     
    public float initialGreenLength = 0.5f;	//Initial health bar length
    public GameObject greenBar;	//Health bar
    public float curHealth;	//Current character health
    public float maxHealth;	//Maximum character health
	public float barOffset = 5f;	//MAGIC NUMBER THAT MAKES THINGS WORK
	     
    Vector3 greenPos;	//Position of the health bar relative to the character
     
    public CharacterStats healthScript;	//Character health script
     
     
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
     
	    //Move the health bar to the left make it look like it's only becoming shorter from one end
		Vector3 newpos = Vector3.zero;
		newpos.x = (1 - curHealth/maxHealth) * barOffset;
		greenBar.transform.localPosition = newpos;
			    
	     
	    curHealth = healthScript.health;
	    maxHealth = healthScript.maxHealth;
	     
	    // C# conversion of Professor Snake's awesome code from Unity forum's
	    // http://answers.unity3d.com/questions/403008/help-with-enemy-health-bars.html
	    
		//Scale the health bar to reflect health
	    Vector3 greenScale = greenBar.transform.localScale;
	    greenScale.x = (curHealth/maxHealth);
	    greenBar.transform.localScale = greenScale;
	     
	    //keeps bar facing camera
	    transform.LookAt(Camera.main.transform);
	    
		//Disable health bar when the character is dead
		if (!healthScript.isAlive) this.gameObject.SetActive(false);
	    }
    }