using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using UnityEngine;

/// <summary>
/// Class used for logging.
/// Speeds up performance by checking if relevant log level is enabled BEFORE
/// calling the logging method. Also makes log statements cleaner outside of this class.
/// </summary>
public class Logger
{
    private readonly ILog _log;
    private const string ConfigFile = "log4netconfig.xml";

    public Logger(MethodBase methodBase)
    {
        _log = LogManager.GetLogger(methodBase.DeclaringType);


        var configPath = Application.dataPath +
                             Path.DirectorySeparatorChar +
                             "StreamingAssets" +
                             Path.DirectorySeparatorChar +
                             ConfigFile;
        Configure(configPath);
    }

    public Logger(string loggerName)
    {
        _log = LogManager.GetLogger(loggerName);

        var configPath = Application.dataPath +
                             Path.DirectorySeparatorChar +
                             "Plugins" +
                             Path.DirectorySeparatorChar +
                             ConfigFile;
        Configure(configPath);
    }

    private static void Configure(string configFile)
    {
        var fileInfo = new FileInfo(configFile);
        XmlConfigurator.ConfigureAndWatch(fileInfo);
    }

    #region Logging Methods

    /// <summary>
    /// Log on debug level if debug is enabled
    /// </summary>
    /// <param name="message"></param>
    public void Debug(object message)
    {
        if (_log.IsDebugEnabled)
        {
            _log.Debug(message);
        }
    }
    /// <summary>
    /// Log on info level if info is enabled
    /// </summary>
    /// <param name="message"></param>
    public void Info(object message)
    {
        if (_log.IsInfoEnabled)
        {
            _log.Info(message);
        }
    }
    /// <summary>
    /// Log on warn level if warn is enabled
    /// </summary>
    /// <param name="message"></param>
    public void Warn(object message)
    {
        if (_log.IsWarnEnabled)
        {
            _log.Warn(message);
        }
    }
    /// <summary>
    /// Log on error level if error is enabled
    /// </summary>
    /// <param name="message"></param>
    public void Error(object message)
    {
        if (_log.IsErrorEnabled)
        {
            _log.Error(message);
        }
    }
    /// <summary>
    /// Log on fatal level if fatal is enabled
    /// </summary>
    /// <param name="message"></param>
    public void Fatal(object message)
    {
        if (_log.IsFatalEnabled)
        {
            _log.Fatal(message);
        }
    }

    #endregion

}
