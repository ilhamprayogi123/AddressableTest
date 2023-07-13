using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using Cinemachine;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoBehaviour
{
    [SerializeField]
    private AssetReference playerArmatureAssetReference;

    //[SerializeField]
    //private AssetReferenceTexture2D unityLogoAssetReference;

    [SerializeField]
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private GameObject playerController;

    // Start is called before the first frame update
    void Start()
    {
        Addressables.InitializeAsync().Completed += AddressableManager_Completed;
        Debug.Log("Test for Upload");
    }

    private void AddressableManager_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        playerArmatureAssetReference.InstantiateAsync().Completed += (go) =>
        {
            playerController = go.Result;
            cinemachineVirtualCamera.Follow = playerController.transform.Find("PlayerCameraRoot");
        };
    }

    private void OnDestroy()
    {
        playerArmatureAssetReference.ReleaseInstance(playerController);
    }
}
