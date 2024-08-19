using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MummyCharacterMaster : CharacterMaster
{
    public MummyBody MummyBodyComp;

    //Our navMeshAgent.
    public NavMeshAgent NavMeshAgentComp;

    public NavMeshPath OurPath;

    public GameObject Target;

    public void Start()
    {
        Target = PlayerSpawner.ThePlayerRef;
        NavMeshAgentComp = GetComponent<NavMeshAgent>();
        NavMeshAgentComp.updatePosition = false;
        NavMeshAgentComp.updateRotation = false;
        NavMeshAgentComp.enabled = true;
        OurPath = new NavMeshPath();
    }

    void FixedUpdate()
    {
        primaryFireHeld = false;
        movementYAxisInput = 0;
        if(Target == null)
            Target = PlayerSpawner.ThePlayerRef;
        if (Target == null)
            return;

        //NavMeshAgentComp.SetDestination(Target.transform.position);
        //Vector3 testVector = NavMeshAgentComp.desiredVelocity;
        //testVector.Normalize();
        //Vector3 desiredDirection = transform.position - new Vector3(transform.position.x + (testVector.x * 10), transform.position.y + (testVector.y * 10), transform.position.z + (testVector.z * 10));
        //NavMeshAgentComp.nextPosition = transform.position;
        //NavMeshAgentComp.SetDestination(Target.transform.position);

        //MummyBodyComp.DontHandleCamera = true;
        //Quaternion lookRotation = Quaternion.identity;
        //if (desiredDirection != Vector3.zero && NavMeshAgentComp.path.status == NavMeshPathStatus.PathComplete)
        //    lookRotation = Quaternion.LookRotation(-desiredDirection, Vector3.up);
        //else
        //    lookRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
        //MummyBodyComp.cameraYRotation = lookRotation.eulerAngles.y;
        //movementYAxisInput = 1;

        //int navPath = 0;
        //navPath |= 1 << NavMesh.GetAreaFromName("Everything");
        //NavMesh.CalculatePath(transform.position, Target.transform.position, navPath, OurPath);
        //for (int i = 0; i < OurPath.corners.Length - 1; i++)
        //    Debug.DrawLine(OurPath.corners[i], OurPath.corners[i + 1], Color.red);

        Quaternion lookRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
        MummyBodyComp.cameraYRotation = lookRotation.eulerAngles.y;
        if (Vector3.Distance(Target.transform.position, transform.position) <= MummyBodyComp.AttackRange)
            primaryFireHeld = true;
    }
}
