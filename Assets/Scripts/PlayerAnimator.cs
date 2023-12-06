using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_Walking = "IsWalking";
    [SerializeField] private Player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool(IS_Walking, player.IsWalking());
    }
}
