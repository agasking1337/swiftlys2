using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menus;

public interface IMenuBuilder
{
    IMenuBuilder SetMenu(IMenu menu);
    IMenuBuilder AddButton(string text, Action<IPlayer>? onClick = null, IMenuTextSize size = IMenuTextSize.Medium);
    IMenuBuilder AddToggle(string text, bool defaultValue = false, Action<IPlayer, bool>? onToggle = null, IMenuTextSize size = IMenuTextSize.Medium);
    IMenuBuilder AddSlider(string text, float min, float max, float defaultValue, float step = 1, Action<IPlayer, float>? onChange = null, IMenuTextSize size = IMenuTextSize.Medium);
    IMenuBuilder AddAsyncButton(string text, Func<IPlayer, Task> onClickAsync, IMenuTextSize size = IMenuTextSize.Medium);
    IMenuBuilder AddText(string text, ITextAlign alignment = ITextAlign.Left, IMenuTextSize size = IMenuTextSize.Medium);
    IMenuBuilder AddSubmenu(string text, IMenu submenu, IMenuTextSize size = IMenuTextSize.Medium);
    IMenuBuilder AddSubmenu(string text, IMenu submenu);
    IMenuBuilder AddSubmenu(string text, Func<IMenu> submenuBuilder, IMenuTextSize size = IMenuTextSize.Medium);
    IMenuBuilder AddSubmenu(string text, Func<IMenu> submenuBuilder);
    IMenuBuilder AddChoice(string text, string[] choices, string? defaultChoice = null, Action<IPlayer, string>? onChange = null, IMenuTextSize size = IMenuTextSize.Medium);
    IMenuBuilder AddChoice(string text, string[] choices, string? defaultChoice, Action<IPlayer, string>? onChange);
    IMenuBuilder AddSeparator();
    IMenuBuilder AddProgressBar(string text, Func<float> progressProvider, int barWidth = 20, IMenuTextSize size = IMenuTextSize.Medium);
    IMenuBuilder AddProgressBar(string text, Func<float> progressProvider, int barWidth);

    IMenuBuilder WithParent(IMenu parent);
    IMenuBuilder VisibleWhen(Func<IPlayer, bool> condition);
    IMenuBuilder EnabledWhen(Func<IPlayer, bool> condition);
    IMenuBuilder CloseOnSelect();
    IMenuBuilder AutoClose(float seconds);
    IMenuBuilder OverrideSelectButton(params string[] buttonNames);
    IMenuBuilder OverrideMoveButton(params string[] buttonNames);
    IMenuBuilder OverrideExitButton(params string[] buttonNames);
    IMenuBuilder MaxVisibleItems(int count);
    IMenuBuilder NoFreeze();
    IMenuBuilder ForceFreeze();
    IMenuBuilder SetColor(Color color);
    string Build();
}

public enum ITextAlign
{
    Left,
    Center,
    Right
}