namespace SwiftlyS2.Shared.Menus;

/// <summary>
/// Provides a fluent builder interface for creating and configuring menus.
/// All methods support chaining for convenient menu construction.
/// </summary>
public interface IMenuBuilderAPI
{
    /// <summary>
    /// Gets the design interface for this menu.
    /// </summary>
    public IMenuDesignAPI Design { get; }

    /// <summary>
    /// Creates a hierarchical menu by setting a parent menu that players can navigate back to.
    /// </summary>
    /// <param name="parent">The parent menu.</param>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI WithParent( IMenuAPI parent );

    /// <summary>
    /// Adds an option to the menu.
    /// </summary>
    /// <param name="option">The menu option to add.</param>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI AddOption( IMenuOption option );

    /// <summary>
    /// Enables or disables sound effects for menu interactions.
    /// </summary>
    /// <param name="playSound">True to play sounds, false to remain silent.</param>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI PlaySound( bool playSound = false );

    /// <summary>
    /// Controls whether players can move while the menu is open.
    /// </summary>
    /// <param name="freeze">True to freeze player movement, false to allow movement.</param>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI FreezePlayer( bool freeze = false );

    /// <summary>
    /// Sets how long the menu stays open before automatically closing.
    /// </summary>
    /// <param name="seconds">Time in seconds before auto-close. Set to 0 to disable.</param>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI AutoClose( float seconds = 0f );

    /// <summary>
    /// Overrides the key binding for selecting menu options.
    /// </summary>
    /// <param name="keyBind">The key binding to use.</param>
    /// <returns>This builder for method chaining.</returns>
    /// <remarks>
    /// Supports multiple key bindings using the bitwise OR operator.
    /// Example: <c>KeyBind.Mouse1 | KeyBind.E</c> allows either Mouse1 or E to select options.
    /// </remarks>
    public IMenuBuilderAPI OverrideSelectButton( KeyBind keyBind );

    /// <summary>
    /// Overrides the key binding for moving forward through menu options.
    /// </summary>
    /// <param name="keyBind">The key binding to use.</param>
    /// <returns>This builder for method chaining.</returns>
    /// <remarks>
    /// Supports multiple key bindings using the bitwise OR operator.
    /// Example: <c>KeyBind.W | KeyBind.Mouse1</c> allows either W or Mouse1 to move forward.
    /// </remarks>
    public IMenuBuilderAPI OverrideMoveButton( KeyBind keyBind );

    /// <summary>
    /// Overrides the key binding for moving backward through menu options.
    /// </summary>
    /// <param name="keyBind">The key binding to use.</param>
    /// <returns>This builder for method chaining.</returns>
    /// <remarks>
    /// Supports multiple key bindings using the bitwise OR operator.
    /// Example: <c>KeyBind.S | KeyBind.Mouse2</c> allows either S or Mouse2 to move backward.
    /// </remarks>
    public IMenuBuilderAPI OverrideMoveBackButton( KeyBind keyBind );

    /// <summary>
    /// Overrides the key binding for closing the menu.
    /// </summary>
    /// <param name="keyBind">The key binding to use.</param>
    /// <returns>This builder for method chaining.</returns>
    /// <remarks>
    /// Supports multiple key bindings using the bitwise OR operator.
    /// Example: <c>KeyBind.Esc | KeyBind.A</c> allows either Esc or A to close the menu.
    /// </remarks>
    public IMenuBuilderAPI OverrideExitButton( KeyBind keyBind );

    /// <summary>
    /// Builds the menu and returns the final menu instance.
    /// </summary>
    /// <returns>The built menu instance.</returns>
    public IMenuAPI Build();
}