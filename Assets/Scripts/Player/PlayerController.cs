using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float speed = 1f;
        [SerializeField] private Animator animator = null;
        [Header("Inputs")]
        [SerializeField] private KeyCode dash;
        [SerializeField] private KeyCode attack;
        [SerializeField] private KeyCode interact;
        [Header("Dash")]
        [SerializeField] private float dashDuration = 1f;
        [SerializeField] private float dashSpeed = 1f;
        [SerializeField] private float dashCooldownTime = 1f;
        
        private Vector2 _movement = Vector2.zero;
        private SpriteRenderer _sprite;
        private readonly int _velocityProperty = Animator.StringToHash("Speed");
        private bool _canDash = true;

        private WeaponInventory _inventory;
        private InteractionSystem _interactionSystem;

        void Start()
        {
            _sprite = GetComponentInChildren<SpriteRenderer>();
            _inventory = GetComponent<WeaponInventory>();
            _interactionSystem = GetComponent<InteractionSystem>();
        }

        void Update()
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _movement = new Vector2(horizontalInput, verticalInput).normalized;
            transform.position += new Vector3(_movement.x,_movement.y) * (speed * Time.deltaTime);
            HandleInput();

            if(animator)
                animator.SetFloat(_velocityProperty, _movement.magnitude);
            if (_sprite && Camera.main)
                _sprite.flipX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x;
        }
        
        void HandleInput()
        {
            if (Input.GetKeyDown(dash) && _canDash)
            {
                _canDash = false;
                StartCoroutine(Dash());
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                if(TryGetComponent<IDamageInterface>(out IDamageInterface damageInterface))
                    damageInterface.Damage(DamageType.Normal, 1);
            }

            if (Input.GetKeyDown(attack))
            {
                Vector3 attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                Attack(attackDirection);
            }

            if (Input.GetKeyDown(interact))
            {
                if(_interactionSystem)
                    _interactionSystem.Interact();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (!_inventory) return;
                _inventory.SwitchWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (!_inventory) return;
                _inventory.SwitchWeapon(1);
            }
        }

        void Attack(Vector3 direction)
        {
            if (_inventory)
            {
                WeaponBase weapon = _inventory.GetEquipedWeapon();
                if (weapon)
                {
                    weapon.Attack(direction);
                }
            }
        }

        IEnumerator Dash()
        {
            float time = 0;
            Vector3 direction = _movement;
            while (time < dashDuration)
            {
                transform.position += direction * (dashSpeed);
                time += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(DashCooldown(dashCooldownTime));
        }

        IEnumerator DashCooldown(float cooldownDuration)
        {
            yield return new WaitForSeconds(cooldownDuration);
            _canDash = true;
            yield return null;
        }
    }
    
}
