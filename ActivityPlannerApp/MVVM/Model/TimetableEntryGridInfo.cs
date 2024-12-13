namespace ActivityPlannerApp.MVVM.Model
{
    internal class TimetableEntryGridInfo
    {
        public TimetableEntry TimetableEntry { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int RowSpan { get; set; }

        public TimetableEntryGridInfo(TimetableEntry timetableEntry, int row, int column, int rowSpan = 1)
        {
            TimetableEntry = timetableEntry;
            Row = row;
            Column = column;
            RowSpan = rowSpan;
        }
    }
}
