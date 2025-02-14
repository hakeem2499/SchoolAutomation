using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using Ndeal.Domain.TimeTableAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.TimeTableAggregate.Entities;

public class TimetableEntry : Entity<TimetableEntryId>
{
    public TimetableEntry(
        TimetableEntryId entryId,
        TimeTableId timetableId,
        CourseId courseId,
        TeacherId teacherId,
        RoomId roomId,
        TimeRange timeRange
    )
        : base(entryId)
    {
        TimetableId = timetableId;
        CourseId = courseId;
        TeacherId = teacherId;
        RoomId = roomId;
        TimeRange = timeRange;
    }

    public TimeTableId TimetableId { get; private set; }
    public CourseId CourseId { get; private set; }
    public TeacherId TeacherId { get; private set; }
    public RoomId RoomId { get; private set; }
    public TimeRange TimeRange { get; private set; }

    internal static TimetableEntry Create(
        TimeTableId timetableId,
        CourseId courseId,
        TeacherId teacherId,
        RoomId roomId,
        TimeRange timeRange
    )
    {
        return new TimetableEntry(
            TimetableEntryId.NewTimetableEntryId(),
            timetableId,
            courseId,
            teacherId,
            roomId,
            timeRange
        );
    }

    internal void Update(CourseId courseId, TeacherId teacherId, RoomId roomId, TimeRange timeRange)
    {
        CourseId = courseId ?? throw new ArgumentNullException(nameof(courseId));
        TeacherId = teacherId ?? throw new ArgumentNullException(nameof(teacherId));
        RoomId = roomId ?? throw new ArgumentNullException(nameof(roomId));
        TimeRange = timeRange ?? throw new ArgumentNullException(nameof(timeRange));
    }
}


