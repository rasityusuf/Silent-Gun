using UnityEngine;

public static class TransformExtensions
{
    public static void MoveToHeight(this Transform transform, float targetHeight, float maxVelocity, float acceleration, ref float currentVelocityY)
    {
        float currentY = transform.position.y;

        // Eğer hedef yüksekliğe ulaşılmamışsa hareket ettir
        if (currentY < targetHeight)
        {
            // Yeni hızı hesapla
            currentVelocityY = Mathf.Lerp(currentVelocityY, maxVelocity, acceleration * Time.deltaTime);

            // Yeni pozisyonu uygula
            transform.position = new Vector3(transform.position.x, currentY + currentVelocityY * Time.deltaTime, transform.position.z);
        }
        else
        {
            // Hedefe ulaşıldığında hızı sıfırla
            currentVelocityY = 0;
            //Debug.Log("Platform reached target height.");
        }
    }
}
