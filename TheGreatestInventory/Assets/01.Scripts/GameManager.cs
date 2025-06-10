using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [field: SerializeField] public Character player { get; private set; }

    public List<ItemData> itemDatas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async void Start()
    {
        // Addressable 비동기 로드 대기
        await LoadItemDatas();

        // 로드가 완료되면 기존 SetData() 호출
        SetData();
    }

    private async Task LoadItemDatas()
    {
        var handle = Addressables.LoadAssetsAsync<ItemData>("Sword", null);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            itemDatas = new List<ItemData>(handle.Result);
            Debug.Log($"Addressable에서 아이템 데이터 {itemDatas.Count}개 로드 완료");
        }
        else
        {
            Debug.LogError("아이템 데이터 로드 실패");
            itemDatas = new List<ItemData>();
        }
    }

    private void SetData()
    {
        // 기존 로직 유지
        player = new Character("낭만용사", 1, "얼큰오이티라미수 \nvs \n제티마라탕", 10, 100, 5, 5, 100, 10, itemDatas);

        UIManager.instance.MainMenu.SetUIMainMenu(player);
        UIManager.instance.Status.SetUIStatus(player);
        UIManager.instance.Inventory.SetInventory(player);
    }
}