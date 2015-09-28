using UnityEngine;
using System.Collections;

public class CollisionNotifier : MonoBehaviour
{
    #region Enums

    public enum State
    {
        Enter,
        Stay,
        Exit
    }

    public enum Type
    {
        Trigger,
        Collision
    }

    #endregion

    #region Structs

    public struct CollisionData
    {
        public Collision collision;
        public Collider collider;
        public Type type;
        public State state;

        public CollisionData(Collision collision, State state)
        {
            this.collision = collision;
            this.type      = Type.Collision;
            this.state     = state;
            this.collider  = null;
        }

        public CollisionData(Collider collider, State state)
        {
            this.collider   = collider;
            this.type       = Type.Trigger;
            this.state      = state;
            this.collision  = null;
        }
    }

    #endregion

    #region Variables

    public System.Action<CollisionData> OnCollision = null;

	private int _stayingCollisionsCount = 0;

    #endregion

	#region Properties

	public bool IsColliding
	{
		get { return _stayingCollisionsCount > 0;}
	}

	#endregion

    #region Events

    void OnCollisionEnter(Collision collision)
    {
		_stayingCollisionsCount++;

        NotifyOnCollision(collision, Type.Collision, State.Enter);
    }

    void OnCollisionStay(Collision collision)
    {
        NotifyOnCollision(collision, Type.Collision, State.Stay);
    }

    void OnCollisionExit(Collision collision)
    {
		_stayingCollisionsCount--;

        NotifyOnCollision(collision, Type.Collision, State.Exit);
    }

    void OnTriggerEnter(Collider collision)
    {
		_stayingCollisionsCount++;

        NotifyOnCollision(collision, Type.Trigger, State.Enter);
    }

    void OnTriggerStay(Collider collision)
    {
        NotifyOnCollision(collision, Type.Trigger, State.Stay);
    }

    void OnTriggerExit(Collider collision)
    {
		_stayingCollisionsCount--;

        NotifyOnCollision(collision, Type.Trigger, State.Exit);
    }

    private void NotifyOnCollision(Collision collision, Type type, State state)
    {
        if(OnCollision != null)
        {
            CollisionData data = new CollisionData(collision, state);
            OnCollision(data);
        }
    }

    private void NotifyOnCollision(Collider collider, Type type, State state)
    {
		//Debug.Log("NotifyOnCollision collider = '" + collider.name + "' Notifier = '" + transform.name + "' type = '" + type + "' state = '" + state + "'");

        if (OnCollision != null)
        {
            CollisionData data = new CollisionData(collider, state);
            OnCollision(data);
        }
    }

    #endregion
}
