using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : IInteractable
{
    bool isOpen = false;
    GameObject gameObject;
    public void OnInteract()
    {
        if (!isOpen)
        {
            this.gameObject.transform.Rotate(0, 90, 0);
        }
        else
        {
            this.gameObject.transform.Rotate(0, -90, 0);
        }
    }
}
