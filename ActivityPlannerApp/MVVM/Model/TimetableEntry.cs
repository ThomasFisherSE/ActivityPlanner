using ActivityPlannerApp.Core;
using System.Windows.Media;

namespace ActivityPlannerApp.MVVM.Model
{
    internal class TimetableEntry : ObservableObject
    {
        private TimeSlot _timeSlot;
        private string _content = string.Empty;

        public TimeSlot TimeSlot
        {
            get => _timeSlot;
            set
            {
                _timeSlot = value;
                OnPropertyChanged();
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public Color Color
        {
            get => Brush.Color;
            set
            {
                Brush.Color = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush Brush { get; } = new SolidColorBrush();

        public TimetableEntry(TimeSlot timeSlot, string content, Color color)
        {
            TimeSlot = timeSlot;
            Content = content;
            Color = color;
        }

        #region SfScheduler Appointment Properties (*Prototyping Only*)
        // TODO: These properties just make it easier to work with SfScheduler, will bind to existing properties later
        public DateTime From
        {
            get => TimeSlot.Start;
            set
            {
                var updatedTimeSlot = TimeSlot;
                updatedTimeSlot.Date = DateOnly.FromDateTime(value);

                var updatedTimeRange = updatedTimeSlot.TimeRange;
                updatedTimeRange.Start = TimeOnly.FromDateTime(value);

                updatedTimeSlot.TimeRange = updatedTimeRange;
                TimeSlot = updatedTimeSlot;

                OnPropertyChanged();
            }
        }

        public DateTime To
        {
            get => TimeSlot.End;
            set
            {
                var updatedTimeSlot = TimeSlot;
                updatedTimeSlot.Date = DateOnly.FromDateTime(value);

                var updatedTimeRange = updatedTimeSlot.TimeRange;
                updatedTimeRange.End = TimeOnly.FromDateTime(value);

                updatedTimeSlot.TimeRange = updatedTimeRange;
                TimeSlot = updatedTimeSlot;

                OnPropertyChanged();
            }
        }
        #endregion
    }
}
