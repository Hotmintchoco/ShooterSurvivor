using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    public float moveSpeed = 5f;
    public float exp = 0;
    public float maxExp = 100f;

    public Crosshairs crosshairs;

    Camera viewCamera;
    PlayerController controller;
    GunController gunController;
    Item nearItem;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);
        if (anim != null)
        {
            anim.SetFloat("Speed", moveInput.magnitude, 0.1f, Time.deltaTime);
        }

        // Look input
        Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * gunController.GunHeight);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            // Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
            crosshairs.transform.position = point;
            crosshairs.DetectTargets(ray);
            if ((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 1)
            {
                gunController.Aim(point);
            }
        }

        // Weapon input
        if (Input.GetMouseButton(0))
        {
            gunController.OnTriggerHold();
            anim.SetTrigger("doShoot");
        }
        if (Input.GetMouseButtonUp(0))
        {
            gunController.OnTriggerRelease();
        }

        // Get Weapon
        if (Input.GetKeyDown(KeyCode.E) && nearItem)
        {
            gunController.EquipGun(nearItem.value);
            Destroy(nearItem.gameObject);
        }
    }

    public override void Die()
    {
        base.Die();
        Cursor.visible = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            nearItem = other.GetComponent<Item>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearItem = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            print(item);
            switch(item.type)
            {
                case Item.Type.Exp:
                    exp += item.value;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    break;
            }
            Destroy(other.gameObject);
        }
    }
}
