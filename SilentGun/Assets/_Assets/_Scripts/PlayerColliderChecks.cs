using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementScript))]
public class PlayerColliderChecks : MonoBehaviour
{
	[SerializeField] private Transform groundCheck;
	[SerializeField] private float groundCheckRadius = 0.2f;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask obstacleLayer;

	[HideInInspector] public bool isGrounded;
	private PlayerMovementScript playerMovementScript;

	private EnvironmentScript envScript;

	private void Start()
	{
		envScript = GameObject.FindObjectOfType<EnvironmentScript>();
		playerMovementScript = this.gameObject.GetComponent<PlayerMovementScript>();
	}

	void Update()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

		Collider2D obstacleCollider;

		if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, obstacleLayer))
		{
			obstacleCollider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, obstacleLayer);
		}

	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
	}
}