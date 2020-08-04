namespace Com.Gosol.CMS.Security
{
    using System;
    using System.Collections;
    using System.Data;

    [Serializable]
    public class SecurityProxy
    {
        private string DELETE_ACCESSRIGHT_ACL = "Delete ObjectACL Where ObjectTypeID = @ObjectTypeID And ObjectID = @ObjectID And EntityID = @EntityID";
        private string DELETE_ACCESSRIGHT_ACLTYPE = "Delete ObjectTypeACL Where ObjectTypeID = @ObjectTypeID And EntityID = @EntityID";
        private string DELETE_USER_GROUP = "Delete UserGroup Where UserID = @UserID And GroupID = @GroupID";
        private string INSERT_ACCESSRIGHT_ACL = "Insert Into ObjectACL (ObjectTypeID, ObjectID, EntityID, EntityType, AccessRight) Values(@ObjectTypeID, @ObjectID, @EntityID, 1, @AccessRight)";
        private string INSERT_ACCESSRIGHT_ACLTYPE = "Insert Into ObjectTypeACL (ObjectTypeID, EntityID, EntityType, AccessRight) Values(@ObjectTypeID, @EntityID, 1, @AccessRight)";
        private string INSERT_USER_GROUP = "Insert Into UserGroup (UserID, GroupID) Values(@UserID, @GroupID)";
        private string PARM_ACCESSRIGHT = "@AccessRight";
        private string PARM_CHANNEL_LOCKED = "@ChannelLocked";
        private string PARM_ENTITY_ID = "@EntityID";
        private string PARM_GROUP_ID = "@GroupID";
        private string PARM_GROUP_LOCKED = "@GroupLocked";
        private string PARM_OBJECT_ID = "@ObjectID";
        private string PARM_OBJECTTYPE_ID = "@ObjectTypeID";
        private string PARM_USER_ID = "@UserID";
        private string PARM_USER_LOCKED = "@UserLocked";
        private string SELECT_ACCESSRIGHT_ACL = "Select AccessRight From ObjectACL Where ObjectTypeID = @ObjectTypeID And ObjectID = @ObjectID And EntityID = @EntityID";
        private string SELECT_ACCESSRIGHT_ACLTYPE = "Select AccessRight From ObjectTypeACL Where ObjectTypeID = @ObjectTypeID And EntityID = @EntityID";
        private string SELECT_ACLCHANNEL_GROUPS = "Select [Group].GroupID, [Group].Name From [Group] Inner Join [ObjectACL] On [Group].GroupID = EntityID Where [Group].Status <> @GroupLocked And ObjectTypeID = @ObjectTypeID And ObjectID = @ObjectID";
        private string SELECT_ACLTYPE_GROUPS = "Select [Group].GroupID, [Group].Name From [Group] Inner Join [ObjectTypeACL] On [Group].GroupID = EntityID Where [Group].Status <> @GroupLocked And ObjectTypeID = @ObjectTypeID";
        private string SELECT_ALL_CHANNELS = "Select ChannelID, [Channel].Name From Channel Where Status <> @ChannelLocked Order By [Channel].Name, [Channel].Priority";
        private string SELECT_ALL_GROUPS = "Select [Group].GroupID, [Group].Name From [Group] Where ([Group].Status <> @GroupLocked)";
        private string SELECT_ALL_OBJECTS = "Select [ObjectType].ObjectTypeID, [ObjectType].Name From [ObjectType]";
        private string SELECT_ALL_USERS = "Select [User].UserID, [User].UserName From [User] Where ([User].Locked <> @UserLocked)";
        private string SELECT_GROUPS_BY_USER_ID = "Select [Group].GroupID, [Group].Name From [User] Inner Join UserGroup On [User].UserId = UserGroup.UserID Inner Join [Group] On [Group].GroupID = UserGroup.GroupID Where ([Group].Status <> @GroupLocked) And ([User].Locked <> @UserLocked) Group By [Group].GroupID, [Group].Name, [User].UserID Having [User].UserID=@UserID";
        private string SELECT_USERS_BY_GROUP_ID = "Select [User].UserID, [User].UserName From [User] Inner Join UserGroup On [User].UserId = UserGroup.UserID Inner Join [Group] On [Group].GroupID = UserGroup.GroupID Where ([Group].Status <> @GroupLocked) And ([User].Locked <> @UserLocked) Group By [User].UserID, [User].UserName, [Group].GroupID Having [Group].GroupID=@GroupID";
        private string UPDATE_ACCESSRIGHT_ACL = "Update ObjectACL Set AccessRight = @AccessRight Where ObjectTypeID = @ObjectTypeID And ObjectID = @ObjectID And EntityID = @EntityID";
        private string UPDATE_ACCESSRIGHT_ACLTYPE = "Update ObjectTypeACL Set AccessRight = @AccessRight Where ObjectTypeID = @ObjectTypeID And EntityID = @EntityID";

        public bool DeleteACL(int objectTypeID, int objectID, int entityID)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.DELETE_ACCESSRIGHT_ACL);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ENTITY_ID, entityID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECTTYPE_ID, objectTypeID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECT_ID, objectID);
            return this.ExecuteNonQuery(dbConnection, dbCommand);
        }

        public bool DeleteACLType(int objectTypeID, int entityID)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.DELETE_ACCESSRIGHT_ACLTYPE);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ENTITY_ID, entityID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECTTYPE_ID, objectTypeID);
            return this.ExecuteNonQuery(dbConnection, dbCommand);
        }

        public bool DeleteUserGroup(int userID, int groupID)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.DELETE_USER_GROUP);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_USER_ID, userID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_GROUP_ID, groupID);
            return this.ExecuteNonQuery(dbConnection, dbCommand);
        }

        private bool ExecuteNonQuery(IDbConnection connection, IDbCommand command)
        {
            object obj2;
            try
            {
                connection.Open();
                obj2 = command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new DatabaseProxyException("Error at DeleteRecord method in ExecuteNonQuery class. Detail: " + exception.Message);
            }
            finally
            {
                connection.Close();
            }
            if (obj2 == null)
            {
                return false;
            }
            return true;
        }

        public AccessLevel GetAccessRight(int objectTypeID, int entityID)
        {
            object noAccess;
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.SELECT_ACCESSRIGHT_ACLTYPE);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECTTYPE_ID, objectTypeID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ENTITY_ID, entityID);
            try
            {
                dbConnection.Open();
                noAccess = dbCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                throw new DatabaseProxyException("Error at GetAccessRight method in SecurityProxy class. Detail:" + exception.Message);
            }
            finally
            {
                dbConnection.Close();
            }
            if (noAccess == null)
            {
                noAccess = AccessLevel.NoAccess;
            }
            return (AccessLevel) noAccess;
        }

        public AccessLevel GetAccessRight(int objectTypeID, int objectID, int entityID)
        {
            object noAccess;
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.SELECT_ACCESSRIGHT_ACL);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECTTYPE_ID, objectTypeID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECT_ID, objectID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ENTITY_ID, entityID);
            try
            {
                dbConnection.Open();
                noAccess = dbCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                throw new DatabaseProxyException("Error at GetAccessRight method in SecurityProxy class. Detail:" + exception.Message);
            }
            finally
            {
                dbConnection.Close();
            }
            if (noAccess == null)
            {
                noAccess = AccessLevel.NoAccess;
            }
            return (AccessLevel) noAccess;
        }

        public IList GetACLChannelGroups(int objectTypeID, int objectID)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.SELECT_ACLCHANNEL_GROUPS);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_GROUP_LOCKED, Convert.ToInt32(GroupStatus.Locked));
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECTTYPE_ID, objectTypeID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECT_ID, objectID);
            return this.GetObjects(dbConnection, dbCommand, IdNameType.ACLChannelGroup);
        }

        public IList GetACLTypeGroups(int objectTypeID)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.SELECT_ACLTYPE_GROUPS);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_GROUP_LOCKED, Convert.ToInt32(GroupStatus.Locked));
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECTTYPE_ID, objectTypeID);
            return this.GetObjects(dbConnection, dbCommand, IdNameType.ACLTypeGroup);
        }

        public IList GetAllChannels()
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.SELECT_ALL_CHANNELS);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_CHANNEL_LOCKED, 0);
            return this.GetObjects(dbConnection, dbCommand, IdNameType.Channel);
        }

        public IList GetAllGroups()
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.SELECT_ALL_GROUPS);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_GROUP_LOCKED, Convert.ToInt32(GroupStatus.Locked));
            return this.GetObjects(dbConnection, dbCommand, IdNameType.Group);
        }

        public IList GetAllObjects()
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand command = DatabaseProxy.CreateDBCommand(dbConnection, this.SELECT_ALL_OBJECTS);
            return this.GetObjects(dbConnection, command, IdNameType.ObjectType);
        }

        public IList GetAllUsers()
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.SELECT_ALL_USERS);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_USER_LOCKED, Convert.ToInt32(UserStatus.Locked));
            return this.GetObjects(dbConnection, dbCommand, IdNameType.User);
        }

        public IList GetGroups(int userID)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.SELECT_GROUPS_BY_USER_ID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_USER_LOCKED, Convert.ToInt32(UserStatus.Locked));
            DatabaseProxy.AddParameter(dbCommand, this.PARM_GROUP_LOCKED, Convert.ToInt32(GroupStatus.Locked));
            DatabaseProxy.AddParameter(dbCommand, this.PARM_USER_ID, userID);
            return this.GetObjects(dbConnection, dbCommand, IdNameType.Group);
        }

        private IList GetObjects(IDbConnection connection, IDbCommand command, IdNameType type)
        {
            IList list = new ArrayList();
            try
            {
                connection.Open();
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    list.Add(new IdNameInfo(id, name, type));
                }
            }
            catch (Exception exception)
            {
                throw new DatabaseProxyException("Error at GetObjects method in SecurityProxy class. Detail:" + exception.Message);
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        public IList GetUsers(int groupID)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.SELECT_USERS_BY_GROUP_ID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_USER_LOCKED, Convert.ToInt32(UserStatus.Locked));
            DatabaseProxy.AddParameter(dbCommand, this.PARM_GROUP_LOCKED, Convert.ToInt32(GroupStatus.Locked));
            DatabaseProxy.AddParameter(dbCommand, this.PARM_GROUP_ID, groupID);
            return this.GetObjects(dbConnection, dbCommand, IdNameType.User);
        }

        public bool InsertACL(int objectTypeID, int objectID, int entityID, int access)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.INSERT_ACCESSRIGHT_ACL);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ACCESSRIGHT, access);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ENTITY_ID, entityID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECTTYPE_ID, objectTypeID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECT_ID, objectID);
            return this.ExecuteNonQuery(dbConnection, dbCommand);
        }

        public bool InsertACLType(int objectTypeID, int entityID, int access)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.INSERT_ACCESSRIGHT_ACLTYPE);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ACCESSRIGHT, access);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ENTITY_ID, entityID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECTTYPE_ID, objectTypeID);
            return this.ExecuteNonQuery(dbConnection, dbCommand);
        }

        public bool InsertUserGroup(int userID, int groupID)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.INSERT_USER_GROUP);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_USER_ID, userID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_GROUP_ID, groupID);
            return this.ExecuteNonQuery(dbConnection, dbCommand);
        }

        public bool UpdateACL(int objectTypeID, int objectID, int entityID, int access)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.UPDATE_ACCESSRIGHT_ACL);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ACCESSRIGHT, access);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ENTITY_ID, entityID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECTTYPE_ID, objectTypeID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECT_ID, objectID);
            return this.ExecuteNonQuery(dbConnection, dbCommand);
        }

        public bool UpdateACLType(int objectTypeID, int entityID, int access)
        {
            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, this.UPDATE_ACCESSRIGHT_ACLTYPE);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ACCESSRIGHT, access);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_ENTITY_ID, entityID);
            DatabaseProxy.AddParameter(dbCommand, this.PARM_OBJECTTYPE_ID, objectTypeID);
            return this.ExecuteNonQuery(dbConnection, dbCommand);
        }
    }
}
