using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        _player.GetComponent<PlayerInventory>().SetupPlayersWeapon();
    }
}
