using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float speedRocket;
    private Transform player;
    private Transform flightPoint;
    private Vector3 directionFlight;
    private Vector3 directionPlayer;
    private Vector3 positionPlayerLast;
    private bool startFollowPlayer = false;

    // Update is called once per frame
    private void Update()
    {
        if (startFollowPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionPlayerLast + new Vector3(0, -2.5f, 0), speedRocket * 2f * Time.deltaTime); 
            transform.LookAt(positionPlayerLast);
            return;
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, flightPoint.position, speedRocket * Time.deltaTime);
            transform.LookAt(flightPoint);
            if ((flightPoint.position - transform.position).magnitude < 0.1f)
            {
                positionPlayerLast = player.position;
                startFollowPlayer = true;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out Enemy enemy) || !collision.gameObject.TryGetComponent(out Rocket rocket))
            Destroy(gameObject);
    }

    private IEnumerator CheckUnitPosition()
    {
        yield return new WaitForSeconds(3f);
        //followPlayer = false;
    }
}
