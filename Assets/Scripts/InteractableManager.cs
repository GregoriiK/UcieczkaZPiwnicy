using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] Material material;
    MeshRenderer[] components;
    void Start()
    {
        components = FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None);
        foreach (var obj in components) 
        {
            if (obj.CompareTag("Interactable"))
            {
                List<Material> matList = new List<Material>();
                Material baseMat = obj.GetComponent<MeshRenderer>().material;
                matList.Add(baseMat);
                matList.Add(material);
                obj.SetMaterials(matList);
            }
        }
    }
}
