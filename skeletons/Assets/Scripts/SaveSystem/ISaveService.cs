using UnityEngine;
using System.Collections;

/*
 * An interface providing methods that allow the saving and loading of internal state.
 * For in-depth information on the operation of this interface, see the Save System Manual.
 */
public interface ISaveService {
	
	/*
	 * Saves the string [content] to the disk with a key composed of [obj] and [name]
	 */
	void SaveString(GameObject obj, string name, string content);
	/*
	 * Loads a string from the disk, identified by a key composed of [obj] and [name]
	 */
	string LoadString(GameObject obj, string name);
	// Same as above, but saves a float instead
	void SaveFloat(GameObject obj, string name, float content);
	// Same as above, but loads a float instead
	float LoadFloat(GameObject obj, string name);
	// Same as above, but saves an int instead
	void SaveInt(GameObject obj, string name, int content);
	// Same as above, but loads an int instead
	int LoadInt(GameObject obj, string name);
}
