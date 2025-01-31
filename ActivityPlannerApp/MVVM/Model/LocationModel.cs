using System.Windows.Media;

namespace ActivityPlannerApp.MVVM.Model
{
    public class LocationModel
    {
        private const string DefaultLocationColorHex = "#2a2e37";

        /// <summary>
        /// The default color to associate with a location
        /// </summary>
        public static readonly Color DefaultLocationColor = 
            (Color)ColorConverter.ConvertFromString(DefaultLocationColorHex);

        /// <summary>
        /// The location's unique identifier
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The name of the location
        /// </summary>
        public required string LocationName { get; set; }

        /// <summary>
        /// The <see cref="Color"/> associated with the location
        /// </summary>
        public Color Color { get; set; } = DefaultLocationColor;
    }
}
