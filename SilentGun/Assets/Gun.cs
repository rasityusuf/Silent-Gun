using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    [SerializeField] private Color fireColor;
    [SerializeField] private Color waterColor;
    [SerializeField] private Color iceColor;
    public enum GunMode
    {
        fireMode, IceMode, waterMode, Default
    }
    [SerializeField] private PlayerMovementScript playerMovementScript;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private FireBubble fireBubble;
    [SerializeField] private WaterBubble waterBubble;
    [SerializeField] private FreezeBubble iceBubble;
    public float angle;
    public Vector2 gunDirection;
    Color color = Color.white;
    GunMode mode = GunMode.Default;

	private void Start()
	{
        color = fireColor;
        mode = GunMode.fireMode;
    }
    private void Update()
    {
        RotateGun();
        ChangeGunType();
        HandleGun();
    }
    private Vector2 GetMouseDirection()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerMovementScript.transform.position;
        gunDirection = direction;
        return direction;
    }

    private void RotateGun()
    {
        Vector2 direction = GetMouseDirection();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.angle = angle;

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

        transform.position = playerMovementScript.transform.position + Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle) * new Vector3(offsetX, offsetY, 0) + new Vector3(0,0, transform.position.z);

    }
    private void ChangeGunType()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            color = fireColor;
            mode = GunMode.fireMode;


        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            color = iceColor;
            mode = GunMode.IceMode;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            color = waterColor;
            mode = GunMode.waterMode;
        }

        spriteRenderer.color = color;
    }
    private void HandleGun()
    {

        if (Input.GetMouseButtonDown(0))
        {
            FireGun();
        }
    }

    private void FireGun()
    {
        if (mode == GunMode.fireMode)
        {
            FireBubble fBubble = Instantiate(fireBubble, transform.position, Quaternion.identity);
            fBubble.BubbleBehaviour(angle, gunDirection);
        }
        else if (mode == GunMode.waterMode)
        {
            WaterBubble wBubble = Instantiate(waterBubble, transform.position, Quaternion.identity);
            wBubble.BubbleBehaviour(angle, gunDirection);
        }
        else if (mode == GunMode.IceMode)
        {
            FreezeBubble iBubble = Instantiate(iceBubble, transform.position, Quaternion.identity);
            iBubble.BubbleBehaviour(angle, gunDirection);
        }
        else if (mode == GunMode.Default)
        {
            Debug.Log("");
        }
        else { Debug.Log(""); }
    }

}
