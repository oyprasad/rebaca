namespace Signet.CM.EntityTranslator
{
    using System;
    using System.Collections.Generic;
    using Signet.Core.DomainObjects;
    using Channel = Signet.CM.Service.Stubs.Channel;
    using Media = Signet.CM.Service.Stubs.Media;
    using Player = Signet.CM.Service.Stubs.Player;
    using Playlist = Signet.CM.Service.Stubs.Playlist;

    public static class EnumTranslatorExtensions
    {
        public static AutoScaleEnum ToEntityEnum(this Channel.autoScaleEnum value)
        {
            AutoScaleEnum retValue = AutoScaleEnum.Unknown;
            switch (value)
            {
                case Signet.CM.Service.Stubs.Channel.autoScaleEnum.FILL_EXACT:
                    retValue = AutoScaleEnum.FillExact;
                    break;
                case Signet.CM.Service.Stubs.Channel.autoScaleEnum.FIT_INSIDE:
                    retValue = AutoScaleEnum.FitInside;
                    break;
                case Signet.CM.Service.Stubs.Channel.autoScaleEnum.FILL_AND_TRIM:
                    retValue = AutoScaleEnum.FillAndTrim;
                    break;
                default:
                    break;
            }

            return retValue;
        }

        public static RecurrencePattern ToEntityEnum(this Channel.recurrencePatternEnum value)
        {
            RecurrencePattern retValue = RecurrencePattern.Unknown;

            switch (value)
            {
                case Signet.CM.Service.Stubs.Channel.recurrencePatternEnum.ONE_TIME:
                    retValue = RecurrencePattern.OneTime;
                    break;
                case Signet.CM.Service.Stubs.Channel.recurrencePatternEnum.WEEKLY:
                    retValue = RecurrencePattern.Weekly;
                    break;
                case Signet.CM.Service.Stubs.Channel.recurrencePatternEnum.MONTHLY:
                    retValue = RecurrencePattern.Monthly;
                    break;
                case Signet.CM.Service.Stubs.Channel.recurrencePatternEnum.YEARLY:
                    retValue = RecurrencePattern.Yearly;
                    break;
                default:
                    break;
            }

            return retValue;
        }

        public static MonthPeriod ToEntityEnum(this Channel.monthPeriodeEnum value)
        {
            MonthPeriod retValue = MonthPeriod.Unknown;

            switch (value)
            {
                case Signet.CM.Service.Stubs.Channel.monthPeriodeEnum.MONTH_1:
                    retValue = MonthPeriod.One;
                    break;
                case Signet.CM.Service.Stubs.Channel.monthPeriodeEnum.MONTH_2:
                    retValue = MonthPeriod.Two;
                    break;
                case Signet.CM.Service.Stubs.Channel.monthPeriodeEnum.MONTH_3:
                    retValue = MonthPeriod.Three;
                    break;
                case Signet.CM.Service.Stubs.Channel.monthPeriodeEnum.MONTH_4:
                    retValue = MonthPeriod.Four;
                    break;
                case Signet.CM.Service.Stubs.Channel.monthPeriodeEnum.MONTH_6:
                    retValue = MonthPeriod.Six;
                    break;
                default:
                    break;
            }

            return retValue;
        }

        public static PlaylistPickPolicy ToEntityEnum(this Playlist.playlistPickPolicyEnum value)
        {
            PlaylistPickPolicy retValue = PlaylistPickPolicy.Unknown;

            switch (value)
            {
                case Signet.CM.Service.Stubs.Playlist.playlistPickPolicyEnum.SEQUENCE:
                    retValue = PlaylistPickPolicy.Sequence;
                    break;
                case Signet.CM.Service.Stubs.Playlist.playlistPickPolicyEnum.SHUFFLE:
                    retValue = PlaylistPickPolicy.Shuffle;
                    break;
                default:
                    break;
            }

            return retValue;
        }

        public static ShuffleNoRepeatEnum ToEntityEnum(this Playlist.shuffleNoRepeatEnum value)
        {
            ShuffleNoRepeatEnum retValue = ShuffleNoRepeatEnum.Unknown;

            switch (value)
            {
                case Signet.CM.Service.Stubs.Playlist.shuffleNoRepeatEnum.PERCENTAGE:
                    retValue = ShuffleNoRepeatEnum.Percentage;
                    break;
                case Signet.CM.Service.Stubs.Playlist.shuffleNoRepeatEnum.NUM_OF_ITEMS:
                    retValue = ShuffleNoRepeatEnum.NumberOfItems;
                    break;
                default:
                    break;
            }

            return retValue;
        }

        public static PlaylistType ToEntityEnum(this Playlist.playlistTypeEnum value)
        {
            PlaylistType retValue = PlaylistType.Unknown;

            switch (value)
            {
                case Signet.CM.Service.Stubs.Playlist.playlistTypeEnum.AUDIO_PLAYLIST:
                    retValue = PlaylistType.Audio;
                    break;
                case Signet.CM.Service.Stubs.Playlist.playlistTypeEnum.MEDIA_PLAYLIST:
                    retValue = PlaylistType.Media;
                    break;
                case Signet.CM.Service.Stubs.Playlist.playlistTypeEnum.DATA_PLAYLIST:
                    retValue = PlaylistType.Data;
                    break;
                case Signet.CM.Service.Stubs.Playlist.playlistTypeEnum.UNKNOWN:
                    break;
                default:
                    break;
            }

            return retValue;
        }

        public static PlaylistItemType ToEntityEnum(this Playlist.playlistItemTypeEnum value)
        {
            PlaylistItemType retValue = PlaylistItemType.Unknown;

            switch (value)
            {
                case Signet.CM.Service.Stubs.Playlist.playlistItemTypeEnum.MEDIA_ITEM:
                    retValue = PlaylistItemType.Media;
                    break;
                case Signet.CM.Service.Stubs.Playlist.playlistItemTypeEnum.MESSAGE:
                    retValue = PlaylistItemType.Message;
                    break;
                case Signet.CM.Service.Stubs.Playlist.playlistItemTypeEnum.SUB_PLAYLIST:
                    retValue = PlaylistItemType.SubPlaylist;
                    break;
                default:
                    break;
            }

            return retValue;
        }

        public static MediaType ToEntityEnum(this Media.mediaTypeEnum value)
        {
            MediaType retValue = MediaType.Unknown;

            switch (value)
            {
                case Signet.CM.Service.Stubs.Media.mediaTypeEnum.AUDIO:
                    retValue = MediaType.Audio;
                    break;
                case Signet.CM.Service.Stubs.Media.mediaTypeEnum.WINDOWS_SCRIPT:
                    retValue = MediaType.WindowsScript;
                    break;
                case Signet.CM.Service.Stubs.Media.mediaTypeEnum.FLASH:
                    retValue = MediaType.Flash;
                    break;
                case Signet.CM.Service.Stubs.Media.mediaTypeEnum.IMAGE:
                    retValue = MediaType.Image;
                    break;
                case Signet.CM.Service.Stubs.Media.mediaTypeEnum.SCALA_SCRIPT:
                    retValue = MediaType.ScalaScript;
                    break;
                case Signet.CM.Service.Stubs.Media.mediaTypeEnum.VIDEO:
                    retValue = MediaType.Video;
                    break;
                case Signet.CM.Service.Stubs.Media.mediaTypeEnum.UNKNOWN:
                    break;
                default:
                    break;
            }

            return retValue;
        }

        public static ApprovalStatus ToEntityEnum(this Media.approvalStatusEnum value)
        {
            ApprovalStatus retValue = ApprovalStatus.Unknown;

            switch (value)
            {
                case Signet.CM.Service.Stubs.Media.approvalStatusEnum.PENDING_APPROVAL:
                    retValue = ApprovalStatus.PendingApproval;
                    break;
                case Signet.CM.Service.Stubs.Media.approvalStatusEnum.APPROVED:
                    retValue = ApprovalStatus.Approved;
                    break;
                case Signet.CM.Service.Stubs.Media.approvalStatusEnum.REJECTED:
                    retValue = ApprovalStatus.Rejected;
                    break;
                case Signet.CM.Service.Stubs.Media.approvalStatusEnum.RECALLED_REQUEST:
                    retValue = ApprovalStatus.RecalledRequest;
                    break;
                default:
                    break;
            }

            return retValue;
        }

        public static List<DayOfWeek> ToEntityEnumArray(this Playlist.weekdayEnum?[] values)
        {
            List<DayOfWeek> weekdays = new List<DayOfWeek>();

            foreach (var day in values)
            {
                switch (day)
                {
                    case Signet.CM.Service.Stubs.Playlist.weekdayEnum.MONDAY:
                        weekdays.Add(DayOfWeek.Monday);
                        break;
                    case Signet.CM.Service.Stubs.Playlist.weekdayEnum.TUESDAY:
                        weekdays.Add(DayOfWeek.Tuesday);
                        break;
                    case Signet.CM.Service.Stubs.Playlist.weekdayEnum.WEDNESDAY:
                        weekdays.Add(DayOfWeek.Wednesday);
                        break;
                    case Signet.CM.Service.Stubs.Playlist.weekdayEnum.THURSDAY:
                        weekdays.Add(DayOfWeek.Thursday);
                        break;
                    case Signet.CM.Service.Stubs.Playlist.weekdayEnum.FRIDAY:
                        weekdays.Add(DayOfWeek.Friday);
                        break;
                    case Signet.CM.Service.Stubs.Playlist.weekdayEnum.SATURDAY:
                        weekdays.Add(DayOfWeek.Saturday);
                        break;
                    case Signet.CM.Service.Stubs.Playlist.weekdayEnum.SUNDAY:
                        weekdays.Add(DayOfWeek.Sunday);
                        break;
                    default:
                        break;
                }
            }

            return weekdays;
        }

        public static PlayerType ToEntityEnum(this Player.playerTypeEnum values)
        {
            PlayerType retValue = PlayerType.Unknown;

            switch (values)
            {
                case Signet.CM.Service.Stubs.Player.playerTypeEnum.SCALA:
                    retValue = PlayerType.Scala;
                    break;
                case Signet.CM.Service.Stubs.Player.playerTypeEnum.SCALA_AUDIO_ONLY:
                    retValue = PlayerType.ScalaAudioOnly;
                    break;
                case Signet.CM.Service.Stubs.Player.playerTypeEnum.IADEA_SD:
                    retValue = PlayerType.IAdeaSD;
                    break;
                case Signet.CM.Service.Stubs.Player.playerTypeEnum.IADEA_HD:
                    retValue = PlayerType.IAdeaHD;
                    break;
                case Signet.CM.Service.Stubs.Player.playerTypeEnum.IADEA_PICTURE_FRAME:
                    retValue = PlayerType.IAdeaPictureFrame;
                    break;
                case Signet.CM.Service.Stubs.Player.playerTypeEnum.IADEA_AUDIO_ONLY:
                    retValue = PlayerType.IAdeaAudioOnly;
                    break;
                default:
                    break;
            }

            return retValue;
        }
    }
}