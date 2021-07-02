using UnityEngine;
using UnityEngine.AI;

namespace ASOIAF {
    public class UnitBehaviour : MonoBehaviour {
        public static GameObject AgentHolder { get; private set; }
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;

        private void Start() {
            AgentHolder ??= new GameObject("AgentHolder");
            agent.transform.parent = AgentHolder.transform;
            agent.speed += Random.Range(-1f, 1f);
            agent.updateRotation = false;
        }

        private void Update() {
            if(!isDead) {
                //if(Mathf.Approximately(agent.velocity.magnitude, 0f)) {
                //    agent.velocity = Vector3.zero;
                //}
                agent.transform.forward = transform.forward;
                agent.SetDestination(transform.position);
                Debug.DrawLine(agent.transform.position, agent.destination, Color.green);
                Vector3 direction = agent.destination - agent.transform.position;
                Vector3 localDirection = transform.worldToLocalMatrix.MultiplyPoint(agent.destination)
                    - transform.worldToLocalMatrix.MultiplyPoint(agent.transform.position);
                float forward = localDirection.normalized.z;
                float right = localDirection.normalized.x;
                bool isMoving = direction.magnitude >= agent.stoppingDistance;
                agent.isStopped = !isMoving;
                // Debug.Log($"World Direction :{direction} Local Directrion :{localDirection}");
                animator.SetBool("IsMoving", isMoving);
                animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
                animator.SetFloat("Forward", forward);
                animator.SetFloat("Right", right);

                //bool isMoving = Vector3.Distance(transform.position, agent.destination) >= agent.stoppingDistance;
                //Debug.Log(isMoving);
                //agent.SetDestination(transform.position);

                //if(isMoving) {
                //    animator.SetFloat("Forward", -direction.z);
                //    animator.SetFloat("Right", -direction.x);
                //    if(!wasMoving) {
                //        animator.SetBool("IsMoving", isMoving);
                //    }
                //} else {
                //    if(wasMoving) {
                //        animator.SetBool("IsMoving", isMoving);
                //        waitingTime = Random.Range(minWaitingTime, maxWaitingTime);
                //    }
                //}

                //wasMoving = isMoving;
            }
        }

        public void ToggleAnimatorBool(string boolName) {
            animator.SetBool(boolName, !animator.GetBool(boolName));
        }

        public void Die() {
            isDead = true;
            animator.SetTrigger("Die");
            int variant = new System.Random().NextDouble() >= 0.5 ? 1 : 0;
            animator.SetFloat("DeathVariant", variant);
            agent.isStopped = isDead;
            agent.enabled = false;
        }

        private bool isDead = false, wasMoving;
        private float waitingTimeSpent, waitingTime;
    }
}