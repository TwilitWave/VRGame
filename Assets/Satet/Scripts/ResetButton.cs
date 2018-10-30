using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    public class ResetButton : MonoBehaviour
    {
       // public UnityEvent onTakeDamage;

        public bool onceOnly = false;
        public Transform targetCenter;

        public Transform baseTransform;
        public Transform fallenDownTransform;
        public float fallTime = 0.5f;

        const float targetRadius = 0.25f;

        private bool targetEnabled = true;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

