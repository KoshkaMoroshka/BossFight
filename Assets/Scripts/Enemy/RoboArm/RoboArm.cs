using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboArm : MonoBehaviour
{
    public int CountWeakPoints = 3;

    [SerializeField] private float _damageArm = 20f;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _startLine;
    [SerializeField] private Transform _upperPoint;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speedArm = 10f;
    [SerializeField] private GameObject _weakPoint;
    [SerializeField] private GameObject _damageZone;

    private float endY = 11f;
    private Vector3 endPoint = new Vector3(0, 0, 0);
    private LineRenderer line;
    private bool inAir = true;
    private bool armDamage = true;
    

    private List<WeakPoint> weakPoints;

    private void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        weakPoints = new List<WeakPoint>();
    }

    // Update is called once per frame
    private void Update()
    {
        line.SetPosition(0, _startLine.transform.position);
        line.SetPosition(1, transform.position);

        if (inAir)
        {
            var positionInAir = new Vector3(_player.position.x, _upperPoint.position.y, _player.position.z);
            transform.position = Vector3.MoveTowards(transform.position, positionInAir, _speedArm * Time.deltaTime);
            //transform.LookAt(_player);
            Vector3 direction = transform.position - _enemy.transform.position;

            transform.eulerAngles = new Vector3(0, direction.y, -90);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, _speedArm * 5 * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint) < 0.4f && weakPoints.Count == 0)
            {
                SpawnWeakPoints();
            }
        }
    }

    private IEnumerator PrepareAttack()
    {
        yield return new WaitForSeconds(3f);
        transform.position = _upperPoint.position;
        inAir = true;
        StartCoroutine(StartAttack());
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(5f);
        inAir = false;
        endPoint = new Vector3(_player.position.x, endY, _player.position.z);
        StartCoroutine(EndAttack());
    }

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(6f);
        _enemy.AnimatorBoss.SetBool("Attack", false);
        gameObject.active = false;
        _damageZone.active = false;
        foreach (var weakPoint in weakPoints)
        {
            Destroy(weakPoint.gameObject);
        }
        weakPoints.Clear();
    }

    private void SpawnWeakPoints()
    {
        for (int i = 0; i < CountWeakPoints; i++)
        {
            float t = Mathf.Lerp(0f, 1f, (float)i / (CountWeakPoints - 1));
            Vector3 spawnPosition = Vector3.Lerp(_startLine.position, transform.position, t);
            var obj = Instantiate(_weakPoint, spawnPosition, Quaternion.identity);
            weakPoints.Add(obj.GetComponent<WeakPoint>());
        }
        _damageZone.active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHP>(out var playerHP) && armDamage)
        {
            playerHP.GetDamage(_damageArm);
            armDamage = false;
        }
    }

    public void StartAttackArm()
    {
        _enemy.AnimatorBoss.SetBool("Attack", true);
        armDamage = true;
        gameObject.active = true;
        StartCoroutine(PrepareAttack());
    }
}
