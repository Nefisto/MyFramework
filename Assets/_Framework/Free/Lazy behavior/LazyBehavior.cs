using UnityEngine;
using System;

public abstract partial class LazyBehavior : MonoBehaviour
{
    [HideInInspector, NonSerialized]
    private Rigidbody2D _rigidbody2D;
    public new Rigidbody2D rigidbody2D { get => _rigidbody2D ? _rigidbody2D : (_rigidbody2D = GetComponent<Rigidbody2D>()); }

    [HideInInspector, NonSerialized]
    private Animator _animator;
    public Animator animator { get => _animator ? _animator : (_animator = GetComponent<Animator>()); }

    [HideInInspector, NonSerialized]
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer spriteRenderer { get => _spriteRenderer ? _spriteRenderer : (_spriteRenderer = GetComponent<SpriteRenderer>()); }

    [HideInInspector, NonSerialized]
    private BoxCollider2D _boxCollider2D;
    public BoxCollider2D boxCollider2D { get => _boxCollider2D ? _boxCollider2D : (_boxCollider2D = GetComponent<BoxCollider2D>()); }
}