using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    public Transform destination;
    public Waypoint waypoint;
    public bool seenTarget = false;
    private float sightFov = 110.0f;
    public Vector3 lastSeenPosition;
    GameObject target;
    SphereCollider collider;
    public StateMachine stateMachine = new StateMachine();
    public GameObject shot;
    public Transform shotTransform;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    void Start()
    {
        //consults unity's api on navmesh it was really helpful to you
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destination.position;
        collider = GetComponent<SphereCollider>();
        target = GameObject.FindWithTag("Player");
        stateMachine.ChangeState(new State_Patrol(this));

    }

    void FixedUpdate()
    {
      //Update state enemy is in
        stateMachine.Update();

    }

    private void OnTriggerStay(Collider other)
    {
        //is it the player?
        if (other.gameObject == target)
        {
            // angle between us and the player
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            //reset whether we've seen he player
            seenTarget = false;

            RaycastHit hit;

            //is it less than our field of view
            if (angle < sightFov * 0.5f)
            {
                //if the raycast hits the player we know
                //there is nothing in the way
                //adding transform.up raises up from the floor by 1 unit
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, collider.radius))
                {
                    if (hit.collider.gameObject == target)
                    {
                        //flag that we've seen the player
                        //remember their position
                        seenTarget = true;
                        if (seenTarget == true)
                        {
                            Debug.Log("Seen Target");
                            lastSeenPosition = target.transform.position;
                        }

                    }
                }
            }


        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (collider != null)
        {
            Gizmos.DrawWireSphere(transform.position, collider.radius);

            if (seenTarget)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, lastSeenPosition);
            }

            if (lastSeenPosition != Vector3.zero)
            {
                //draw a small sphere at where you last saw the player
                Gizmos.DrawWireSphere(lastSeenPosition, 1.1f);
            }

            Vector3 rightPepripheral;
            rightPepripheral = (Quaternion.AngleAxis(sightFov * 0.5f, Vector3.up) * transform.forward * collider.radius);

            Vector3 leftPepripheral;
            leftPepripheral = (Quaternion.AngleAxis(-sightFov * 0.5f, Vector3.up) * transform.forward * collider.radius);

            //draw lines for the left and right edges of the field of view
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, rightPepripheral);
            Gizmos.DrawRay(transform.position, leftPepripheral);
        }
    }

    //Fire when player is spotted
    public void ShootAtPlayer()
    {
        transform.LookAt(lastSeenPosition);

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotTransform.position, shotTransform.rotation);
            Debug.Log("shooting player");
        }

    }


}
