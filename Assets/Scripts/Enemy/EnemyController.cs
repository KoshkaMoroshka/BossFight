using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _eyeEnemy;

    private Enemy infoEnemy;
    private RoboArm armAttack;
    private EnemyMoving moving;
    private EnemyLazer lazerAttack;
    private EnemyRocketAttack rocketsAttack;
    private SpineRocketEnemy spineRocketAttack;

    private bool readyNextAction = true;
    private int lastRandom = 1;
    private int lastLastRandom = 1;
    // Start is called before the first frame update
    void Start()
    {
        infoEnemy = GetComponent<Enemy>();
        armAttack = GetComponent<Enemy>().GetRoboArm();
        moving = GetComponent<EnemyMoving>();
        lazerAttack = GetComponent<EnemyLazer>();
        rocketsAttack = GetComponent<EnemyRocketAttack>();
        spineRocketAttack = GetComponent<SpineRocketEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (infoEnemy.IsAction)
        {
            return;
        }
        else
        {
            if (readyNextAction)
            {
                Physics.Raycast(_eyeEnemy.transform.position, (infoEnemy.GetPlayerTransform().position - _eyeEnemy.transform.position).normalized, out RaycastHit hit, 1000f);
                if (hit.collider.gameObject.TryGetComponent<PlayerHP>(out var playerHP))
                    ActionInSightEnemy();
                else
                    ActionNotSightEnemy();
                readyNextAction = false;
                StartCoroutine(DelayBeforeAction());
            }
        }
    }

    private IEnumerator DelayBeforeAction()
    {
        yield return new WaitForSeconds(12f);
        readyNextAction = true;
    }
    private void ActionInSightEnemy()
    {
        var action = Random.Range(1, 6);
        while (action == lastRandom || action == lastLastRandom)
        {
            action = Random.Range(1, 6);
        }
        lastLastRandom = lastRandom;
        lastRandom = action;

        switch (action)
        {
            case 1:
                lazerAttack.AttackLaser();
                break;
            case 2:
                rocketsAttack.AttackTwoArms();
                break;
            case 3:
                armAttack.StartAttackArm();
                break;
            case 4:
                spineRocketAttack.StartFireRockets();
                break;
            case 5:
                moving.GetNewEndPoint();
                break;
        }
    }

    private void ActionNotSightEnemy()
    {
        var action = Random.Range(1, 3);
        while (action == lastRandom)
        {
            action = Random.Range(1, 3);
        }
        lastRandom = action;
        switch (action)
        {
            case 1:
                moving.GetNewEndPoint();
                break;
            case 2:
                armAttack.StartAttackArm();
                break;
        }
    }
    public void DisableComponents()
    {
        armAttack.gameObject.active = false;
        moving.enabled = false;
        lazerAttack.enabled = false;
        rocketsAttack.enabled = false;
        spineRocketAttack.enabled = false;
        enabled = false;
    }
}
