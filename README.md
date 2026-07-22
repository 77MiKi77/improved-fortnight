# Rose Desk Pet

一个 Windows x64 桌宠：透明无边框、默认置顶、可拖动、滚轮缩放、右键菜单，以及点击后的随机中文气泡和跳跃／压扁回弹／左右抖动互动。

## 角色素材

基于四张参考照片共同提炼的设定：长款微卷黑发、中分、白色蝴蝶发夹、珍珠耳钉、温柔圆眼与微笑；白色镂空刺绣泡袖连衣裙、白袜、黑色厚底系带鞋与细手链。`assets/action-reference-sheet.png` 和 `assets/bonus-actions.png` 是统一画风的动作设计稿；主体 PNG 已完成去除洋红背景。

动作稿包含待机、挥手、开心跳跃、左顾右盼、坐下休息、困倦／睡觉、生气鼓嘴、害羞、思考、奔跑、比心、欢呼等，并以统一朝向和完整肢体输出，便于继续切分。`tools/prepare-sprites.ps1` 可用 ImageMagick 输出独立透明动作图。

## 在 Windows 直接生成 EXE

安装 [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 后，在 PowerShell 执行：

```powershell
cd PetRose
.\publish-win-x64.ps1
```

完成后双击 `dist\RoseDeskPet-win-x64\RoseDeskPet.exe`。发布为自包含 x64 单文件，不要求最终使用者另装 .NET。

## 操作

- **左键拖拽**：移动桌宠。
- **单击角色**：依次跳跃、压扁回弹、左右摇晃，同时弹出不遮挡角色的随机中文白色对话气泡。
- **滚轮**：缩放（45%–190%）。
- **右键角色**：放大、缩小、始终置顶开关、退出。

## hatch-pet 包装

`hatch-pet/pet.json` 保留了 8×9 atlas 工作流所需的 manifest。此项目按 hatch-pet 的“先建立稳定角色基准图，再生成逐动作参考、去背景、QA 与打包”流程建立；桌宠程序当前使用最稳定的透明待机主图，并用 WPF 变换制作交互动画。
