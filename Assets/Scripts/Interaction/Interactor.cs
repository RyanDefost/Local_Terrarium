using System.Threading;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] float _interactionDistance = 10f;
    [SerializeField] KeyCode _interactionKey = KeyCode.Mouse0;
    [SerializeField] LayerMask interactionMask;
    public bool CanInteract { get; set; }
    public bool IsInteracting { get; private set; }

    private void OnEnable() => CanInteract = true;
    private void OnDisable() => CanInteract = false;

    private void Update()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        IsInteracting = CanInteract && Input.GetKey(_interactionKey);
        if (!IsInteracting) return;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out var hit, _interactionDistance, interactionMask))
        {
            print(hit.collider.gameObject);
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            interactable?.Interact();
        }
    }
}
