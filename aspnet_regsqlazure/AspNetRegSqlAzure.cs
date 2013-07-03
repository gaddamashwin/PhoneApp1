using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Web.Management
{
    public static class AspNetRegSqlAzure
    {
        #region Constants

        /// <summary>Default database for installing feature schemas.</summary>
        public const string DefaultFeatureDatabase = "aspnetdb";

        /// <summary>Default database for installing session state schema.</summary>
        public const string DefaultStateDatabase = "ASPState";

        /// <summary>Exit code indicating success.</summary>
        public const int SuccessCode = 0;

        /// <summary>Exit code indicating failure.</summary>
        public const int FailureCode = 1;

        /// <summary>Prefix for scripts which install a feature.</summary>
        public const string InstallPrefix = "Install";
        
        /// <summary>Prefix for scripts which uninstall a feature.</summary>
        public const string UninstallPrefix = "Uninstall";

        /// <summary>Name of script which affects common elements of feature schemas.</summary>
        public const string FeatureCommonScript = "Common.sql";

        /// <summary>Name of script which affects sql state schema.</summary>
        public const string StateScript = "PersistSqlState.sql";

        /// <summary>
        /// Dictionary which matches feature scripts to the characters which represent them on the command line.
        /// </summary>
        public static Dictionary<char, string> FeatureScripts = new Dictionary<char, string>()
        {
            {'m', "Membership.sql"},
            {'r', "Roles.sql"},
            {'p', "Profile.sql"},
            {'c', "Personalization.sql"},
            {'w', "WebEventSqlProvider.sql"}
        };

        /// <summary>
        /// List of operations that can be requested from the program.
        /// </summary>
        public enum Operation
        {
            /// <summary>No operation specified.</summary>
            NoOperation,

            /// <summary>Install requested service schemas.</summary>
            InstallFeatures,

            /// <summary>Uninstall requested service schemas.</summary>
            RemoveFeatures,

        }

        #endregion

        #region Static fields

        /// <summary>Dictionary which contains parsed command line options.</summary>
        private static Dictionary<string, string> s_options;

        /// <summary>Stores the currently requested operation.</summary>
        private static Operation s_mode = Operation.NoOperation;

        /// <summary>Stores the server name specified on the command line.</summary>
        private static string s_server = null;

        /// <summary>Stores the login name specified on the command line.</summary>
        private static string s_login = null;

        /// <summary>Stores the password specified on the command line.</summary>
        private static string s_password = null;

        /// <summary>Stores the list of requested features specified on the command line.</summary>
        private static string s_featureList = null;

        /// <summary>Stores the database specified on the command line.</summary>
        private static string s_database = null;

        /// <summary>Quiet option (-Q), off by default.</summary>
        private static bool s_quiet = false;

        //v-mihu add//
        /// <summary>Connection string </summary>
        private static string s_connectionString=null;

        #endregion

        #region Primary Methods

        /// <summary>
        /// Execute aspnet_regsqlazure.
        /// </summary>
        public static int Main(string[] args)
        {
            try
            {
                ParseCommandLine(args);
                ValidateOptions();
                switch (s_mode)
                {
                    case Operation.NoOperation:
                        Console.WriteLine("No operation was specified on the command line.");
                        return SuccessCode;

                    case Operation.InstallFeatures:
                        InstallFeatures();
                        break;

                    case Operation.RemoveFeatures:
                        Console.WriteLine("Unistalling services is not supported at this time.");
                        return FailureCode;
                }
            }
            catch (ApplicationException ae)
            {
                Console.WriteLine("Execution failed:");
                Console.WriteLine(ae.Message);
                return FailureCode;
            }
            catch (SqlException se)
            {
                Console.WriteLine("Caught unexpected SqlException, code {0}", se.Number);
                Console.WriteLine(se.Message);
               
                return FailureCode;
            }

            return SuccessCode;
        }

        /// <summary>
        /// Installs requested ASP.NET features specified by the -A option
        /// </summary>
        public static void InstallFeatures()
        {
            if (string.IsNullOrEmpty(s_featureList))
            {
                throw new ApplicationException("No services were specified for installation!");
            }

            if (string.Equals(s_featureList, "all"))
            {
                s_featureList = "mrpcw";
            }

            if (string.IsNullOrEmpty(s_database))
            {
                s_database = DefaultFeatureDatabase;
            }

            List<string> InstallScripts = new List<string>(s_featureList.Length + 1);
            InstallScripts.Add(InstallPrefix + FeatureCommonScript);
            foreach (char c in s_featureList)
            {
                if (!FeatureScripts.ContainsKey(c))
                {
                    throw new ApplicationException(
                        string.Format("{0} does not correspond to an installable ASP.NET service.", c));
                }

                InstallScripts.Add(InstallPrefix + FeatureScripts[c]);
            }

            foreach (string script in InstallScripts)
            {
                if (!File.Exists(LocateScriptFile(script)))
                {
                    throw new ApplicationException(
                        string.Format("Installation script {0} not found in current directory.", script));
                } 
            }

            Console.WriteLine(RegSql.Start);

            // Ensure s_database exists
            EnsureDatabaseExists();

            // Install requested features
            RunScripts(InstallScripts);

            Console.WriteLine(RegSql.Finished);

        }

        //add by v-mihu
        /// <summary>
        /// find the script in the app's base directory
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        private static string LocateScriptFile(string script)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, script);
        }


        /// <summary>
        /// Connects to the server's master database and ensures that that database s_database exists - if s_database
        /// does not already exist, it will be created.
        /// </summary>
        private static void EnsureDatabaseExists()
        {
            using (SqlConnection conn = CreateConnection(s_server, s_login, s_password, null))
            {
                int dbCount = ExecScalar<int>(conn,
                    "SELECT COUNT(*) FROM sys.databases WHERE name = N'{0}'",
                    s_database);

                if (dbCount == 0)
                {
                    ExecCommand(conn, "CREATE DATABASE [{0}]", s_database);
                }
            }
        }

        /// <summary>
        /// Connects to s_database and runs the requested scripts.
        /// </summary>
        private static void RunScripts(IList<string> scripts)
        {
            foreach (string script in scripts)
            {
                if (!File.Exists(LocateScriptFile(script)))
                {
                    throw new ApplicationException(
                        string.Format("Requested script {0} not found in current directory.", script));
                }
            }

            // Connect to s_database and run requested scripts
            using (SqlConnection conn = CreateConnection(s_server, s_login, s_password, s_database))
            {
                if (!s_quiet)
                {
                    conn.InfoMessage += new SqlInfoMessageEventHandler(DisplaySqlMessage);
                }

                foreach (string script in scripts)
                {

                    using (FileStream fstream = File.Open(LocateScriptFile(script), FileMode.Open, FileAccess.Read))
                    {
                        foreach (string batch in ReadSqlBatches(fstream))
                        {
                            if (batch == string.Empty)
                            {
                                continue;
                            }

                            ExecCommand(conn, batch);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parse the command line for option flags and their values.
        /// </summary>
        private static void ParseCommandLine(string[] args)
        {
            s_options = new Dictionary<string, string>(args.Length);
            char[] flags = new char[] { '-', '/' };
            string lastOption = null;

            foreach (string arg in args)
            {
                if (flags.Contains(arg[0]))
                {
                    if (s_options.ContainsKey(arg))
                    {
                        throw new ApplicationException(string.Format(
                            "Error in command line at argument {0}: duplicate option flag.",
                            arg));
                    }

                    lastOption = arg.Substring(1);
                    s_options.Add(lastOption, null);
                }
                else
                {
                    if (lastOption == null)
                    {
                        throw new ApplicationException(string.Format(
                            "Error in command line at argument {0}: aspnet_reqsqlazure does not accept free arguments",
                            arg));
                    }

                    s_options[lastOption] = arg;
                    lastOption = null;
                }
            }
        }

        /// <summary>
        /// Validate that the command line options in s_options are valid and consistent.
        /// </summary>
        private static void ValidateOptions()
        {
            foreach (string option in s_options.Keys)
            {
                switch (option.ToLower())
                {
                    case "s":
                        s_server = s_options[option];
                        break;

                    case "u":
                        s_login = s_options[option];
                        break;

                    case "p":
                        s_password = s_options[option];
                        break;

                    case "a":
                        SetMode(Operation.InstallFeatures);
                        s_featureList = s_options[option].ToLower();
                        break;

                    case "r":
                        SetMode(Operation.RemoveFeatures);
                        s_featureList = s_options[option].ToLower();
                        break;

                    case "ssadd":
                        throw new ApplicationException("Install Session State is not supported by aspnet_regsqlazure");

                    case "ssremove":
                        throw new ApplicationException("Remove Session State is not supported by aspnet_regsqlazure");
                        
                    case "sstype":
                        s_featureList = s_options[option].ToLower();
                        break;

                    case "d":
                        s_database = s_options[option];
                        break;

                    case "q":
                        s_quiet = true;
                        break;

                    case "w":
                        throw new ApplicationException("Wizard mode is not supported for aspnet_regsqlazure");

                    case "e":
                        throw new ApplicationException("NT Logins are not supported in Sql Azure.");

                    case "c":
                        throw new ApplicationException(
                            "aspnet_regsqlazure cannot accept a raw connection string, must use -S -U -P switches.");

                    case "sqlexportonly":
                        throw new ApplicationException("SQL Export is not supported by aspnet_regsqlazure");

                    case "ed":
                    case "dd":
                    case "et":
                    case "dt":
                    case "t":
                    case "lt":
                        throw new ApplicationException(string.Format(
                            "Option {0} is not supported: SQL Cache dependency settings are not supported in Sql Azure.",
                            option));

                    default:
                        throw new ApplicationException(string.Format("Unknown Option {0} is not supported.", option));
                }
            }

            if (string.IsNullOrEmpty(s_connectionString) && (string.IsNullOrEmpty(s_server)
                || string.IsNullOrEmpty(s_login)
                || string.IsNullOrEmpty(s_password)))
            {
                throw new ApplicationException(
                    "You must specify a Server, Login ID and Password.");
            }

            if (s_mode == Operation.NoOperation)
            {
                throw new ApplicationException(
                    "No operation was specified on the command line - wizard mode is not supported.");
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Create and open a new Sql connection.
        /// </summary>
        /// <param name="server">Server to connect.</param>
        /// <param name="user">Login Used ID.</param>
        /// <param name="password">Password for login.</param>
        /// <param name="database">Database to connect.</param>
        private static SqlConnection CreateConnection(string server, string user, string password, string database)
        {
            string connStr = string.Format("Server={0};User ID={1};Password={2}", server, user, password);
            if (!string.IsNullOrEmpty(database))
            {
                connStr += string.Format(";Database={0}", database);
            }

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// Execute a scalar query against the given SqlConnection.
        /// </summary>
        private static T ExecScalar<T>(SqlConnection conn, string queryFormat, params object[] args)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = String.Format(queryFormat, args);
                cmd.CommandType = System.Data.CommandType.Text;

                object o = cmd.ExecuteScalar();

                if (o != null)
                {
                    return (T)o;
                }
                else
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// Execute a nonquery command against the given SqlConnection.
        /// </summary>
        private static void ExecCommand(SqlConnection conn, string commandFormat, params object[] args)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = args.Length > 0 ?  
                    String.Format(commandFormat, args)
                    : commandFormat;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Iterator which returns consecutive sql batches from a test stream.  A Sql Batch is a consecutive series of 
        /// Sql Statements followed by a "GO" statement.
        /// </summary>
        /// <param name="sqlStream">Text stream from which to read batches.</param>
        private static IEnumerable<string> ReadSqlBatches(Stream sqlStream)
        {
            StreamReader reader = new StreamReader(sqlStream);
            StringBuilder statement = new StringBuilder(string.Empty);

            while(true)
            {
                string nextLine = reader.ReadLine();

                // If this is the last line, return the current batch and break.
                if (nextLine == null)
                {
                    yield return statement.ToString();
                    yield break;
                }

                nextLine = nextLine.Trim();

                // If this line is a "GO" statement, return the current batch.
                if (string.Equals(nextLine, "GO", StringComparison.InvariantCultureIgnoreCase))
                {
                    yield return statement.ToString();
                    statement = new StringBuilder(string.Empty);
                    continue;
                }

                if (nextLine == string.Empty)
                {
                    continue;
                }

                // If this line is a single-line comment, just skip the line
                if (nextLine.StartsWith("--"))
                {
                    continue;
                }

                statement.AppendLine(nextLine);
            }
        }

        /// <summary>
        /// Helper function to ensure that only one operation is requested on the command line.
        /// </summary>
        private static void SetMode(Operation mode)
        {
            if (s_mode != Operation.NoOperation)
            {
                throw new ApplicationException(
                    "Cannot specify more than one operation. must specify one of: -A, -R, -ssadd, -ssremove");
            }

            s_mode = mode;
        }

        /// <summary>
        /// Dumps Info Messages from the connection directly to the Console.
        /// </summary>
        private static void DisplaySqlMessage(object o, SqlInfoMessageEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.Message);
        }

        #endregion
    }
}
