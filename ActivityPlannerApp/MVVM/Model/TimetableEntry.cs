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
                OnPropertyChanged(nameof(TimeSlot));
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public Color Color
        {
            get => Brush.Color;
            set
            {
                Brush.Color = value;
                OnPropertyChanged(nameof(Color));
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
