using UnityEngine;

public class MoovingPlatformObject : PuzzleObjects, IInteractable
{
    [SerializeField] private float targetHeight = 5f; // Hedef yüksekliği
    [SerializeField] private float maxMoveVelocity = 5f; // Maksimum hız
    [SerializeField] private float acceleration = 2f; // Hızlanma katsayısı
    private bool isActivated = false;
    private float currentVelocityY = 0f; // Yavaş yavaş hızlanmak için kullanılan geçici değişken
    private Transform parentTransform; // Parent Transform referansı

    private void Awake()
    {
        parentTransform = transform.parent.transform.parent;
        Debug.Log(parentTransform.name);
    }

    public void Interact(Bubble bubble)
    {
        if (bubble is FireBubble)
        {
            Activate();
        }
    }

    public override void Activate()
    {
        isActivated = true; // Hareketi başlat
    }

    private void Update()
    {
        if (isActivated && parentTransform != null)
        {
            // Parent transformu hedef yükseklik boyunca hareket ettir
            parentTransform.MoveToHeight(targetHeight, maxMoveVelocity, acceleration, ref currentVelocityY);
        }
    }
}
