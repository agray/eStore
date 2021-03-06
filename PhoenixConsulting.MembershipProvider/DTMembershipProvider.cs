//Adapted from: http://msdn2.microsoft.com/en-us/library/6tc47t75(VS.80).aspx
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using phoenixconsulting.common.handlers;

// This provider works with the following schema for the table of user data.
// 
//CREATE TABLE [dbo].[Users](
//	[UserID] [uniqueidentifier] NOT NULL,
//	[Username] [nvarchar](255) NOT NULL,
//	[ApplicationName] [nvarchar](255) NOT NULL,
//  [FirstName] [nvarchar](255) NOT NULL,
//	[LastName] [nvarchar](255) NOT NULL,
//	[RequireNewsletter] [bit] NOT NULL,
//	[Email] [nvarchar](128) NOT NULL,
//	[Comment] [nvarchar](255) NULL,
//	[Password] [nvarchar](128) NOT NULL,
//	[IsApproved] [bit] NULL,
//	[LastActivityDate] [datetime] NULL,
//	[LastLoginDate] [datetime] NULL,
//	[LastPasswordChangedDate] [datetime] NULL,
//	[CreationDate] [datetime] NULL,
//	[IsOnLine] [bit] NULL,
//	[IsLockedOut] [bit] NULL,
//	[LastLockedOutDate] [datetime] NULL,
//	[FailedPasswordAttemptCount] [int] NULL,
//	[FailedPasswordAttemptWindowStart] [datetime] NULL,
//	[FailedPasswordAnswerAttemptCount] [int] NULL,
//	[FailedPasswordAnswerAttemptWindowStart] [datetime] NULL,
//PRIMARY KEY CLUSTERED 
//(
//	[UserID] ASC
//)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
//) ON [PRIMARY]
// 

namespace com.phoenixconsulting.AspNet.Membership {

    public sealed class DTMembershipProvider : MembershipProvider {

        #region Class Variables

        private int newPasswordLength = 8;
        private string connectionString;
        private string applicationName;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private int maxInvalidPasswordAttempts;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private string passwordStrengthRegularExpression;
        private MachineKeySection machineKey; //Used when determining encryption key values.

        #endregion

        #region Enums

        private enum FailureType {
            Password = 1,
            PasswordAnswer = 2
        }

        #endregion

        #region Properties

        public override string ApplicationName {
            get { return applicationName; }
            set { applicationName = value; }
        }

