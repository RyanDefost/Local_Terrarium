using UnityEngine;
using UnityEngine.LowLevelPhysics2D;

public class Interactor : MonoBehaviour
{
    [SerializeField] KeyCode interactionKey = KeyCode.Mouse0;
    private bool canInteract = true;
    private bool isInteracting;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        isInteracting = canInteract && Input.GetKey(interactionKey);
        if (!isInteracting) return;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out var hit, 10f))
        {
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            interactable?.Interact();
        }
    }
}
