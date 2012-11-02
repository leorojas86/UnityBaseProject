using UnityEngine;
using System.Collections;

public class FollowCharacterCamera : MonoBehaviour 
{
	public Character character  = null;
	public Vector3 followOffset = Vector3.zero;

	void Start()
	{
		followOffset = transform.position - character.transform.position;
	}
	
	void Update() 
	{
		//transform.position = Vector3.SmoothDamp(transform.position, character.transform.position, ref velocity,0.05f);

		transform.position = character.transform.position + followOffset;

		transform.LookAt(character.transform);
	}
}
