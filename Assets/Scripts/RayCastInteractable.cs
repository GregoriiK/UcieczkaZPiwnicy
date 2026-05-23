using System;
using UnityEngine;

public class RayCastInteractable : MonoBehaviour
{
    [SerializeField] private float rayRange = 10f;

    private GameObject currentObject;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CastRay();
        if (Input.GetMouseButtonDown(0)) 
        {
            if (currentObject == null) return;
                
            IInteractable interactable = currentObject.GetComponent<IInteractable>();

            if (interactable is IInteractable)
            {
                interactable.Interact();
                Debug.Log("interacting");
            }
        }
    }

    void CastRay()
    {
        RaycastHit hit;

        bool hasHit = Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            rayRange
        );

        // Nic nie trafiono
        if (!hasHit)
        {
            ClearHighlight();
            return;
        }

        // Trafiono coœ bez tagu Interactable
        if (!hit.collider.CompareTag("Interactable"))
        {
            ClearHighlight();
            return;
        }

        GameObject hitObject = hit.collider.gameObject;

        // Jeœli patrzymy na nowy obiekt
        if (currentObject != hitObject)
        {
            ClearHighlight();

            currentObject = hitObject;

            MeshRenderer renderer = currentObject.GetComponent<MeshRenderer>();

            if (renderer != null)
            {
                foreach (Material mat in renderer.materials)
                {
                    if (mat.HasFloat("_IsActive"))
                    {
                        mat.SetInt("_IsActive", 1);
                    }
                }
            }
        }
    }

    void ClearHighlight()
    {
        if (currentObject == null)
            return;

        MeshRenderer renderer = currentObject.GetComponent<MeshRenderer>();

        if (renderer != null)
        {
            foreach (Material mat in renderer.materials)
            {
                if (mat.HasFloat("_IsActive"))
                {
                    mat.SetInt("_IsActive", 0);
                }
            }
        }

        currentObject = null;
    }
}