using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineRocketEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _rocket;
    [SerializeField] private List<Transform> _spawnRockets; // left 0; right 1
    [SerializeField] private List<Transform> _firstPointsRockets;
    [SerializeField] private List<Transform> _secondPointsRockets;
    [SerializeField] private List<Transform> _thirdPointsRockets;
    [SerializeField] private float _speedRockets = 5f;
    private int waveRocket = 3;

    private Enemy infoEnemy;

    private void Start()
    {
        infoEnemy = GetComponent<Enemy>();
    }

    private IEnumerator SpawnRocket(float spawnDelayRockets, Transform leftPoint, Transform rightPoint)
    {
        yield return new WaitForSeconds(spawnDelayRockets);
        var leftRocket = Instantiate(_rocket, _spawnRockets[0].position, Quaternion.identity);
        leftRocket.GetComponent<Rocket>().SetupRocket(leftPoint, _speedRockets, infoEnemy.GetPlayerTransform()); ;
        var rightRocket = Instantiate(_rocket, _spawnRockets[1].position, Quaternion.identity);
        rightRocket.GetComponent<Rocket>().SetupRocket(rightPoint, _speedRockets, infoEnemy.GetPlayerTransform());
        waveRocket -= 1;
        if (waveRocket <= 0)
            infoEnemy.IsAction = false;
    }

    public void StartFireRockets()
    {
        infoEnemy.IsAction = true;
        StartCoroutine(SpawnRocket(0, _firstPointsRockets[0].transform, _firstPointsRockets[1].transform));
        StartCoroutine(SpawnRocket(3, _secondPointsRockets[0].transform, _secondPointsRockets[1].transform));
        StartCoroutine(SpawnRocket(7, _thirdPointsRockets[0].transform, _thirdPointsRockets[1].transform));
    }
}
