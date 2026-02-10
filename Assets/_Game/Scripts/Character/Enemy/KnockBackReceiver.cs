using FSMState.Character;
using UnityEngine;

public class KnockBackReceiver : MonoBehaviour
{
    [Header("Knockback Settings")]
    [SerializeField] private float _knockbackForce = 1f;
    [SerializeField] private float _knockbackDuration = 3f;
    [SerializeField] private float _upwardForce = 2f;

    private Rigidbody rb;
    private float knockbackTimer = 0f;

    [SerializeField] private CharacterMovementController movementScript;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyKnockback(Vector3 direction, float forceMultiplier = 1f)
    {
        movementScript.IsTakeInput = false;
        Vector3 knockbackDirection = direction.normalized;
        knockbackDirection.y += _upwardForce;

        rb.AddForce(knockbackDirection * _knockbackForce * forceMultiplier, ForceMode.Impulse);

        knockbackTimer = _knockbackDuration;

        StartCoroutine(movementScript.GetPushBack(_knockbackDuration));
    }
}
