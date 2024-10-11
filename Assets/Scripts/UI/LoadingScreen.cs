using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Sprite[] flowerSprites;
    [SerializeField] private Image flowerImage;
    [SerializeField] private LoadingRing loadingRing;
    [SerializeField] private GameObject welcomeText;
    
    void Start()
    {
        welcomeText.SetActive(false);
        Loading().Forget();
    }

    private async UniTask Loading()
    {
        flowerImage.sprite = flowerSprites[0];
        await UniTask.Delay(2000);
        flowerImage.sprite = flowerSprites[1];
        await UniTask.Delay(2000);
        flowerImage.sprite = flowerSprites[2];
        welcomeText.SetActive(true);
        await UniTask.Delay(2000);
        loadingRing.TurnOffLoadingRing();
        AudioManager.Instance.SetStartState();
        gameObject.SetActive(false);
    }
}
