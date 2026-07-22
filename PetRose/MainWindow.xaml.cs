using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace RoseDeskPet;
public partial class MainWindow : Window
{
    private Point _dragStart; private bool _dragging; private int _interaction; private double _size = 1;
    private readonly string[] _lines = ["今天也要元气满满！", "嘿嘿，被你发现啦～", "给你一朵小玫瑰！", "一起摸鱼五分钟？", "眨眨眼，烦恼飞走！", "我会在这里陪你哦。"];
    private readonly Random _random = new();
    public MainWindow()
    {
        InitializeComponent();
        Pet.Source = new BitmapImage(new Uri(System.IO.Path.Combine(AppContext.BaseDirectory, "assets", "rose-dress-chibi.png")));
        Pet.MouseLeftButtonDown += Down; Pet.MouseMove += Drag; Pet.MouseLeftButtonUp += Up; Pet.MouseWheel += Wheel;
        Pet.ContextMenu = BuildMenu();
    }
    private ContextMenu BuildMenu()
    {
        var menu = new ContextMenu();
        var larger = new MenuItem { Header = "放大" }; larger.Click += (_,_) => SetSize(.1);
        var smaller = new MenuItem { Header = "缩小" }; smaller.Click += (_,_) => SetSize(-.1);
        var top = new MenuItem { Header = "始终置顶", IsCheckable = true, IsChecked = true }; top.Click += (_,_) => Topmost = top.IsChecked;
        var quit = new MenuItem { Header = "退出程序" }; quit.Click += (_,_) => Close();
        menu.Items.Add(larger); menu.Items.Add(smaller); menu.Items.Add(new Separator()); menu.Items.Add(top); menu.Items.Add(new Separator()); menu.Items.Add(quit); return menu;
    }
    private void Down(object s, MouseButtonEventArgs e) { _dragStart = e.GetPosition(this); _dragging = false; Pet.CaptureMouse(); }
    private void Drag(object s, MouseEventArgs e) { if (e.LeftButton != MouseButtonState.Pressed) return; var p=e.GetPosition(this); if ((p-_dragStart).Length > 4) _dragging=true; if (_dragging) { Left += p.X-_dragStart.X; Top += p.Y-_dragStart.Y; } }
    private void Up(object s, MouseButtonEventArgs e) { Pet.ReleaseMouseCapture(); if (!_dragging) Interact(); }
    private void Wheel(object s, MouseWheelEventArgs e) => SetSize(e.Delta > 0 ? .08 : -.08);
    private void SetSize(double delta) { _size = Math.Clamp(_size+delta,.45,1.9); Width=300*_size; Height=360*_size; }
    private void Interact()
    {
        ShowBubble(); (_interaction++ % 3) switch { 0 => Jump(), 1 => Squash(), _ => Shake() };
    }
    private void ShowBubble()
    {
        BubbleText.Text = _lines[_random.Next(_lines.Length)]; Bubble.Visibility=Visibility.Visible;
        var timer = new DispatcherTimer { Interval=TimeSpan.FromSeconds(2.2) }; timer.Tick += (_,_) => { Bubble.Visibility=Visibility.Collapsed; timer.Stop(); }; timer.Start();
    }
    private void Jump() { Move.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimationUsingKeyFrames { KeyFrames = { new EasingDoubleKeyFrame(0,KeyTime.FromTimeSpan(TimeSpan.Zero)), new EasingDoubleKeyFrame(-55,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(210))), new EasingDoubleKeyFrame(0,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(510))) } }); }
    private void Squash() { var a=new DoubleAnimationUsingKeyFrames { KeyFrames = { new EasingDoubleKeyFrame(1,KeyTime.FromTimeSpan(TimeSpan.Zero)), new EasingDoubleKeyFrame(.77,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(130))), new EasingDoubleKeyFrame(1.12,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(250))), new EasingDoubleKeyFrame(1,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(430))) } }; Scale.BeginAnimation(ScaleTransform.ScaleYProperty,a); Scale.BeginAnimation(ScaleTransform.ScaleXProperty,a); }
    private void Shake() { Move.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimationUsingKeyFrames { KeyFrames = { new LinearDoubleKeyFrame(-17,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(70))), new LinearDoubleKeyFrame(17,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(140))), new LinearDoubleKeyFrame(-12,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(210))), new LinearDoubleKeyFrame(12,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(280))), new LinearDoubleKeyFrame(0,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(350))) } }); }
}
