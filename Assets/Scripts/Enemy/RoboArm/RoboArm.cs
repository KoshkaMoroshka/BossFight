using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboArm : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _startLine;
    [SerializeField] private Transform _upperPoint;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speedArm = 10f;
    private float endY = 11f;
    private Vector3 endPoint = new Vector3(0, 0, 0);
    private LineRenderer line;
    private bool inAir = true;
    private void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
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
            transform.LookAt(_player);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, -90);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, _speedArm * 5 * Time.deltaTime);
        }
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
        yield return new WaitForSeconds(4f);
        _enemy.AnimatorBoss.SetBool("Attack", false);
        gameObject.active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.TryGetComponent(out PlayerActions player))
        //{

        //}
    }

    public void StartAttackArm()
    {
        _enemy.AnimatorBoss.SetBool("Attack", true);
        transform.position = _upperPoint.position;
        inAir = true;
        gameObject.active = true;
        StartCoroutine(StartAttack());
    }
}
