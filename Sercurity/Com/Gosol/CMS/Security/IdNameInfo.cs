namespace Com.Gosol.CMS.Security
{
    using System;

    [Serializable]
    public class IdNameInfo
    {
        private int _id;
        private string _name;
        private IdNameType _type;

        public IdNameInfo(int id, string name, IdNameType type)
        {
            this._id = id;
            this._name = name;
            this._type = type;
        }

        public int ID
        {
            get
            {
                return this._id;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public IdNameType Type
        {
            get
            {
                return this._type;
            }
        }
    }
}
