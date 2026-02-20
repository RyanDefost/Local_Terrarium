using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : MonoBehaviour, Interactable
{
    public void Interact()
    {
        Debug.Log("Interact");
        Destroy(this.gameObject);
    }
}
