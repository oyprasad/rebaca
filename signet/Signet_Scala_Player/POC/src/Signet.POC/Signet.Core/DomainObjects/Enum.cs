namespace Signet.Core.DomainObjects
{
    public enum ApprovalStatus
    {
        Approved        = 2,
        PendingApproval = 1,
        RecalledRequest = 4,
        Rejected        = 3,
        Unknown         = 5
    }

    public enum AutoScaleEnum
    {
        FillAndTrim = 3,
        FillExact   = 1,
        FitInside   = 2,
        Unknown     = 4
    }

    public enum MediaType
    {
        Audio         = 1,
        Flash         = 3,
        Image         = 4,
        ScalaScript   = 5,
        Unknown       = 7,
        Video         = 6,
        WindowsScript = 2
    }

    public enum MonthPeriod
    {
        Four    = 4,
        One     = 1,
        Six     = 5,
        Three   = 3,
        Two     = 2,
        Unknown = 6
    }

    public enum PlayerType
    {
        IAdeaAudioOnly    = 6,
        IAdeaHD           = 4,
        IAdeaPictureFrame = 5,
        IAdeaSD           = 3,
        Scala             = 1,
        ScalaAudioOnly    = 2,
        Unknown           = 7
    }

    public enum PlaylistItemType
    {
        Media       = 1,
        Message     = 2,
        SubPlaylist = 3,
        Unknown     = 4
    }

    public enum PlaylistPickPolicy
    {
        Sequence = 1,
        Shuffle  = 2,
        Unknown  = 3
    }

    public enum PlaylistType
    {
        Audio   = 1,
        Data    = 3,
        Media   = 2,
        Unknown = 4
    }

    public enum RecurrencePattern
    {
        Monthly = 3,
        OneTime = 1,
        Unknown = 5,
        Weekly  = 2,
        Yearly  = 4
    }

    public enum ShuffleNoRepeatEnum
    {
        NumberOfItems = 2,
        Percentage    = 1,
        Unknown       = 3
    }

    public enum TimeslotPriorityEnum
    {
        AlwaysOnTop      = 1,
        AlwaysUnderneath = 3,
        Normal           = 2,
        Unknown          = 4
    }
}