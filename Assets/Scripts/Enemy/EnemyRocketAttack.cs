using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocketAttack : MonoBehaviour
{
    [SerializeField] private GameObject _rocket;
    [SerializeField] private int _countShoots = 3;
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
        StartCoroutine(DelaySpawnRockets(2.3f));
        StartCoroutine(DelaySpawnRockets(3.3f));
        StartCoroutine(DelaySpawnRockets(4.3f));
    }

    // Start is called before the first frame update
    private IEnumerator DelaySpawnRockets(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        var leftRocket = Instantiate(_rocket, _leftSpawnRocket.position, Quaternion.identity);
        leftRocket.GetComponent<Rocket>().SetupRocket(infoEnemy.GetPlayerTransform(), 20f);
        var rightRocket = Instantiate(_rocket, _rightSpawnRockets.position, Quaternion.identity);
        rightRocket.GetComponent<Rocket>().SetupRocket(infoEnemy.GetPlayerTransform(), 20f);
        _countShoots--;
        if (_countShoots <= 0)
        {
            infoEnemy.AnimatorBoss.SetBool("AttackTwoArms", false);
        }
    }
}
