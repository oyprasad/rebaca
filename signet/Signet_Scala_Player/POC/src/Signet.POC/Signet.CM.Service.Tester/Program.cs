namespace Signet.CM.Service.Tester
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Xml.Linq;
    using Signet.Core.Configuration;
    using Signet.Core.DomainObjects;
    using Signet.Core.Downloader;
    using Signet.Core.Extensions;
    using Signet.Core.Factory;
    using Signet.Core.Logging;
    using Signet.Core.ServiceRepository;
    using Signet.Core.Utils;
    using CommandLine;
    using CommandLine.Text;
    using System.Diagnostics;

    internal sealed class Options : CommandLineOptionsBase
    {
        [Option("p", "play", Required = false, HelpText = @"Play the content directly without downloading content or plan information from Content Manager. This option assumes that the player channel xml file and media content files are already existing under the Content subfolder in the application directory")]
        public bool PlayOnly { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }

    internal class Program
    {
        private static string httpPathFormat                                     = "http://localhost/{0}/{1}";

        private static readonly string playerUID                                 = ConfigurationManager.AppSettings["PlayerUniqueID"];
        private static readonly string VDirName                                  = ConfigurationManager.AppSettings["VDirName"];
        private static readonly bool scaleFit                                    = bool.Parse(ConfigurationManager.AppSettings["OpenPlayerInScaleFitMode"]);
        

        private static readonly object _syncRoot                                 = new object();
        private static ManualResetEvent evt                                      = new ManualResetEvent(false);
        private static int fileDownloadCompleteCount                             = 0;
        private static int mediaListingCompleted                                 = 0;
        private static int mediaCount                                            = 0;

        private static readonly string folderPath                                = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Content");
        private static ConcurrentDictionary<string, int> mediaDownloadPercentMap = new ConcurrentDictionary<string, int>();
        private static string mediaUrl                                           = string.Empty;
        private static string localFilename                                      = string.Empty;
        private static Dictionary<int, string> mediaToLocalPathMap               = new Dictionary<int, string>();
        private static string xmlFilePath                                        = string.Empty;

        private static IChannelServiceRepository channelRepository;
        private static IMediaServiceRepository mediaRepository;
        private static IPlayerServiceRepository playerRepository;
        private static IPlaylistServiceRepository playlistRepository;
        private static IServiceConnectionConfiguration config;

        private static bool IsElevated
        {
            get
            {
                return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private static void Main(string[] args)
        {
            try
            {
                if (!IsElevated)
                {
                    throw new InvalidOperationException("This application needs elevated privileges to execute. Please run as administrator or use an elevated command prompt to run");
                }

                Bootstrapper.Init();
                
                var options = new Options();
                var parser = new CommandLineParser(new CommandLineParserSettings(Console.Out));
                if (!parser.ParseArguments(args, options))
                {
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else
                {
                    setupConfig();
                    setupServiceRepositories();

                    if (options.PlayOnly)
                    {
                        IPlayerEntity player = getPlayer();
                        IList<IPlayerDisplayEntity> playerDisplays = ServiceRepositoryFactory.GetServiceRepository<IPlayerServiceRepository>().GetAllPlayerDisplays(player.Id);
                        IChannelEntity channel = getChannelForPlayer(playerDisplays.First().ChannelId.Value);
                        validateSetup(channel);
                        ConsoleLogger.Log(LogLevel.Info, "Completed validation. Invoking player");
                        invokePlayer();
                    }
                    else
                    {
                        cleanUp(folderPath);

                        Bootstrapper.Run();

                        copyPlayerFilesToVDir();

                        string cmrooturl = config.RootUrl;

                        ConsoleLogger.Log(LogLevel.Info, "Getting all player information");

                        IPlayerEntity player = getPlayer();

                        if (player != null)
                        {
                            ConsoleLogger.Log(LogLevel.Info, "Retrieved information for player: {0}", player.Name);
                            ConsoleLogger.Log(LogLevel.Info, "Getting all player displays");

                            IList<IPlayerDisplayEntity> playerDisplays = ServiceRepositoryFactory.GetServiceRepository<IPlayerServiceRepository>().GetAllPlayerDisplays(player.Id);

                            ConsoleLogger.Log(LogLevel.Info, "Retrieved {0} player display information for player: {1}", playerDisplays.Count.ToString(), player.Name);

                            foreach (var playerDisplay in playerDisplays)
                            {
                                if (playerDisplay.ChannelId.HasValue)
                                    getChannelData(playerDisplay.ChannelId.Value, cmrooturl);
                            }

                            ConsoleLogger.Log(LogLevel.Info, "Waiting for media downloads to complete");
                            ConsoleLogger.Log(LogLevel.Info, getCurrentDownloadStatus());

                            evt.WaitOne();

                            ConsoleLogger.Log(LogLevel.Info, "Media downloads completed. Invoking player.");

                            invokePlayer();
                        }
                        else
                        {
                            ConsoleLogger.Log(LogLevel.Info, "No player display found for player with Id: {0}", playerUID);
                        }
                    }
                }

               Console.WriteLine("Completed. Press enter to exit.");
            }
            catch (Exception ex)
            {
                ConsoleLogger.Log(LogLevel.Error, ex.Message);
                Logger.Error(ex.CreateExceptionString());
                Console.WriteLine("Press enter to exit.");
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void setupConfig()
        {
            CMConfigEntry.EnvironmentOption env = (CMConfigEntry.EnvironmentOption)Enum.Parse(typeof(CMConfigEntry.EnvironmentOption), ConfigurationManager.AppSettings["CMActiveConfig"]);
            config = CMConnectionInfo.getConfig(env);

            ServiceRepositoryFactory.Configure(config);
        }

        private static void setupServiceRepositories()
        {
            channelRepository  = ServiceRepositoryFactory.GetServiceRepository<IChannelServiceRepository>();
            mediaRepository    = ServiceRepositoryFactory.GetServiceRepository<IMediaServiceRepository>();
            playerRepository   = ServiceRepositoryFactory.GetServiceRepository<IPlayerServiceRepository>();
            playlistRepository = ServiceRepositoryFactory.GetServiceRepository<IPlaylistServiceRepository>();
        }

        private static IPlayerEntity getPlayer()
        {
            IList<IPlayerEntity> players = ServiceRepositoryFactory.GetServiceRepository<IPlayerServiceRepository>().GetAllPlayers(0);
            IPlayerEntity player = players.Where(p => p.UniqueId == playerUID).First();
            return player;
        }

        private static IChannelEntity getChannelForPlayer(int channelId)
        {
            IChannelEntity channel = channelRepository.GetAllChannels(0).Where(c => c.Id == channelId).First();

            return channel;
        }

        private static void validateSetup(IChannelEntity channel)
        {
            ConsoleLogger.Log(LogLevel.Info, "Validating setup required to play");
            if (Directory.Exists(folderPath))
            {
                string xmlFileName = channel.Id.ToString() + ".xml";
                var file = Directory.GetFiles(folderPath, xmlFileName, SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (file != null)
                {
                    xmlFilePath = file;
                    validateChannelXmlFilePaths(xmlFilePath);
                }
                else
                {
                    throw new InvalidOperationException("Player xml file: {0} not found at path: {1}".FormatWith(xmlFileName, folderPath));
                }
            }
            else
            {
                throw new InvalidOperationException("Content folder not found at path: {0}".FormatWith(folderPath));
            }
        }

        private static void validateChannelXmlFilePaths(string xmlFilepath)
        {
            string httpPath = string.Empty;
            string localDiskPath = string.Empty;

            XDocument doc = XDocument.Load(xmlFilepath);
            if (doc != null)
            {
                var list = doc.Descendants().Where(x => x.Name == "media");
                if (list.Count() > 0)
                {
                    foreach (var element in list)
                    {
                        if (element.Attribute("path") != null)
                        {
                            httpPath = element.Attribute("path").Value;
                            localDiskPath = Path.Combine(folderPath, Path.GetFileName(httpPath));
                            if (!File.Exists(localDiskPath))
                            {
                                throw new InvalidOperationException("Media file: {0} for channel xml: {1} not found".FormatWith(localDiskPath, xmlFilepath));
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("Channel xml: {0} does not contain media path information".FormatWith(xmlFilepath));
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Channel xml: {0} does not contain media information".FormatWith(xmlFilepath));
                }
            }
            else
            {
                throw new InvalidOperationException("Channel xml: {0} not found".FormatWith(xmlFilepath));
            }
        }

        private static void downloadMedia(IMediaEntity media, string cmrooturl)
        {
            DownloadManager downloadManager = initDownloadManager(config);
            ThreadPool.QueueUserWorkItem((wc) =>
            {
                ConsoleLogger.Log(LogLevel.Info, "Initialised download for {0}", media.Name);
                mediaUrl = string.Concat(cmrooturl, media.WebDavDownloadPath);
                localFilename = getFileName(mediaUrl);
                mediaToLocalPathMap.Add(media.Id, httpPathFormat.FormatWith(VDirName, Path.GetFileName(localFilename)));
                downloadManager.DownloadFileAsync(mediaUrl, localFilename);
            });
        }

        private static void getChannelData(int channelId, string cmrooturl)
        {
            IFramesetEntity frameset;
            IList<IFrameEntity> frames;
            IList<ITimeslotEntity> timeslots;
            IPlaylistEntity playlist;
            IList<IPlaylistItemEntity> playlistItems;
            IList<ITimeScheduleEntity> timeSchedules;
            IMediaEntity media;

            ConsoleLogger.Log(LogLevel.Info, "Getting channel information");

            IChannelEntity channel = getChannelForPlayer(channelId);

            ConsoleLogger.Log(LogLevel.Info, "Retrieved information for channel : {0}", channel.Name);

            frameset = channelRepository.GetFramesetForChannel(channel.Id);

            ConsoleLogger.Log(LogLevel.Info, "Retrieved frameset information for {0}", channel.Name);

            frameset.ChannelId = channel.Id;
            channel.Frameset = frameset;

            ConsoleLogger.Log(LogLevel.Info, "Getting all frame information for {0}", channel.Name);

            frames = channelRepository.GetAllFramesForChannel(channel.Id);

            ConsoleLogger.Log(LogLevel.Info, "Retrieved information for {0} frames for {1}", frames.Count.ToString(), channel.Name);

            frameset.Frames = frames;
            channel.Frames = frames;

            foreach (IFrameEntity frame in frames)
            {
                frame.ChannelId = channel.Id;

                ConsoleLogger.Log(LogLevel.Info, "Getting all timeslots for frame: {0} in channel: {1}", frame.Name, channel.Name);

                timeslots = channelRepository.GetAllTimeslotsForChannelAndFrame(channel.Id, frame.Id);

                ConsoleLogger.Log(LogLevel.Info, "Retrieved information for {0} timeslots", timeslots.Count.ToString());

                frame.TimeSlots = timeslots;

                foreach (ITimeslotEntity timeslot in timeslots)
                {
                    timeslot.FrameId = frame.Id;
                    timeslot.Frame = frame;

                    ConsoleLogger.Log(LogLevel.Info, "Getting playlist information for frame: {0} in channel: {1}", frame.Name, channel.Name);

                    playlist = playlistRepository.GetPlaylistById(timeslot.PlaylistId.Value);

                    ConsoleLogger.Log(LogLevel.Info, "Retrieved playlist information");

                    playlist.TimeslotId = timeslot.Id;
                    playlist.Timeslot = timeslot;

                    timeslot.PlaylistId = playlist.Id;
                    timeslot.Playlist = playlist;

                    ConsoleLogger.Log(LogLevel.Info, "Getting playlist items for playlist: {0}", playlist.Name);

                    playlistItems = playlistRepository.GetAllPlaylistItems(playlist.Id);

                    ConsoleLogger.Log(LogLevel.Info, "Retrieved information for {0} items in playlist: {1}", playlistItems.Count.ToString(), playlist.Name);

                    playlist.Items = playlistItems;

                    foreach (IPlaylistItemEntity playListItem in playlistItems)
                    {
                        playListItem.PlaylistId = playlist.Id;
                        playListItem.Playlist = playlist;

                        ConsoleLogger.Log(LogLevel.Info, "Getting timeschedule information for playlist item: {0}", playListItem.ItemType.Value.ToString());

                        timeSchedules = playlistRepository.GetAllTimeSchedulesForPlaylistItem(playListItem.Id);

                        ConsoleLogger.Log(LogLevel.Info, "Retrieved timeschedule information");

                        playListItem.TimeSchedules = timeSchedules;

                        foreach (ITimeScheduleEntity timeScheduleEntity in timeSchedules)
                        {
                            timeScheduleEntity.PlayListItemId = playListItem.Id;
                            timeScheduleEntity.PlaylistItem = playListItem;
                        }

                        if (((PlaylistItemType)playListItem.ItemType) == PlaylistItemType.Media)
                        {
                            media = mediaRepository.GetMediaById(playListItem.MediaId.Value);
                            Interlocked.Increment(ref mediaCount);
                            downloadMedia(media, cmrooturl);

                            playListItem.MediaId = media.Id;
                            playListItem.Media = media;
                        }
                    }
                }
            }

            Interlocked.Exchange(ref mediaListingCompleted, 1);

            ConsoleLogger.Log(LogLevel.Info, "Writing xml for {0}", channel.Name);

            var xml = getXMLForChannel(channel, mediaToLocalPathMap);
            xmlFilePath = getXmlFilename(channel);

            using (FileStream fs = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write))
            {
                xml.Save(fs);
            }

            ConsoleLogger.Log(LogLevel.Info, "Saved xml for {0}", channel.Name);
        }

        private static void copyPlayerFilesToVDir()
        {
            string playerFilesFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Player");

            foreach (var dir in Directory.GetDirectories(playerFilesFolderPath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(folderPath + dir.Substring(playerFilesFolderPath.Length));
            }

            foreach (var file in Directory.GetFiles(playerFilesFolderPath, "*", SearchOption.AllDirectories))
            {
                File.Copy(file, folderPath + file.Substring(playerFilesFolderPath.Length), true);
            }
        }

        private static void downloadManager_DownloadError(object sender, DownloadErrorEventArgs e)
        {
            Logger.Error("Error downloading {0} : {1}".FormatWith(e.DownloadedUrl.AbsoluteUri, e.Error.Message));
            ConsoleLogger.Log(LogLevel.Error, "Error downloading {0} : {1}", e.DownloadedUrl.AbsoluteUri, e.Error.Message);

            Interlocked.Increment(ref fileDownloadCompleteCount);
            if (Interlocked.CompareExchange(ref mediaListingCompleted, 1, 1) == 1 && 
                Interlocked.CompareExchange(ref fileDownloadCompleteCount, mediaCount, mediaCount) == mediaCount)
            {
                evt.Set();
            }
        }

        private static void downloadManager_DownloadFileCompleted(object sender, DownloadFileEventArgs e)
        {
            Logger.Info("Downloaded {0} to {1}".FormatWith(e.DownloadedUrl, e.Filename));
            ConsoleLogger.Log(LogLevel.Info, "Downloaded {0} to {1}", e.DownloadedUrl, e.Filename);

            Interlocked.Increment(ref fileDownloadCompleteCount);
            if (Interlocked.CompareExchange(ref mediaListingCompleted, 1, 1) == 1 &&
                Interlocked.CompareExchange(ref fileDownloadCompleteCount, mediaCount, mediaCount) == mediaCount)
            {
                evt.Set();
            }
        }

        private static void downloadManager_DownloadProgressChanged(object sender, DownloadProgressEventArgs e)
        {
            string filename = Path.GetFileName(e.DownloadedUrl.LocalPath);
            lock (_syncRoot)
            {
                if (mediaDownloadPercentMap.ContainsKey(filename) && (mediaDownloadPercentMap[filename] < e.PercentComplete))
                {
                    if (e.PercentComplete % 10 == 0)
                        ConsoleLogger.Log(LogLevel.Info, "Downloaded {0} % of {1}", e.PercentComplete.ToString(), filename);
                    else
                        Logger.Info("Downloaded {0} % of {1}".FormatWith(e.PercentComplete, filename));
                }
                mediaDownloadPercentMap[filename] = e.PercentComplete;
            }
        }

        private static string getFileName(string url)
        {
            string filename = Path.GetFileName(new Uri(url).LocalPath);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, filename);
        }

        private static DownloadManager initDownloadManager(IServiceConnectionConfiguration config)
        {
            DownloadManager downloadManager          = new DownloadManager(config.Username, config.Password);
            downloadManager.DownloadFileCompleted   += new EventHandler<DownloadFileEventArgs>(Program.downloadManager_DownloadFileCompleted);
            downloadManager.DownloadProgressChanged += new EventHandler<DownloadProgressEventArgs>(Program.downloadManager_DownloadProgressChanged);
            downloadManager.DownloadError           += new EventHandler<DownloadErrorEventArgs>(Program.downloadManager_DownloadError);
            return downloadManager;
        }

        private static string getXmlFilename(IChannelEntity channel)
        {
            return Path.Combine(folderPath, channel.Id.ToString() + ".xml");
        }

        private static string getCurrentDownloadStatus()
        {
            StringBuilder sbr = new StringBuilder("Download status of media items:");
            sbr.AppendLine();
            string formatStr = "{0} - {1}% complete";
            if (mediaDownloadPercentMap.Count > 0)
            {
                foreach (var item in mediaDownloadPercentMap)
                {
                    sbr.AppendLine(formatStr.FormatWith(item.Key, item.Value));
                }
            }

            return sbr.ToString();
        }

        private static void invokePlayer()
        {
            try
            {
                string pathFormat = @"""{0}""";
                string env = @"{0}\Google\Chrome\Application\chrome.exe";
                string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var path = env.FormatWith(localAppDataPath);
                var xmlFileName = Path.GetFileName(xmlFilePath);
                var playerHtmlPath = httpPathFormat.FormatWith(VDirName, "signet.html");
                var playerBatchFile = "PlayerLaunch.bat";

                Process p = new Process();
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = playerBatchFile;
                ps.Arguments = pathFormat.FormatWith(path) + " " + playerHtmlPath + " " + xmlFileName + " " + scaleFit.ToString();
                Logger.Info(ps.Arguments);
                ps.UseShellExecute = false;
                ps.CreateNoWindow = false;
                p.StartInfo = ps;
                p.Start();
            }
            catch (Exception ex)
            {
                ConsoleLogger.Log(LogLevel.Error, ex.Message);
            }
        }

        private static XElement getXMLForChannel(IChannelEntity channel, Dictionary<int, string> mediaToLocalPathMap)
        {
            var xml = new XElement("channel",
                        new XAttribute("id", channel.Id),
                        new XAttribute("name", channel.Name),
                            new XElement("frameset",
                                new XAttribute("id", channel.Frameset.Id),
                                new XAttribute("name", channel.Frameset.Name),
                                new XAttribute("screenWidth", (channel.Frameset.ScreenSize.HasValue) ? (object)channel.Frameset.ScreenSize.Value.Width : "null"),
                                new XAttribute("screenHeight", (channel.Frameset.ScreenSize.HasValue) ? (object)channel.Frameset.ScreenSize.Value.Height : "null"),
                                    new XElement("frames",
                                    from f in channel.Frames
                                    select
                                        new XElement("frame",
                                            new XAttribute("id", f.Id),
                                            new XAttribute("name", f.Name),
                                            new XAttribute("x", (f.Position.HasValue) ? (object)f.Position.Value.X : "null"),
                                            new XAttribute("y", (f.Position.HasValue) ? (object)f.Position.Value.Y : "null"),
                                            new XAttribute("width", (f.Size.HasValue) ? (object)f.Size.Value.Width : "null"),
                                            new XAttribute("height", (f.Size.HasValue) ? (object)f.Size.Value.Height : "null"),
                                            new XAttribute("color", (f.Color.HasValue) ? ColorTranslator.ToHtml(f.Color.Value) : ColorTranslator.ToHtml(Color.Empty)),
                                                new XElement("timeslots",
                                                from t in f.TimeSlots
                                                select
                                                    new XElement("timeslot",
                                                        new XAttribute("id", t.Id),
                                                        new XAttribute("name", t.Name),
                                                        new XAttribute("start", t.StartDateTime.DisplayAsDateAndTime()),
                                                        new XAttribute("end", t.EndDateTime.DisplayAsDateAndTime()),
                                                            new XElement("playlist",
                                                                new XAttribute("id", t.Playlist.Id),
                                                                new XAttribute("name", t.Playlist.Name),
                                                                    new XElement("medias",
                                                                    from m in t.Playlist.Items
                                                                    select
                                                                        new XElement("media",
                                                                            new XAttribute("PlayOrder", m.SortOrder),
                                                                            new XAttribute("name", m.Media.Name),
                                                                            new XAttribute("duration", (m.Media.Type.HasValue && m.Media.Type.Value == MediaType.Image) ? (object)m.DurationInSeconds : "null"),
                                                                            new XAttribute("type", (m.Media.Type.HasValue) ? m.Media.Type.Value : default(MediaType?)),
                                                                            new XAttribute("path", mediaToLocalPathMap.ContainsKey(m.Media.Id) ? mediaToLocalPathMap[m.Media.Id] : "null"),
                                                                            new XAttribute("start", (m.Media.Type.HasValue && m.Media.Type.Value == MediaType.Video) ? ((m.StartTime.HasValue) ? m.StartTime.Value.GetTimeInterval() : "null") : "null"),
                                                                            new XAttribute("end", (m.Media.Type.HasValue && m.Media.Type.Value == MediaType.Video) ? ((m.EndTime.HasValue) ? m.EndTime.Value.GetTimeInterval() : "null") : "null"),
                                                                            new XAttribute("width", (m.Media.Width.HasValue) ? (object)m.Media.Width : "null"),
                                                                            new XAttribute("height", (m.Media.Height.HasValue) ? (object)m.Media.Height : "null")
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        );

            return xml;
        }

        private static void cleanUp(string directoryPath)
        {
            if (!string.IsNullOrEmpty(directoryPath) && Directory.Exists(directoryPath))
            {
                DirectoryInfo parentDirectory = new DirectoryInfo(directoryPath);
                clearReadOnly(parentDirectory);
            }

            if (Directory.Exists(directoryPath))
                Directory.Delete(directoryPath, true);
        }

        private static void clearReadOnly(DirectoryInfo parentDirectory)
        {
            if (parentDirectory != null)
            {
                parentDirectory.Attributes = FileAttributes.Normal;
                foreach (FileInfo fi in parentDirectory.GetFiles())
                {
                    fi.Attributes = FileAttributes.Normal;
                }
                foreach (DirectoryInfo di in parentDirectory.GetDirectories())
                {
                    clearReadOnly(di);
                }
            }
        }
    }
}