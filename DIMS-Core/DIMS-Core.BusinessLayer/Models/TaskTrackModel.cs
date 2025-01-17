using System;

namespace DIMS_Core.BusinessLayer.Models
{
    public class TaskTrackModel
    {
        public int TaskTrackId { get; set; }
        
        public int UserTaskId { get; set; }
        
        public DateTime TrackDate { get; set; }
        
        public string TrackNote { get; set; }

        public UserTaskModel UserTask { get; set; }
    }
}