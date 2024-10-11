using UnityEngine;
using UnityEngine.UI;

public class PolicyScreen : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    
    void Start()
    {
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        gameObject.SetActive(false);
    }
}
