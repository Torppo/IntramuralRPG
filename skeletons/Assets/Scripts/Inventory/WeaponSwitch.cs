using UnityEngine;
using System.Collections;

/*
 * Class that allows the player to switch weapons
 */
public class WeaponSwitch : MonoBehaviour {
	public static readonly int falchion = 0;	//Index of the starting sword
	public static readonly int elementStaff = 1;	//Index of the fireball staff
	
	public GameObject[] weapons;	//An array containing the gameobjects representing the different weapons
	
	private int activeWeapon = 0;	//Index of the currently equipped weapon
	
	// Use this for initialization
	void Awake () {
		
		for (int i = 0; i < weapons.Length; i++){
			weapons[i].SetActive(false);
		}
		weapons[activeWeapon].SetActive(true);
	}
	
	/*
	 * Equips a weapon. Valid parameters are static int constants in this class.
	 */
	public void SelectWeapon(int weapon){
		weapons[activeWeapon].SetActive(false);
		weapons[weapon].SetActive(true);
		activeWeapon = weapon;
	}
	
	public void SaveData(ISaveService sc){
		sc.SaveInt(this.gameObject, "equipped_weapon", activeWeapon);
	}
	
	public void LoadData(ISaveService sc){
		SelectWeapon(sc.LoadInt(this.gameObject, "equipped_weapon"));
	}
}
