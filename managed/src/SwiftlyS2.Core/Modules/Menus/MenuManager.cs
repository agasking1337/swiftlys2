using System.Collections.Concurrent;
using System.Globalization;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menus;

internal class MenuManager : IMenuManager
{
    public MenuSettings Settings { get; set; } = new MenuSettings();

    public event Action<IPlayer, IMenu>? OnMenuClosed;
    public event Action<IPlayer, IMenu>? OnMenuOpened;
    public event Action<IPlayer, IMenu>? OnMenuRendered;

    private ConcurrentDictionary<IPlayer, IMenu> OpenMenus { get; set; } = new();

    public MenuManager()
    {
        var settings = NativeEngineHelpers.GetMenuSettings();
        var parts = settings.Split('\x01');
        Settings = new MenuSettings
        {
            NavigationPrefix = parts[0],
            InputMode = parts[1],
            ButtonsUse = parts[2],
            ButtonsScroll = parts[3],
            ButtonsExit = parts[4],
            SoundUseName = parts[5],
            SoundUseVolume = float.Parse(parts[6], CultureInfo.InvariantCulture),
            SoundScrollName = parts[7],
            SoundScrollVolume = float.Parse(parts[8], CultureInfo.InvariantCulture),
            SoundExitName = parts[9],
            SoundExitVolume = float.Parse(parts[10], CultureInfo.InvariantCulture),
            ItemsPerPage = int.Parse(parts[11]),
        };
    }

    public void CloseMenu(IMenu menu)
    {
        foreach (var kvp in OpenMenus)
        {
            var player = kvp.Key;
            var openMenu = kvp.Value;

            if (openMenu == menu)
            {
                CloseMenuForPlayer(player);
            }
        }
    }

    public void CloseMenuByTitle(string title, bool exact = false)
    {
        foreach (var kvp in OpenMenus)
        {
            var player = kvp.Key;
            var menu = kvp.Value;

            if ((exact && menu.Title == title) || (!exact && menu.Title.Contains(title)))
            {
                CloseMenuForPlayer(player);
            }
        }
    }

    public void CloseMenuForPlayer(IPlayer player)
    {
        if (OpenMenus.TryRemove(player, out var menu))
        {
            menu.Close(player);
            OnMenuClosed?.Invoke(player, menu);
            if(menu.Parent != null)
            {
                OpenMenu(player, menu.Parent);
            }
        }
    }

    public IMenu CreateMenu(string title)
    {
        return new Menu { Title = title, MenuManager = this, MaxVisibleOptions = Settings.ItemsPerPage };
    }

    public IMenu? GetMenu(IPlayer player)
    {
        return OpenMenus.TryGetValue(player, out var menu) ? menu : null;
    }

    public void OpenMenu(IPlayer player, IMenu menu)
    {
        OpenMenus[player] = menu;
        menu.Show(player);
        OnMenuOpened?.Invoke(player, menu);
    }
}