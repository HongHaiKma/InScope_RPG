using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;
    [SerializeField]
    private Text statValue;
    [SerializeField]
    private float lerpSpeed;
    private float currentFill;
    public float MyMaxValue {get; set;}
    private float currentValue;
    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if(value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if(value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;

            statValue.text = currentValue + "/" + MyMaxValue;
        }
    }

    
    void Start()
    {
        MyMaxValue = 100f; 
        content = GetComponent<Image>();
        
    }

    void Update()
    {
        if(currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
        // content.fillAmount = currentFill;
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
