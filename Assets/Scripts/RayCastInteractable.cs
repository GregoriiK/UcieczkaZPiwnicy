using UnityEngine;

public class RayCastInteractable : MonoBehaviour
{
    [SerializeField] float rayRange = 10;
    GameObject lookingAtObject;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        CastRay();
    }

    void CastRay()
    {
        RaycastHit hit;
        Physics.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRange);

        if (hit.collider != null )
        {
            if(lookingAtObject == null)
            {
                lookingAtObject = hit.collider.gameObject;
            }
            else
            {
                if (lookingAtObject == hit.collider.gameObject) 
                {
                    Debug.Log("the same");
                }
                else
                {

                }
            }
            if (hit.collider.CompareTag("Interactable"))
            {
                Material[] mats = hit.collider.GetComponent<MeshRenderer>().materials;
                foreach (Material mat in mats) 
                {
                    if (mat.HasFloat("_IsActive"))
                    {
                        mat.SetInt("_IsActive", 1);
                    }
                }
            }
        }
        lookingAtObject = hit.collider.gameObject;
    }
}