        public override bool EnablePasswordReset {
            get { return enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval {
            get { return enablePasswordRetrieval; }
        }

        public override bool RequiresQuestionAndAnswer {
            get { return requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail {
            get { return requiresUniqueEmail; }
        }

        public override int MaxInvalidPasswordAttempts {
            get { return maxInvalidPasswordAttempts; }
        }

        public override int PasswordAttemptWindow {
            get { return passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat {
            get { return passwordFormat; }
        }

        public override int MinRequiredNonAlphanumericCharacters {
            get { return minRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength {
            get { return minRequiredPasswordLength; }
        }

        public override string PasswordStrengthRegularExpression {
            get { return passwordStrengthRegularExpression; }
        }

        //public override string FullName {
        //    get {

        //    }
        //}

        #endregion

        #region Initialization

        public override void Initialize(string name, NameValueCollection config) {
            if(config == null) {
                throw new ArgumentNullException("config");
            }

            if(String.IsNullOrEmpty(name)) {
                name = "DTMembershipProvider";
            }

            if(String.IsNullOrEmpty(config["description"])) {
                config.Remove("description");
                config.Add("description", "How Do I: Sample Membership provider");
            }

            //Initialize the abstract base class.
            base.Initialize(name, config);

            applicationName = GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);
            maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredAlphaNumericCharacters"], "1"));
            minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], String.Empty));
            enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));

            var temp_format = config["passwordFormat"] ?? "Hashed";
            passwordFormat = getMembershipPasswordFormat(temp_format);

            var ConnectionStringSettings = ConfigurationManager.ConnectionStrings["eStoreConnectionString"];

            if((ConnectionStringSettings == null) || (ConnectionStringSettings.ConnectionString.Trim() == String.Empty)) {
                throw new ProviderException("Connection string cannot be blank.");
            }

            connectionString = ConnectionStringSettings.ConnectionString;
            //connectionString = getConnectionString("DTMembershipProviderSqlServer");

            //Get encryption and decryption key information from the configuration.
            var cfg = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
            machineKey = cfg.GetSection("system.web/machineKey") as MachineKeySection;

            if(machineKey.ValidationKey.Contains("AutoGenerate")) {
                if(PasswordFormat != MembershipPasswordFormat.Clear) {
                    throw new ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.");
                }
            }
        }

        private MembershipPasswordFormat getMembershipPasswordFormat(string tempFormat) {
            switch(tempFormat) {
                case "Hashed":
                    return MembershipPasswordFormat.Hashed;
                case "Encrypted":
                    return MembershipPasswordFormat.Encrypted;
                case "Clear":
                    return MembershipPasswordFormat.Clear;
                default:
                    throw new ProviderException("Password format not supported.");
            }
        }

        private string getConnectionString(string name) {
            var rootWebConfig = WebConfigurationManager.OpenWebConfiguration(null);
            WebConfigurationManager.OpenWebConfiguration(null);
            return rootWebConfig.ConnectionStrings.ConnectionStrings[name].ConnectionString;
        }

        private string GetConfigValue(string configValue, string defaultValue) {
            return String.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }

        #endregion

        #region Implemented Abstract Methods from MembershipProvider

        /// <summary>
        /// Change the user password.
        /// </summary>
        /// <param name="username">UserName</param>
        /// <param name="oldPwd">Old password.</param>
        /// <param name="newPwd">New password.</param>
        /// <returns>T/F if password was changed.</returns>
        public override bool ChangePassword(string username, string oldPwd, string newPwd) {
            if(!ValidateUser(username, oldPwd)) {
                return false;
            }

            var args = new ValidatePasswordEventArgs(username, newPwd, true);

            OnValidatingPassword(args);

            if(args.Cancel) {
                if(args.FailureInformation == null) {
                    throw new Exception("Change password canceled due to new password validation failure.");
                }
                throw args.FailureInformation;
            }

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_ChangePassword", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = EncodePassword(newPwd);
            sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            try {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            } catch(SqlException) {
                //Add exception handling here.
                return false;
            } finally {
                sqlConnection.Close();
            }

            return true;

        }

        /// <summary>
        /// Change the user name.
        /// </summary>
        /// <param name="username">Old UserName</param>
        /// <param name="newPwd">New UserName.</param>
        /// <returns>T/F if password was changed.</returns>
        public bool ChangeUsername(string oldUsername, string newUsername) {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_ChangeUserName", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@oldusername", SqlDbType.NVarChar, 255).Value = oldUsername;
            sqlCommand.Parameters.Add("@newusername", SqlDbType.NVarChar, 255).Value = newUsername;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            try {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            } catch(SqlException) {
                //Add exception handling here.
                return false;
            } finally {
                sqlConnection.Close();
            }

            return true;
        }

        /// <summary>
        /// Change the question and answer for a password validation.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="newPwdQuestion">New question text.</param>
        /// <param name="newPwdAnswer">New answer text.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPwdQuestion, string newPwdAnswer) {

            if(!ValidateUser(username, password)) {
                return false;
            }

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_ChangePasswordQuestionAnswer", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@returnValue", SqlDbType.Int, 0).Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add("@question", SqlDbType.NVarChar, 255).Value = newPwdQuestion;
            sqlCommand.Parameters.Add("@answer", SqlDbType.NVarChar, 255).Value = EncodePassword(newPwdAnswer);
            sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            try {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                if((int)sqlCommand.Parameters["@returnValue"].Value != 0) {
                    return false;
                }
            } catch(SqlException) {
                //Add exception handling here.
                return false;
            } finally {
                sqlConnection.Close();
            }

