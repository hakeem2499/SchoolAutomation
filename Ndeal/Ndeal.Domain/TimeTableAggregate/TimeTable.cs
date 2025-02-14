using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using Ndeal.Domain.TimeTableAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.TimeTableAggregate.Entities;

public class Timetable : Entity<TimeTableId>
{
    private readonly List<TimetableEntry> _entries = new();

    public Timetable(TimeTableId timeTableId, DepartmentId departmentId)
        : base(timeTableId)
    {
        DepartmentId = departmentId ?? throw new ArgumentNullException(nameof(departmentId));
    }

    public DepartmentId DepartmentId { get; private set; }
    public IReadOnlyCollection<TimetableEntry> Entries => _entries.AsReadOnly();

    public static Timetable Create(DepartmentId departmentId)
    {
        return new Timetable(TimeTableId.NewTimeTableId(), departmentId);
    }

    public void AddEntry(CourseId courseId, TeacherId teacherId, RoomId roomId, TimeRange timeRange)
    {
        if (HasConflict(teacherId, roomId, timeRange))
        {
            throw new InvalidOperationException(
                "Timetable entry conflicts with an existing entry."
            );
        }

        var entry = TimetableEntry.Create(Id, courseId, teacherId, roomId, timeRange);
        _entries.Add(entry);
    }

    public void UpdateEntry(
        TimetableEntryId entryId,
        CourseId courseId,
        TeacherId teacherId,
        RoomId roomId,
        TimeRange timeRange
    )
    {
        TimetableEntry entry = GetEntry(entryId);

        if (HasConflict(teacherId, roomId, timeRange, entryId))
        {
            throw new InvalidOperationException(
                "Timetable entry conflicts with an existing entry."
            );
        }

        entry.Update(courseId, teacherId, roomId, timeRange);
    }

    public void RemoveEntry(TimetableEntryId entryId)
    {
        TimetableEntry entry = GetEntry(entryId);
        _entries.Remove(entry);
    }

    private bool HasConflict(
        TeacherId teacherId,
        RoomId roomId,
        TimeRange timeRange,
        TimetableEntryId? excludeEntryId = null
    )
    {
        return _entries.Any(entry =>
            (entry.TeacherId == teacherId || entry.RoomId == roomId)
            && entry.TimeRange.Overlaps(timeRange)
            && entry.Id != (excludeEntryId ?? entry.Id)
        );
    }

    private TimetableEntry GetEntry(TimetableEntryId entryId)
    {
        return _entries.SingleOrDefault(x => x.Id == entryId)
            ?? throw new InvalidOperationException($"Timetable entry with ID {entryId} not found.");
    }
}
