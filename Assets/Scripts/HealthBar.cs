using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int max;
    public int min;
    public int curr;
    public Image mask;
    void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float currentOffset = curr - min;
        float maxOffset = max - min;
        float fillAmount = currentOffset / maxOffset;
        mask.fillAmount = fillAmount;
    }
}