            return true;

        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="email">Email address.</param>
        /// <param name="passwordQuestion">Security quesiton for password.</param>
        /// <param name="passwordAnswer">Security quesiton answer for password.</param>
        /// <param name="isApproved"></param>
        /// <param name="userID">User ID</param>
        /// <param name="status"></param>
        /// <returns>MembershipUser</returns>

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
            var args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if(args.Cancel) {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if((RequiresUniqueEmail && (GetUserNameByEmail(email) != String.Empty))) {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            var membershipUser = GetUser(username, false);

            if(membershipUser == null) {
                //DateTime createDate = DateTime.Now;

                var sqlConnection = new SqlConnection(connectionString);
                var sqlCommand = new SqlCommand("User_Ins", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@returnValue", SqlDbType.Int, 0).Direction = ParameterDirection.ReturnValue;
                sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = email;
                sqlCommand.Parameters.Add("@firstname", SqlDbType.NVarChar, 255).Value = SessionHandler.Instance.LoginFirstName;
                sqlCommand.Parameters.Add("@lastname", SqlDbType.NVarChar, 255).Value = SessionHandler.Instance.LoginLastName;
                sqlCommand.Parameters.Add("@requirenewsletter", SqlDbType.Bit).Value = SessionHandler.Instance.LoginRequireNewsletter;
                sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
                sqlCommand.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = EncodePassword(password);
                sqlCommand.Parameters.Add("@email", SqlDbType.NVarChar, 128).Value = email;
                //sqlCommand.Parameters.Add("@passwordQuestion", SqlDbType.NVarChar, 255).Value = passwordQuestion;
                //sqlCommand.Parameters.Add("@passwordAnswer", SqlDbType.NVarChar, 255).Value = EncodePassword(passwordAnswer);
                sqlCommand.Parameters.Add("@isApproved", SqlDbType.Bit).Value = isApproved;
                sqlCommand.Parameters.Add("@comment", SqlDbType.NVarChar, 255).Value = String.Empty;

                try {
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();
                    status = (int)sqlCommand.Parameters["@returnValue"].Value == 0 
                             ? MembershipCreateStatus.Success 
                             : MembershipCreateStatus.UserRejected;
                } catch(SqlException) {
                    //Add exception handling here.
                    status = MembershipCreateStatus.ProviderError;
                } finally {
                    sqlConnection.Close();
                }

                return GetUser(username, false);
            } else {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="deleteAllRelatedData">Whether to delete all related data.</param>
        /// <returns>T/F if the user was deleted.</returns>
        public override bool DeleteUser(string username, bool deleteAllRelatedData) {

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_Del", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@returnValue", SqlDbType.Int, 0).Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            try {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                if((int)sqlCommand.Parameters["@returnValue"].Value == 0) {
                    if(deleteAllRelatedData) {
                        //Process commands to delete all data for the user in the database.
                    }
                } else {
                    return false;
                }
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                sqlConnection.Close();
            }

            return true;
        }

        /// <summary>
        /// Get a collection of users.
        /// </summary>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="totalRecords">Total # of records to retrieve.</param>
        /// <returns>Collection of MembershipUser objects.</returns>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("Users_Sel", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            var users = new MembershipUserCollection();

            SqlDataReader sqlDataReader = null;
            totalRecords = 0;

            try {
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                var counter = 0;
                var startIndex = pageSize * pageIndex;
                var endIndex = startIndex + pageSize - 1;

                while(sqlDataReader.Read()) {
                    if(counter >= startIndex) {
                        users.Add(GetUserFromReader(sqlDataReader));
                    }

                    if(counter >= endIndex) {
                        sqlCommand.Cancel();
                    }
                    counter += 1;
                }
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                if(sqlDataReader != null) {
                    sqlDataReader.Close();
                }
            }

            return users;

        }

        /// <summary>
        /// Gets the number of users currently on-line.
        /// </summary>
        /// <returns>  /// # of users on-line.</returns>
        public override int GetNumberOfUsersOnline() {

            var onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
            var compareTime = DateTime.Now.Subtract(onlineSpan);

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("Users_NumberOnline", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
            sqlCommand.Parameters.Add("@compareDate", SqlDbType.DateTime).Value = compareTime;

            var numOnline = 0;

            try {
                sqlConnection.Open();
                numOnline = Convert.ToInt32(sqlCommand.ExecuteScalar());
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                sqlConnection.Close();
            }

            return numOnline;

        }

        /// <summary>
        /// Get the password for a user.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="answer">Answer to security question.</param>
        /// <returns>Password for the user.</returns>
        public override string GetPassword(string username, string answer) {

            if(!EnablePasswordRetrieval) {
                throw new ProviderException("Password Retrieval Not Enabled.");
            }

            if(PasswordFormat == MembershipPasswordFormat.Hashed) {
                throw new ProviderException("Cannot retrieve Hashed passwords.");
            }

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_GetPassword", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            var password = String.Empty;
            var passwordAnswer = String.Empty;
            SqlDataReader sqlDataReader = null;

            try {
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow & CommandBehavior.CloseConnection);
                if(sqlDataReader.HasRows) {
                    sqlDataReader.Read();

                    if(sqlDataReader.GetBoolean(2)) {
                        throw new MembershipPasswordException("The supplied user is locked out.");
                    }

                    password = sqlDataReader.GetString(0);
                    passwordAnswer = sqlDataReader.GetString(1);
                } else {
                    throw new MembershipPasswordException("The supplied user name is not found.");
                }
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                if(sqlDataReader != null) {
                    sqlDataReader.Close();
                }
            }

            if(RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer)) {
                UpdateFailureCount(username, FailureType.PasswordAnswer);
                throw new MembershipPasswordException("Incorrect password answer.");
            }

            if(PasswordFormat == MembershipPasswordFormat.Encrypted) {
                password = UnEncodePassword(password);
            }
            return password;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline) {

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_Sel", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            MembershipUser membershipUser = null;
            SqlDataReader sqlDataReader = null;

            try {
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if(sqlDataReader.HasRows) {
                    sqlDataReader.Read();
                    membershipUser = GetUserFromReader(sqlDataReader);

                    if(userIsOnline) {
                        var sqlUpdateCommand = new SqlCommand("User_UpdateActivityDate_ByUserName", sqlConnection);

                        sqlUpdateCommand.CommandType = CommandType.StoredProcedure;
                        sqlUpdateCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
                        sqlUpdateCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
                        sqlUpdateCommand.ExecuteNonQuery();
                    }
                }
            } catch(SqlException e) {
                Console.Out.WriteLine(e.Message);
                //Add exception handling here.
            } finally {
                if(sqlDataReader != null) {
                    sqlDataReader.Close();
                }
            }

            return membershipUser;
        }

        /// <summary>
        /// Get a user based upon provider key and if they are on-line.
        /// </summary>
        /// <param name="userID">Provider key.</param>
        /// <param name="userIsOnline">T/F whether the user is on-line.</param>
        /// <returns></returns>
        public override MembershipUser GetUser(object userID, bool userIsOnline) {

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_SelByUserID", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@userID", SqlDbType.UniqueIdentifier).Value = userID;

            MembershipUser membershipUser = null;
            SqlDataReader sqlDataReader = null;

            try {
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if(sqlDataReader.HasRows) {
                    sqlDataReader.Read();
                    membershipUser = GetUserFromReader(sqlDataReader);

                    if(userIsOnline) {
                        var sqlUpdateCommand = new SqlCommand("User_UpdateActivityDate_ByUserID", sqlConnection);

                        sqlUpdateCommand.CommandType = CommandType.StoredProcedure;
                        sqlUpdateCommand.Parameters.Add("@userID", SqlDbType.NVarChar, 255).Value = userID;
                        sqlUpdateCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
                        sqlUpdateCommand.ExecuteNonQuery();
                    }
                }
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                if(sqlDataReader != null) {
                    sqlDataReader.Close();
                }
            }

            return membershipUser;

        }

