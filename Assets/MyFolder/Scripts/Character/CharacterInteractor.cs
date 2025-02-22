using UnityEngine;

public class CharacterInteractor : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 1.5f;

    public void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _interactionDistance);
        foreach (Collider2D collider in colliders)
        {
            if(collider.GetComponent<IInteractable>() != null)
            {
                collider.GetComponent<IInteractable>().Interract();
            }

            if(collider.GetComponent<Movable>() != null)
            {
                Rigidbody2D movable = collider.GetComponent<Rigidbody2D>();
                HingeJoint2D hingeJoint = GetComponent<HingeJoint2D>();
                if (hingeJoint.connectedBody == null)
                {
                    hingeJoint.enabled = true;
                    hingeJoint.connectedBody = movable.GetComponent<Rigidbody2D>();
                }
                else
                {
                    hingeJoint.connectedBody = null;
                    hingeJoint.enabled = false;
                }
            }
        }
    }
}
