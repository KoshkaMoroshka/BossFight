using UnityEngine;

public class InteractablePistol : Interactable
{
    private void Start()
    {
        Name = "pistol";
    }
    protected override void Interact(GameObject obj)
    {
        obj.GetComponent<PlayerInventory>().GetWeaponInArms(Name);
        Destroy(gameObject);
    }
}
