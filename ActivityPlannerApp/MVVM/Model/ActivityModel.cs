﻿using System.Windows.Media;

namespace ActivityPlannerApp.MVVM.Model
{
    public class ActivityModel
    {
        /// <summary>
        /// The activity's unique identifier
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The name of the activity
        /// </summary>
        public required string ActivityName { get; set; }

        /// <summary>
        /// The source of the icon image
        /// </summary>
        public string IconImageSource { get; set; } = "./Icons/star.png";

        /// <summary>
        /// The location of the activity
        /// </summary>
        public LocationModel? ActivityLocation { get; set; }

        public Color Color => ActivityLocation?.Color ?? LocationModel.DefaultLocationColor;
    }
}
