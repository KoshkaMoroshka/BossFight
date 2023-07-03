using UnityEngine;

public class InteractablePistol : Interactable
{
    private void Start()
    {
        Name = "pistol";
    }
    protected override void Interact()
    {
        Debug.Log("pistol");
    }
}
