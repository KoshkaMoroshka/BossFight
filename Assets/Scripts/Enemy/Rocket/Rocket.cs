using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float _damageRocket = 5f;
    private float speedRocket;
    private Transform player;
    private Transform flightPoint;
    private Vector3 positionPlayerLast;
    private bool startFollowPlayer = false;
    private bool followPlayer = true;

    // Update is called once per frame
    private void Update()
    {
        if (startFollowPlayer)
        {
            if (followPlayer)
            {
                if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out RaycastHit hit, LayerMask.GetMask("Field")))
                    positionPlayerLast = hit.point;
            }
            transform.position = Vector3.MoveTowards(transform.position, positionPlayerLast + new Vector3(0, -1f, 0), speedRocket * 2f * Time.deltaTime); 
            transform.LookAt(positionPlayerLast + new Vector3(0, -2.5f, 0));
            if (Vector3.Distance(transform.position, positionPlayerLast) < 0.01f)
            {
                Destroy(gameObject);
            }
            return;
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, flightPoint.position, speedRocket * Time.deltaTime);
            transform.LookAt(flightPoint);
            if ((flightPoint.position - transform.position).magnitude < 0.1f)
            {
                positionPlayerLast = player.position;
                startFollowPlayer = true;
                StartCoroutine(CheckUnitPosition());
            }
            return;
        }
    }

    public void SetupRocket(Transform direction, float speedRocket, Transform playerPosition)
    {
        flightPoint = direction;
        this.speedRocket = speedRocket;
        player = playerPosition;
    }

    public void SetupRocket(Transform playerPosition, float speedRocket)
    {
        player = playerPosition;
        positionPlayerLast = player.position;
        this.speedRocket = speedRocket;
        startFollowPlayer = true;
        StartCoroutine(CheckUnitPosition());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            return;
        if (collision.gameObject.TryGetComponent<PlayerHP>(out var playerHP))
        {
            playerHP.GetDamage(_damageRocket);
        }
        Destroy(gameObject);
    }

    private IEnumerator CheckUnitPosition()
    {
        yield return new WaitForSeconds(3f);
        followPlayer = false;
    }
}
