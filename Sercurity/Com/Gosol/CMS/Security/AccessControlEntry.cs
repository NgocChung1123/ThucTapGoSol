namespace Com.Gosol.CMS.Security
{
    using System;

    [Serializable]
    internal class AccessControlEntry
    {
        private int _accessRight;
        private ACLType _aclType;
        private int _entityID;
        private Com.Gosol.CMS.Security.EntityType _entityType;

        public AccessControlEntry(Com.Gosol.CMS.Security.EntityType entityType, int entityID, int accessRight) : this(entityType, entityID, accessRight, ACLType.ObjectInstance)
        {
        }

        public AccessControlEntry(Com.Gosol.CMS.Security.EntityType entityType, int entityID, int accessRight, ACLType aclType)
        {
            this._entityType = entityType;
            if (aclType == ACLType.ObjectClass)
            {
                this._entityID = -1;
            }
            else if (aclType == ACLType.ObjectInstance)
            {
                this._entityID = entityID;
            }
            this._aclType = aclType;
            this._accessRight = accessRight;
        }

        public int AccessRight
        {
            get
            {
                return this._accessRight;
            }
        }

        public ACLType ACEType
        {
            get
            {
                return this._aclType;
            }
        }

        public int EntityID
        {
            get
            {
                return this._entityID;
            }
        }

        public Com.Gosol.CMS.Security.EntityType EntityType
        {
            get
            {
                return this._entityType;
            }
        }
    }
}
