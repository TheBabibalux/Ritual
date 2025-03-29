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

			if (use && !useToolInput.ReadValue<bool>()) 
			{
				use = false;
				useHold = 0;
			}
            else if (use && useToolInput.ReadValue<bool>())
            {
                useHold += Time.deltaTime;
            }
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnCrouch(InputValue value)
		{
			CrouchInput(value.isPressed);
		}

        public void OnInteract(InputValue value)
        {
            InteractInput(value.isPressed);
        }

        public void OnUse(InputValue value)
        {
            InteractInput(value.isPressed);
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