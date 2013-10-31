using UnityEngine;
using System.Collections;

public interface ISaveService {

	void SaveString(GameObject obj, string name, string content);
	
	string LoadString(GameObject obj, string name);
	
	void SaveFloat(GameObject obj, string name, float content);
	
	float LoadFloat(GameObject obj, string name);
	
	void SaveInt(GameObject obj, string name, int content);
	
	int LoadInt(GameObject obj, string name);
}
