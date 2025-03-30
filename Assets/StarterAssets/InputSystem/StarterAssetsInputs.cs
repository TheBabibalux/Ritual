using Unity.VisualScripting;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool crouch;
		public bool interact;
		public float interactHold;
		public InputAction interactInput;

        public bool use;
		public float useHold;
        public InputAction useToolInput;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

        private void Update()
        {
			if(interact && !interactInput.ReadValue<bool>())
			{
				interact = false;
				interactHold = 0;
			}
			else if(interact && interactInput.ReadValue<bool>())
			{
				interactHold += Time.deltaTime;
			}

            if (use)
            {
                useHold += Time.deltaTime;
            }
            else
            {
				useHold = 0;
            }
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputAction.CallbackContext _context)
		{
			MoveInput(_context.ReadValue<Vector2>());
		}

		public void OnLook(InputAction.CallbackContext _context)
		{
			if(cursorInputForLook)
			{
				LookInput(_context.ReadValue<Vector2>());
			}
		}

		public void OnJump(InputAction.CallbackContext _context)
		{
			JumpInput(_context.performed);
		}

		public void OnSprint(InputAction.CallbackContext _context)
		{
			SprintInput(_context.performed);
        }

		public void OnCrouch(InputAction.CallbackContext _context)
		{
			CrouchInput(_context.performed);
		}

        public void OnInteract(InputAction.CallbackContext _context)
        {
            InteractInput(_context.performed);
        }

        public void OnUsePress(InputAction.CallbackContext _context)
        {
            UseInput(_context.performed);
        }
#endif
        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		public void CrouchInput(bool newCrouchState)
		{
			crouch = newCrouchState;
		}
        public void InteractInput(bool newInteractState)
        {
            interact = newInteractState;
        }
        public void UseInput(bool newUseState)
        {
            use = newUseState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}