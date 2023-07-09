using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocketAttack : MonoBehaviour
{
    [SerializeField] private GameObject _rocket;
    [SerializeField] private int _countShoots = 3;
    [SerializeField] private float _delaySpawnRockets = 1.5f;
    [SerializeField] private Transform _leftSpawnRocket;
    [SerializeField] private Transform _rightSpawnRockets;

    private Enemy infoEnemy;

    private void Start()
    {
        infoEnemy = GetComponent<Enemy>();
    }

    public void AttackTwoArms()
    {
        infoEnemy.AnimatorBoss.SetBool("AttackTwoArms", true);

        //Not ready task
    }

    // Start is called before the first frame update
    private IEnumerator SpawnRockets()
    {
        yield return new WaitForSeconds(_delaySpawnRockets);
        var leftRocket = Instantiate(_rocket, _leftSpawnRocket.position, Quaternion.identity);
        leftRocket.GetComponent<Rocket>().SetupRocket(infoEnemy.GetPlayerTransform(), 5f);
        var rightRocket = Instantiate(_rocket, _rightSpawnRockets.position, Quaternion.identity);
        rightRocket.GetComponent<Rocket>().SetupRocket(infoEnemy.GetPlayerTransform(), 5f);
    }
}
