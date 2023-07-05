using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private string _nameInteractable;
    public string Name
    {
        get { return _nameInteractable; }

        protected set { _nameInteractable = value; }
    }

    public void BaseInteract(GameObject obj)
    {
        Interact(obj);
    }

    protected virtual void Interact(GameObject obj) { }
}
