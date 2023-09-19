・ChangeSoundVolume
AudioSourceが付いたオブジェクトにアタッチするとゲーム内での設定に基づき、音量を変更します
計算式 ： 元々AudioSourceに設定していたVolume × 設定画面から設定したゲーム内音量(1～100) / 100

・ChangeTextSize
Textにアタッチするとゲーム内での設定に基づき、文字サイズを変更します
RectTransform.scale = 1, Text.fontSize = 40 で設定画面と同じ文字サイズになります

・PoseWindow
メインゲームに追加してください