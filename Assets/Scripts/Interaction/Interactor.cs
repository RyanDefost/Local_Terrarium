using System.Threading;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] readonly KeyCode _interactionKey = KeyCode.Mouse0;
    [SerializeField] readonly float _interactionDistance = 10f;
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
        if (Physics.Raycast(ray, out var hit, _interactionDistance))
        {
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            interactable?.Interact();
        }
    }
}
