using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask interactionLayer;

    private GameObject _closestItem;

    private void Update()
    {
        RaycastHit2D[] hits =
            Physics2D.CircleCastAll(transform.position, radius, transform.right, radius, interactionLayer);

        if (hits.Length <= 0) _closestItem = null;

        foreach (var hit in hits)
        {
            if (!_closestItem)
                _closestItem = hit.collider.gameObject;
            else
            {
                if (Vector3.Distance(transform.position, hit.collider.transform.position) <
                    Vector3.Distance(transform.position, _closestItem.transform.position))
                    _closestItem = hit.collider.gameObject;
            }
        }

        //UI de closest item
    }

    public void Interact()
    {
        if (_closestItem &&
            _closestItem.TryGetComponent<IInteractionInterface>(out IInteractionInterface interactionInterface))
            interactionInterface.HandleInteraction();
    }
}