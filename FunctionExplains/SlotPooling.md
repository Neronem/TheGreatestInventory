## 📦 인벤토리 슬롯 풀링

### ✅ **사용 배경**
제 게임에선 인벤토리 슬롯(UI 요소)이 **반복 생성/제거**되는 구조였기 때문에,  
**효율적인 UI 관리를 위해 풀링 기법**을 도입하였습니다.

### 🛠 **슬롯 풀링 사용 방식**
- `List<UISlot> slots`를 통해 슬롯 풀을 유지
- `SetInventory()` 호출 시:
    - 재사용 가능한 슬롯이 있다면 `SetActive(true)`만 하고 재사용
    - 부족한 경우에만 `Instantiate()`로 새로운 슬롯 생성 및 풀에 추가
    - 남은 슬롯은 `SetActive(false)`로 비활성화하여 다음에 재사용

```csharp
// 이미 존재하는 슬롯은 재활용
if (i < slots.Count)
{
    slot = slots[i];
    slot.gameObject.SetActive(true);
}
else
{
    // 부족하면 새로 생성
    GameObject obj = Instantiate(slotPrefab, slotParent);
    slot = obj.GetComponent<UISlot>();
    slots.Add(slot);
}
```

<br>

### 💡 **기대 효과**
- 인벤토리 열고 닫을 때 발생할 수 있는 메모리 낭비 방지

