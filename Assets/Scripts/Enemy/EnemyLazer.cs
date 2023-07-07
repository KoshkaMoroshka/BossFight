using System.Collections;
using UnityEngine;

public class EnemyLazer : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _accumulationLaserTime = 3f;
    [SerializeField] private float _fireLaserTime = 4f;
    [SerializeField] private GameObject _startLazerPoint;

    private bool isAttack;
    private Enemy infoEnemy;
    private bool laserReady;

    private void Start()
    {
        infoEnemy = gameObject.GetComponent<Enemy>();
        isAttack = false;
        laserReady = false;
    }

    private void Update()
    {
        if (isAttack && infoEnemy.IsAnimationPlaying("Base Layer.AttackOneArm"))
        {
            StartCoroutine(AccumulateLaser());
            isAttack = false;
        }

        if (laserReady)
        {
            Debug.DrawLine(_startLazerPoint.transform.position, infoEnemy.GetPlayerTransform().position, Color.red);
            if (Physics.Raycast(_startLazerPoint.transform.position, infoEnemy.GetPlayerTransform().position, out RaycastHit hit, 100f))
            {
                Debug.Log(hit.transform.name);
            }
        }
    }

    private IEnumerator AccumulateLaser()
    {
        yield return new WaitForSeconds(_accumulationLaserTime);
        laserReady = true;
        StartCoroutine(EndBatteryLaser());
    }
    private IEnumerator EndBatteryLaser()
    {
        yield return new WaitForSeconds(_fireLaserTime);
        laserReady = false;
        infoEnemy.AnimatorBoss.SetBool("Attack", false);
    }

    public void AttackLaser() 
    {
        infoEnemy.AnimatorBoss.SetBool("Attack", true);
        isAttack = true;
    }
}
