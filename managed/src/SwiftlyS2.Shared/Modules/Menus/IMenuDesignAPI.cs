using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Menus;

public interface IMenuDesignAPI
{
    /// <summary>
    /// Sets how many menu items can be displayed on screen at once. Menus with more items will be paginated.
    /// </summary>
    /// <param name="count">Maximum visible items (clamped between 1 and 5).</param>
    /// <returns>This design interface for method chaining.</returns>
    /// <remarks>
    /// Values outside the range of 1-5 will be automatically clamped, and a warning will be logged.
    /// </remarks>
    public IMenuDesignAPI MaxVisibleItems( int count = 5 );

    /// <summary>
    /// Sets the color theme for the menu display.
    /// </summary>
    /// <param name="color">The rendering color for the menu.</param>
    /// <returns>This design interface for method chaining.</returns>
    public IMenuDesignAPI SetColor( Color color );
}