        /// <summary>
        /// Unlock a user.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <returns>T/F if unlocked.</returns>
        public override bool UnlockUser(string username) {

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_Unlock", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@returnValue", SqlDbType.Int, 0).Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            //int rowsAffected = 0;

            try {
                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
                if((int)sqlCommand.Parameters["@returnValue"].Value == 0) {
                    return false;
                }
            } catch(SqlException) {
                //Add exception handling here.
                return false;
            } finally {
                sqlConnection.Close();
            }

            return true;

        }

        /// <summary>
        /// Lock a user.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <returns>T/F if locked.</returns>
        public bool LockUser(string username) {

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_Lock", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@returnValue", SqlDbType.Int, 0).Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            try {
                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
                if((int)sqlCommand.Parameters["@returnValue"].Value == 0) {
                    return false;
                }
            } catch(SqlException) {
                //Add exception handling here.
                return false;
            } finally {
                sqlConnection.Close();
            }

            return true;
        }

        /// <summary>
        /// Get user name by email address.
        /// </summary>
        /// <param name="username">Email Address.</param>
        /// <returns>username.</returns>
        public override string GetUserNameByEmail(string email) {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("UserName_Sel_ByEmail", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@email", SqlDbType.NVarChar, 128).Value = email;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            var username = String.Empty;

            try {
                sqlConnection.Open();
                username = Convert.ToString(sqlCommand.ExecuteScalar());
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                sqlConnection.Close();
            }

            if(username == null) {
                return String.Empty;
            } else {
                username.Trim();
            }

            return username;
        }

        /// <summary>
        /// Reset the user password.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="answer">Answer to security question.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public override string ResetPassword(string username, string answer) {

            if(!EnablePasswordReset) {
                throw new NotSupportedException("Password Reset is not enabled.");
            }

            if((answer == null) && (RequiresQuestionAndAnswer)) {
                UpdateFailureCount(username, FailureType.PasswordAnswer);
                throw new ProviderException("Password answer required for password Reset.");
            }

            var newPassword = System.Web.Security.Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters);

            var args = new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);

            if(args.Cancel) {
                if(args.FailureInformation != null) {
                    throw args.FailureInformation;
                } else {
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");
                }
            }


            var rowsAffected = 0;
            var passwordAnswer = String.Empty;
            SqlDataReader sqlDataReader = null;

            try {
                var sqlConnection = new SqlConnection(connectionString);
                var sqlCommand = new SqlCommand("User_IsLockedOut", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
                sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow & CommandBehavior.CloseConnection);

                if(sqlDataReader.HasRows) {
                    sqlDataReader.Read();

                    //if(sqlDataReader.GetBoolean(1)) {
                    if(sqlDataReader.GetBoolean(0)) {
                        throw new MembershipPasswordException("The supplied user is locked out.");
                    }
                    //passwordAnswer = sqlDataReader.GetString(0);
                } else {
                    throw new MembershipPasswordException("The supplied user name is not found.");
                }

            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                if(sqlDataReader != null) {
                    sqlDataReader.Close();
                }
            }

            if(RequiresQuestionAndAnswer && (!CheckPassword(answer, passwordAnswer))) {
                UpdateFailureCount(username, FailureType.PasswordAnswer);
                throw new MembershipPasswordException("Incorrect password answer.");
            }

            try {
                var sqlConnection2 = new SqlConnection(connectionString);
                var sqlUpdateCommand = new SqlCommand("User_UpdatePassword", sqlConnection2);

                sqlUpdateCommand.CommandType = CommandType.StoredProcedure;
                sqlUpdateCommand.Parameters.Add("@password", SqlDbType.NVarChar, 128).Value = EncodePassword(newPassword);
                sqlUpdateCommand.Parameters.Add("@userName", SqlDbType.NVarChar, 255).Value = username;
                sqlUpdateCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
                sqlConnection2.Open();
                rowsAffected = sqlUpdateCommand.ExecuteNonQuery();
                var i = 1;
                i++;

            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                if(sqlDataReader != null) {
                    sqlDataReader.Close();
                }
            }

            if(rowsAffected > 0) {
                return newPassword;
            } else {
                throw new MembershipPasswordException("User not found, or user is locked out. Password not Reset.");
            }
        }

        ///// <summary>
        ///// Reset the user password.
        ///// </summary>
        ///// <param name="username">User name.</param>
        ///// <param name="answer">Answer to security question.</param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //public string ResetPassword(string email) {

        //    if(!EnablePasswordReset) {
        //        throw new NotSupportedException("Password Reset is not enabled.");
        //    }

        //    string newPassword = System.Web.Security.Membership.GeneratePassword(newPasswordLength,
        //                                                                         MinRequiredNonAlphanumericCharacters);

        //    ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(email,
        //                                                                   newPassword,
        //                                                                   true);

        //    OnValidatingPassword(args);

        //    if(args.Cancel) {
        //        if(args.FailureInformation != null) {
        //            throw args.FailureInformation;
        //        } else {
        //            throw new MembershipPasswordException("Reset password canceled due to password validation failure.");
        //        }
        //    }

        //    SqlConnection sqlConnection = new SqlConnection(connectionString);
        //    SqlCommand sqlCommand = new SqlCommand("User_GetPasswordAnswer", sqlConnection);

        //    sqlCommand.CommandType = CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = email;
        //    sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

        //    int rowsAffected = 0;
        //    //string passwordAnswer = String.Empty;
        //    SqlDataReader sqlDataReader = null;

        //    try {
        //        sqlConnection.Open();
        //        sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow & CommandBehavior.CloseConnection);

        //        if(sqlDataReader.HasRows) {
        //            sqlDataReader.Read();
        //            if(sqlDataReader.GetBoolean(1)) {
        //                throw new MembershipPasswordException("The supplied user is locked out.");
        //            }
        //        } else {
        //            throw new MembershipPasswordException("The supplied user name is not found.");
        //        }

        //        SqlCommand sqlUpdateCommand = new SqlCommand("User_UpdatePassword", sqlConnection);

        //        sqlUpdateCommand.CommandType = CommandType.StoredProcedure;
        //        sqlUpdateCommand.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = EncodePassword(newPassword);
        //        sqlUpdateCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = email;
        //        sqlUpdateCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
        //        rowsAffected = sqlUpdateCommand.ExecuteNonQuery();
        //    } catch(SqlException) {
        //        //Add exception handling here.
        //    } finally {
        //        if(sqlDataReader != null) { sqlDataReader.Close(); }
        //    }

        //    if(rowsAffected > 0) {
        //        return newPassword;
        //    } else {
        //        throw new MembershipPasswordException("User not found, or user is locked out. Password not Reset.");
        //    }
        //}


        public void UpdateUserContactPreferences(string userName, bool requiresNewsletter) {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_ContactPreferences_Upd", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
            sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = userName;
            sqlCommand.Parameters.Add("@requireNewsletter", SqlDbType.Bit).Value = requiresNewsletter ? 1 : 0;

            try {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                sqlConnection.Close();
            }

        }


        /// <summary>
        /// Update the user information.
        /// </summary>
        /// <param name="_membershipUser">MembershipUser object containing data.</param>
        public override void UpdateUser(MembershipUser membershipUser) {

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_Upd", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@email", SqlDbType.NVarChar, 128).Value = membershipUser.Email;
            sqlCommand.Parameters.Add("@comment", SqlDbType.NVarChar, 255).Value = membershipUser.Comment;
            sqlCommand.Parameters.Add("@isApproved", SqlDbType.Bit).Value = membershipUser.IsApproved;
            sqlCommand.Parameters.Add("@requireNewsletter", SqlDbType.Bit).Value = SessionHandler.Instance.LoginRequireNewsletter;
            sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = membershipUser.UserName;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            try {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Validate the user based upon username and password.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>T/F if the user is valid.</returns>
        public override bool ValidateUser(string username, string password) {

            var isValid = false;

            if(String.IsNullOrEmpty(SessionHandler.Instance.LoginEmailAddress)) {
                SessionHandler.Instance.LoginEmailAddress = username;
            }

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("User_Validate", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@emailAddress", SqlDbType.NVarChar, 255).Value = SessionHandler.Instance.LoginEmailAddress;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            SqlDataReader sqlDataReader = null;
            var isApproved = false;
            var storedPassword = String.Empty;

            try {
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);

                if(sqlDataReader.HasRows) {
                    sqlDataReader.Read();
                    storedPassword = sqlDataReader.GetString(0);
                    isApproved = sqlDataReader.GetBoolean(1);
                } else {
                    return false;
                }

                sqlDataReader.Close();

                if(CheckPassword(password, storedPassword)) {
                    if(isApproved) {
                        isValid = true;

                        var sqlUpdateCommand = new SqlCommand("User_UpdateLoginDate", sqlConnection);

                        sqlUpdateCommand.CommandType = CommandType.StoredProcedure;
                        sqlUpdateCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
                        sqlUpdateCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
                        sqlUpdateCommand.ExecuteNonQuery();
                    }
                } else {
                    sqlConnection.Close();
                    UpdateFailureCount(username, FailureType.Password);
                }
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                if(sqlDataReader != null) {
                    sqlDataReader.Close();
                }
                if((sqlConnection != null) && (sqlConnection.State == ConnectionState.Open)) {
                    sqlConnection.Close();
                }
            }

            return isValid;
        }

        /// <summary>
        /// Find all users matching a search string.
        /// </summary>
        /// <param name="usernameToMatch">Search string of user name to match.</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords">Total records found.</param>
        /// <returns>Collection of MembershipUser objects.</returns>

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("Users_Sel_ByUserName", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = usernameToMatch;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            var membershipUsers = new MembershipUserCollection();
            SqlDataReader sqlDataReader = null;
            var counter = 0;

            try {
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                var startIndex = pageSize * pageIndex;
                var endIndex = startIndex + pageSize - 1;

                while(sqlDataReader.Read()) {
                    if(counter >= startIndex) {
                        var membershipUser = GetUserFromReader(sqlDataReader);
                        membershipUsers.Add(membershipUser);
                    }

                    if(counter >= endIndex) {
                        sqlCommand.Cancel();
                    }

                    counter += 1;
                }
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                if(sqlDataReader != null) {
                    sqlDataReader.Close();
                }
            }

            totalRecords = counter;

            return membershipUsers;
        }

        /// <summary>
        /// Find all users matching a search string of their email.
        /// </summary>
        /// <param name="emailToMatch">Search string of email to match.</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords">Total records found.</param>
        /// <returns>Collection of MembershipUser objects.</returns>

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("Users_Sel_ByUserName", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@email", SqlDbType.NVarChar, 255).Value = emailToMatch;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            var membershipUsers = new MembershipUserCollection();
            SqlDataReader sqlDataReader = null;
            var counter = 0;

            try {
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                var startIndex = pageSize * pageIndex;
                var endIndex = startIndex + pageSize - 1;

                while(sqlDataReader.Read()) {
                    if(counter >= startIndex) {
                        var membershipUser = GetUserFromReader(sqlDataReader);
                        membershipUsers.Add(membershipUser);
                    }

                    if(counter >= endIndex) {
                        sqlCommand.Cancel();
                    }

                    counter += 1;
                }
            } catch(SqlException) {
                //Add exception handling here.
            } finally {
                if(sqlDataReader != null) {
                    sqlDataReader.Close();
                }
            }

            totalRecords = counter;

            return membershipUsers;
        }

        public Guid getLoggedInUserID(string username) {
            return new Guid(GetUser(username, true).ProviderUserKey.ToString());
        }

        #endregion

        #region "Utility Functions"

        /// <summary>
        /// Create a MembershipUser object from a data reader.
        /// </summary>
        /// <param name="sqlDataReader">Data reader.</param>
        /// <returns>MembershipUser object.</returns>
        private MembershipUser GetUserFromReader(SqlDataReader sqlDataReader) {
            var userID = sqlDataReader.GetValue(0);
            var username = sqlDataReader.GetString(1);
            var email = sqlDataReader.GetString(5);

            //string passwordQuestion = String.Empty;
            //if(sqlDataReader.GetValue(3) != DBNull.Value) {
            //    passwordQuestion = sqlDataReader.GetString(3);
            //}

            var comment = String.Empty;
            if(sqlDataReader.GetValue(6) != DBNull.Value) {
                comment = sqlDataReader.GetString(6);
            }

            var isApproved = sqlDataReader.GetBoolean(7);
            var isLockedOut = sqlDataReader.GetBoolean(8);
            var creationDate = sqlDataReader.GetDateTime(9);

            var lastLoginDate = new DateTime();
            if(sqlDataReader.GetValue(10) != DBNull.Value) {
                lastLoginDate = sqlDataReader.GetDateTime(10);
            }

            var lastActivityDate = sqlDataReader.GetDateTime(11);
            var lastPasswordChangedDate = sqlDataReader.GetDateTime(12);

            var lastLockedOutDate = new DateTime();
            if(sqlDataReader.GetValue(13) != DBNull.Value) {
                lastLockedOutDate = sqlDataReader.GetDateTime(13);
            }

            var membershipUser = new MembershipUser(Name, username, userID, email, "", comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockedOutDate);

            return membershipUser;

        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private byte[] HexToByte(string hexString) {
            var returnBytes = new byte[hexString.Length / 2];
            for(var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// Update password and answer failure information.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="failureType">Type of failure</param>
        /// <remarks></remarks>
        private void UpdateFailureCount(string username, FailureType failureType) {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("Users_Sel_ByUserName", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@failureType", SqlDbType.Int, 0).Value = failureType;
            sqlCommand.Parameters.Add("@passwordAttempWindow", SqlDbType.DateTime, 0).Value = passwordAttemptWindow;
            sqlCommand.Parameters.Add("@maxInvalidPasswordAttempts", SqlDbType.Int, 0).Value = maxInvalidPasswordAttempts;
            sqlCommand.Parameters.Add("@userName", SqlDbType.NVarChar, 255).Value = username;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            try {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            } catch(Exception) {
                //Add exception handling here.
            }

        }

        /// <summary>
        /// Check the password format based upon the MembershipPasswordFormat.
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="dbpassword"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool CheckPassword(string password, string dbpassword) {
            var pass1 = password;
            var pass2 = dbpassword;

            switch(PasswordFormat) {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
            }

            return pass1 == pass2;
        }

        /// <summary>
        /// Encode password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <returns>Encoded password.</returns>
        private string EncodePassword(string password) {
            var encodedPassword = password;

            switch(PasswordFormat) {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword = Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    var hash = new HMACSHA1();
                    hash.Key = HexToByte(machineKey.ValidationKey);
                    encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return encodedPassword;
        }

        /// <summary>
        /// UnEncode password.
        /// </summary>
        /// <param name="encodedPassword">Password.</param>
        /// <returns>Unencoded password.</returns>
        private string UnEncodePassword(string encodedPassword) {
            var password = encodedPassword;

            switch(PasswordFormat) {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password = Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }

        #endregion
    }
}