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
        [Header("Dash")]
        [SerializeField] private float dashDuration = 1f;
        [SerializeField] private float dashSpeed = 1f;
        [SerializeField] private float dashCooldownTime = 1f;
        
        private Vector2 _movement = Vector2.zero;
        private SpriteRenderer _sprite;
        private readonly int _velocityProperty = Animator.StringToHash("Speed");
        private bool _canDash = true;

        void Start()
        {
            _sprite = GetComponentInChildren<SpriteRenderer>();
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
            
            if (horizontalInput < 0.01f && horizontalInput > -0.01f) return;
            
            bool shouldFlipSprite = false;
            if (verticalInput > 0) shouldFlipSprite = false;
            else if (horizontalInput < 0) shouldFlipSprite = true;
            
            if (_sprite)
                _sprite.flipX = shouldFlipSprite;
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
                CameraShake.Instance.Shake(.05f, .1f);
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
