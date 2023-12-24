#if UNITY_EDITOR
using System;
using UnityEngine;
using System.Linq;
using Gann4Games.RagdollFactory.States;

namespace Gann4Games.RagdollFactory
{
    [ExecuteInEditMode]
    public partial class RagdollFactory : MonoBehaviour
    {
        public RFComponentState CurrentComponent { get; private set; }

        public RFComponentState[] States { get; private set; }
        
        public BoxColliderComponentState boxColliderComponentState;
        public CapsuleColliderComponentState capsuleColliderComponentState;
        public ConfigurableJointComponentState configurableJointComponentState;
        public RigidbodyComponentState rigidbodyComponentState;
        
        public Transform[] Bones => GetComponentsInChildren<Transform>().Where(bone => bone.GetComponent<Collider>() == false).ToArray();

        public Transform selectedBoneA;
        public Transform selectedBoneB;
        
        public ComponentType componentType;
        public ActionTypeOnClick actionTypeOnClick;

        //[Header("Capsule Collider Settings")]
        public float capsuleColliderRadius = 0.1f;
        public float capsuleColliderLength;
        
        //[Header("Box Collider Settings")]
        public float boxColliderWidth = 0.1f;
        public float boxColliderDepth = 0.1f;
        public float boxColliderLength;
        
        // Joint settings
        public Vector3 jointAxis = new(1, 0, 0);
        [Range(0, 180)] public float jointLowXLimit;
        [Range(0, 180)] public float jointHighXLimit;
        [Range(0, 180)] public float jointYLimit;
        [Range(0, 180)] public float jointZLimit;

        // Rigidbody settings
        public float rigidbodyMass = 1;
        public float rigidbodyDrag;
        public float rigidbodyAngularDrag = 0.05f;
        public bool rigidbodyUseGravity = true;
        public bool rigidbodyIsKinematic;
        
        [Header("Gizmos Settings")]
        public float discRadius = 0.02f;
        public Color normalColor = new(1, 1, 1, 0.25f);
        public Color selectedColor = new(1, 1, 0, 0.25f);

        public bool IsFirstBoneSelected => selectedBoneA != null;

        public enum ActionTypeOnClick
        {
            Create, Select, Delete
        }
        
        public enum ComponentType
        {
            Capsule, Box, ConfigurableJoint, Rigidbody
        }

        public void SetState(RFComponentState state)
        {
            CurrentComponent = state;
        }
        
        /// <summary>
        /// Returns the index of a state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private int GetStateIndex(RFComponentState state) => Array.IndexOf(States, state);

        /// <summary>
        /// Returns the current state as an integer
        /// </summary>
        public int CurrentStateIndex() => GetStateIndex(CurrentComponent);

        public void DeselectBones()
        {
            selectedBoneA = null;
            selectedBoneB = null;
        }

        private void OnValidate()
        {
            CurrentComponent?.Update();
        }

        private void OnEnable()
        {   
            capsuleColliderComponentState = new CapsuleColliderComponentState(this);
            boxColliderComponentState = new BoxColliderComponentState(this);
            configurableJointComponentState = new ConfigurableJointComponentState(this);
            rigidbodyComponentState = new RigidbodyComponentState(this);
            States = new RFComponentState[]
            {
                capsuleColliderComponentState, 
                boxColliderComponentState, 
                configurableJointComponentState, 
                rigidbodyComponentState
            };

            CurrentComponent = capsuleColliderComponentState;
        }
    }
}
#endif