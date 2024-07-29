using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoad : MonoBehaviour
{
    [field: SerializeField]
    public Collider RoadCollider;

    [field: SerializeField]
    public List<Collider> DecorColliders { get; private set; }
}
