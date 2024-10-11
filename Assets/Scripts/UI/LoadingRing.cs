using DG.Tweening;
using UnityEngine;

public class LoadingRing : MonoBehaviour
{
    [SerializeField] private GameObject rotatingObject;
    [SerializeField] private float rotationSpeed;

    private Tweener tweener;

    private void OnEnable()
    {
        TurnOnLoadingRing();
    }

    private void OnDestroy()
    {
        tweener?.Kill();
    }

    public void TurnOnLoadingRing()
    {
        gameObject.SetActive(true);
        
        tweener ??= rotatingObject.transform.DORotate(new Vector3(0, 0, -360f), rotationSpeed)
            .SetLoops(-1)
            .SetEase(Ease.Linear)
            .SetRelative(true);  
        
        tweener.Play();
    }

    public void TurnOffLoadingRing()
    {
        gameObject.SetActive(false);
        tweener.Pause();
    }
}