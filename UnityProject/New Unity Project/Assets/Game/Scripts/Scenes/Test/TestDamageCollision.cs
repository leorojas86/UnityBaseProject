using UnityEngine;
using System.Collections;

public class TestDamageCollision : MonoBehaviour 
{
	#region Constants

	private const float DAMAGE_PER_FRAME = 0.02f;

	#endregion

	#region Variables

	private CollisionNotifier _collisionNotifier = null;

	#endregion

	#region Methods

	void Start()
	{
		_collisionNotifier = GetComponent<CollisionNotifier>();
	}

	void Update()
	{
		if(_collisionNotifier.IsColliding)
			TestNewGame.Instance.mainCharacter.StatsController.TakeDamage(DAMAGE_PER_FRAME, gameObject);
	}

	#endregion
}
