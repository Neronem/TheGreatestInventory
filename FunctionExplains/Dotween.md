## 🎞️ DoTween

### ✅ **사용 배경**
UI 시각적 효과를 간단하게나마 높이고 싶었기에, Dotween 패키지를 활용했습니다.

### 🛠 **Dotween 사용 방식**

#### 1️. UI 전환 슬라이드 (`UIManager.cs`)
- UI 패널이 **오른쪽에서 슬라이드되어 등장**, 다시 **오른쪽으로 슬라이드되며 퇴장**
- `anchoredPosition`을 `DOAnchorPos`로 보간하여 부드럽게 이동

```csharp
// 등장 시
uiRect.anchoredPosition = hiddenPos; // 오른쪽에서 시작
uiRect.DOAnchorPos(originalPos, doTweenDuration) // 원래 위치로 이동
    .SetEase(Ease.OutCubic);

// 퇴장 시
uiRect.DOAnchorPos(hiddenPos, doTweenDuration)
    .SetEase(Ease.InCubic)
    .OnComplete(() => { rootObject.SetActive(false); });
```

#### 2. 버튼 클릭 애니메이션 (`UIMainMenu.cs`)
- 버튼 클릭 시 **살짝 확대되며 반동 애니메이션 적용**
```csharp
_transform.DOScale(1.2f, 0.2f)
    .SetLoops(2, LoopType.Yoyo) // 두 번 반복 (확대 → 원래대로)
    .SetEase(Ease.OutBack)
    .OnComplete(() => { onComplete?.Invoke(); });
```

<br>

### 💡 **기대 효과**
- UI 전환 시 **사용자 시선 유도 및 몰입도 향상**

