using ActivityPlannerApp.Core;
using System.Windows.Media;

namespace ActivityPlannerApp.MVVM.Model
{
    internal class TimetableEntry : ObservableObject
    {
        private const string DefaultBackgroundColorHex = "#2a2e37";

        private TimeSlot timeSlot;
        private string content = string.Empty;
        Brush color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DefaultBackgroundColorHex));

        public TimeSlot TimeSlot
        {
            get => timeSlot;
            set
            {
                timeSlot = value;
                OnPropertyChanged(nameof(TimeSlot));
            }
        }

        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public Brush Color
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public TimetableEntry(TimeSlot timeSlot, string content)
        {
            TimeSlot = timeSlot;
            Content = content;
        }

        #region SfScheduler Appointment Properties (*Prototyping Only*)
        // TODO: These properties just make it easier to work with SfScheduler, will bind to existing properties later
        public DateTime From
        {
            get => TimeSlot.Start;
            set
            {
                TimeSlot updatedTimeSlot = TimeSlot;
                updatedTimeSlot.Date = DateOnly.FromDateTime(value);

                TimeRange updatedTimeRange = updatedTimeSlot.TimeRange;
                updatedTimeRange.Start = TimeOnly.FromDateTime(value);

                updatedTimeSlot.TimeRange = updatedTimeRange;
                TimeSlot = updatedTimeSlot;

                OnPropertyChanged(nameof(From));
            }
        }

        public DateTime To
        {
            get => TimeSlot.End;
            set
            {
                TimeSlot updatedTimeSlot = TimeSlot;
                updatedTimeSlot.Date = DateOnly.FromDateTime(value);

                TimeRange updatedTimeRange = updatedTimeSlot.TimeRange;
                updatedTimeRange.End = TimeOnly.FromDateTime(value);

                updatedTimeSlot.TimeRange = updatedTimeRange;
                TimeSlot = updatedTimeSlot;

                OnPropertyChanged(nameof(To));
            }
        }
        #endregion
    }
}
