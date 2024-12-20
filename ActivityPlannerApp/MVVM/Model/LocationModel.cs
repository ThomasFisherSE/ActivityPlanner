namespace ActivityPlannerApp.MVVM.Model
{
    public class LocationModel
    {
        /// <summary>
        /// The location's unique identifier
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The name of the location
        /// </summary>
        public required string LocationName { get; set; }
    }
}
