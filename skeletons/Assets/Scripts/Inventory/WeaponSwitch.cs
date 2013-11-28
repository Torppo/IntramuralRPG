using UnityEngine;
using System.Collections;

public class WeaponSwitch : MonoBehaviour {
	public static int falchion = 0;
	public static int elementStaff = 1;
	
	public GameObject[] weapons;
	
	private int activeWeapon = 0;
	
	// Use this for initialization
	void Awake () {
		weapons[0].SetActive(true);
		for (int i = 1; i < weapons.Length; i++){
			weapons[i].SetActive(false);
		}
	}
	
	public void SelectWeapon(int weapon){
		weapons[activeWeapon].SetActive(false);
		weapons[weapon].SetActive(true);
		activeWeapon = weapon;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
