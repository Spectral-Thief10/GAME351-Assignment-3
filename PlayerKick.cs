using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    [Header("Tuning")]
    public float KickForce = 8f;
    public float KickRange = 1.2f;
    public float KickRadius = 0.35f;

    [Header("References")]
    public Animator animator;
    public Transform kickorigin;
    public LayerMask kickablelayers;

    int kickIndexHash;
    int kickTriggerHash;
    void Awake()
    {
        kickIndexHash = Animator.StringToHash("KickIndex");
        kickTriggerHash = Animator.StringToHash("Kick");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SPACE PRESSED!");
            int index = Random.Range(0, 3);
            animator.SetInteger(kickIndexHash, index);
            animator.SetTrigger(kickTriggerHash);

            DoKick();
        }
    }

    void DoKick()
    {
        Vector3 center = kickorigin.position + transform.forward * KickRange;

        Collider[] hits = Physics.OverlapSphere(center, KickRadius, kickablelayers);
        Debug.Log("Kick hits: " + hits.Length);
        if(hits.Length == 0) return;

        Collider best = hits[0];
        float bestDist = Vector3.Distance(transform.position, best.transform.position);

        for (int i = 1; i <hits.Length; i++)
        {
            float d = Vector3.Distance(transform.position, hits[i].transform.position);
            if (d < bestDist)
            {
                best = hits[i];
                bestDist = d;
            }
        }

        Rigidbody rb = best.attachedRigidbody;
        if (rb == null) return;

        Vector3 dir = transform.forward;
        dir.y = 0.15f;
        dir.Normalize();

        Vector3 hitPoint = best.ClosestPoint(kickorigin.position);
        rb.AddForceAtPosition(dir * KickForce, hitPoint, ForceMode.Impulse);
    }

    void OnDrawGizmosSelected()
    {
        if (kickorigin == null) return;
        Gizmos.color = Color.yellow;
        Vector3 cetner = kickorigin. position + transform.forward * KickRange;
        Gizmos.DrawWireSphere(cetner, KickRadius);
    }
}
