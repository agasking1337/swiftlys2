using System.Collections.Concurrent;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menus;

public interface IMenu
{
    public string Title { get; set; }
    public List<IOption> Options { get; }
    public IMenu? Parent { get; set; }
    public ConcurrentDictionary<IPlayer, CancellationTokenSource?> AutoCloseCancelTokens { get; set; }
    public IMenuButtonOverrides? ButtonOverrides { get; set; }
    public int MaxVisibleOptions { get; set; }
    public bool? ShouldFreeze { get; set; }
    public bool? CloseOnSelect { get; set; }
    public Color RenderColor { get; set; }
    public IMenuManager MenuManager { get; set; }
    public float AutoCloseAfter { get; set; }
    public IMenuBuilder Builder { get; }

    event Action<IPlayer>? OnOpen;
    event Action<IPlayer>? OnClose;
    event Action<IPlayer>? OnMove;
    event Action<IPlayer, IOption>? OnItemSelected;
    event Action<IPlayer, IOption>? OnItemHovered;
    event Action<IPlayer>? BeforeRender;
    event Action<IPlayer>? AfterRender;

    public void Show(IPlayer player);
    public void Close(IPlayer player);
    public void MoveSelection(IPlayer player, int offset);
    public void UseSelection(IPlayer player);
    public void UseSlideOption(IPlayer player, bool isRight);
    public void Rerender(IPlayer player);
    public bool IsCurrentOptionSelectable(IPlayer player);
    public void SetFreezeState(IPlayer player, bool freeze);
}
