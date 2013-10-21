using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {

	public static int movementSpeedFloat = Animator.StringToHash("speed");	//object movement speed
	public static int strikeAnimBool = Animator.StringToHash("strike");		//attack animation trigger
	public static int getHitAnimBool = Animator.StringToHash("takeDamage");	//hit animation trigger
	
	public static int attackState = Animator.StringToHash("Attack Layer.attack");	//attack animation state
	public static int getHitState = Animator.StringToHash("Base Layer.gethit");		//hit animation state
	
}
