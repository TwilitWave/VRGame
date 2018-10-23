using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace SSR.Player
{
    public class PlayerSetup : MonoBehaviour
    {
        public Hand setupHand;

        public GameObject itemPrefab;

        public GameObject otherHandItemPrefab;
        [EnumFlags]
        public Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags;
        private void Awake()
        {
            if (this.setupHand == null)
            {
                throw new System.Exception("Please assign the setup hand");
            }
            if (this.itemPrefab == null)
            {
                throw new System.Exception("Please assign the item prefab");
            }
            if (this.otherHandItemPrefab == null)
            {
                throw new System.Exception("Please assign the other hand item prefab");
            }
        }
        // Use this for initialization
        private void Start()
        {
            this.StartCoroutine(this.SetupNextFrame());
        }

        private IEnumerator SetupNextFrame()
        {
            yield return null;
            var spawnedItem = GameObject.Instantiate(this.itemPrefab);
            spawnedItem.SetActive(true);
            this.setupHand.AttachObject(spawnedItem, GrabTypes.Scripted, attachmentFlags);

            if (this.otherHandItemPrefab != null)
            {
                GameObject otherHandObjectToAttach = GameObject.Instantiate(this.otherHandItemPrefab);
                otherHandObjectToAttach.SetActive(true);
                this.setupHand.otherHand.AttachObject(otherHandObjectToAttach, GrabTypes.Scripted, attachmentFlags);
            }
        }


        private void Update()
        {
        }
    }
}

