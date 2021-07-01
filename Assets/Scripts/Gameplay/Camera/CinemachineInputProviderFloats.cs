using Cinemachine;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace ASOIAF {
    public class CinemachineInputProviderFloats : CinemachineExtension, AxisState.IInputAxisProvider {
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
        }
        /// <summary>
        /// Leave this at -1 for single-player games.
        /// For multi-player games, set this to be the player index, and the actions will
        /// be read from that player's controls
        /// </summary>
        [Tooltip("Leave this at -1 for single-player games.  "
            + "For multi-player games, set this to be the player index, and the actions will "
            + "be read from that player's controls")]
        public int PlayerIndex = -1;

        /// <summary>Vector2 action for XY movement</summary>
        [Tooltip("Vector2 action for XY movement")]
        public InputActionReference XAxis;
        /// <summary>Vector2 action for XY movement</summary>
        [Tooltip("Vector2 action for XY movement")]
        public InputActionReference YAxis;

        /// <summary>Float action for Z movement</summary>
        [Tooltip("Float action for Z movement")]
        public InputActionReference ZAxis;

        /// <summary>
        /// Implementation of AxisState.IInputAxisProvider.GetAxisValue().
        /// Axis index ranges from 0...2 for X, Y, and Z.
        /// Reads the action associated with the axis.
        /// </summary>
        /// <param name="axis"></param>
        /// <returns>The current axis value</returns>
        public virtual float GetAxisValue(int axis) {
            InputActionReference actionRef = axis == 1 ? axis == 2 ? ZAxis : YAxis : XAxis;
            InputAction action = ResolveForPlayer(axis, actionRef);
            if(action != null) {
                switch(axis) {
                    case 0:
                        return action.ReadValue<float>();
                    case 1:
                        return action.ReadValue<float>();
                    case 2:
                        return action.ReadValue<float>();
                }
            }
            return 0;
        }

        private const int NUM_AXES = 3;
        private InputAction[] m_cachedActions;

        /// <summary>
        /// In a multi-player context, actions are associated with specific players
        /// This resolves the appropriate action reference for the specified player.
        /// 
        /// Because the resolution involves a search, we also cache the returned 
        /// action to make future resolutions faster.
        /// </summary>
        /// <param name="axis">Which input axis (0, 1, or 2)</param>
        /// <param name="actionRef">Which action reference to resolve</param>
        /// <returns>The cached action for the player specified in PlayerIndex</returns>
        protected InputAction ResolveForPlayer(int axis, InputActionReference actionRef) {
            if(axis < 0 || axis >= NUM_AXES)
                return null;
            if(actionRef == null || actionRef.action == null)
                return null;
            if(m_cachedActions == null || m_cachedActions.Length != NUM_AXES)
                m_cachedActions = new InputAction[NUM_AXES];
            if(m_cachedActions[axis] != null && actionRef.action.id != m_cachedActions[axis].id)
                m_cachedActions[axis] = null;
            if(m_cachedActions[axis] == null) {
                m_cachedActions[axis] = actionRef.action;
                if(PlayerIndex != -1) {
                    InputUser user = InputUser.all[PlayerIndex];
                    m_cachedActions[axis] = user.actions.First(x => x.id == actionRef.action.id);
                }
            }
            // Auto-enable it if disabled
            if(m_cachedActions[axis] != null && !m_cachedActions[axis].enabled)
                m_cachedActions[axis].Enable();

            return m_cachedActions[axis];
        }

        // Clean up
        protected virtual void OnDisable() {
            m_cachedActions = null;
        }
    }
}