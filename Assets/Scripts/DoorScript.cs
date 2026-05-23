using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable
{
    bool isOpen = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Interact()
    {
        if (!isOpen)
        {
            animator.SetBool("isOpen", true);
            //gameObject.transform.Rotate(0, 90, 0);
            isOpen = true;
        }
        else
        {
            animator.SetBool("isOpen", false);
            //gameObject.transform.Rotate(0, -90, 0);
            isOpen = false;
        }
    }
}
