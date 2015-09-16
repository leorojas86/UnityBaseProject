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
        public Type type;
        public State state;

        public CollisionData(Collision collision, Type type, State state)
        {
            this.collision = collision;
            this.type      = type;
            this.state     = state;
        }
    }

    #endregion

    #region Variables

    public System.Action<CollisionData> OnCollision = null;

    #endregion

    #region Events

    void OnCollisionEnter(Collision collision)
    {
        NotifyOnCollision(collision, Type.Collision, State.Enter);
    }

    void OnCollisionStay(Collision collision)
    {
        NotifyOnCollision(collision, Type.Collision, State.Stay);
    }

    void OnCollisionExit(Collision collision)
    {
        NotifyOnCollision(collision, Type.Collision, State.Exit);
    }

    void OnTriggerEnter(Collision collision)
    {
        NotifyOnCollision(collision, Type.Trigger, State.Enter);
    }

    void OnTriggerStay(Collision collision)
    {
        NotifyOnCollision(collision, Type.Trigger, State.Stay);
    }

    void OnTriggerExit(Collision collision)
    {
        NotifyOnCollision(collision, Type.Trigger, State.Exit);
    }

    private void NotifyOnCollision(Collision collision, Type type, State state)
    {
        if(OnCollision != null)
        {
            CollisionData data = new CollisionData(collision, type, state);
            OnCollision(data);
        }
    }

    #endregion
}
