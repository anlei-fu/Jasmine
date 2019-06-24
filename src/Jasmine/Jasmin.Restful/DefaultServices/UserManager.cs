﻿using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Ioc.Attributes;
using Jasmine.Restful.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Restful.DefaultFilters
{
    public class UserGroup
    {
        public AuntenticateLevel Level { get; set; }
      
        public string Name { get; set; }
        public IDictionary<string, User> Users = new ConcurrentDictionaryIDictonaryAdapter<string, User>();
    }


    public  class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Group { get; set; }
    }

    public enum AuntenticateLevel
    {
        User=0,
        Admin=1,
        SupperAdmin=2,
    }

    public interface IUserManager
    {
        bool CreateUser(string name, string password, string group);
        bool CreateGroup(string name, AuntenticateLevel level);
        bool Validate(string name, string password);
        AuntenticateLevel? GetUserLevel(string name);
        AuntenticateLevel? GetGroupLevel(string name);
        bool UserExists(string name);
        bool UpdatePassword(string name, string password);
        bool ChangeGroup(string user, string newGroup);
        bool RemoveUser(string user);
        bool RemoveGroup(string group);
        UserGroup[] GetAll();
    }



    [BeforeInterceptor(typeof(SessionValidateFilter))]
    [Restful]
    [Path("/api/usermng")]
    public class XmlStoreUserManager:ILoginValidator,IUserManager
    {
        protected  IDictionary<string, UserGroup> _groups = new ConcurrentDictionaryIDictonaryAdapter<string, UserGroup>();
        protected IDictionary<string, User> _users = new ConcurrentDictionaryIDictonaryAdapter<string, User>();
        public virtual void Load([FromConfig("user.path")]string path)
        {

        }
        public bool CreateUser(string name,string password,string group)
        {
            if (!_groups.ContainsKey(group))
                return false;

            if(_groups[group].Users.ContainsKey(name))
            {
                return false;
            }
            else
            {
                _groups[group].Users.Add(name, new User() { Name = name, Group = group, Password = password });

                makeSnapshot();

                return true;
            }
        }

        public bool CreateGroup(string name,AuntenticateLevel level)
        {
            if (_groups.ContainsKey(name))
                return false;

            _groups.Add(name, new UserGroup() { Name = name, Level = level });

            makeSnapshot();

            return true;
        }
        public bool UserExists(string name)
        {
            return _users.ContainsKey(name);
        }

        public bool Validate(string name,string password)
        {
            return _users.TryGetValue(name, out var value) ? value.Password == password : false;
        }

        public AuntenticateLevel? GetUserLevel(string name)
        {
            return _users.TryGetValue(name, out var value) ? 
                                                      _groups.TryGetValue(value.Group,out var group)?(AuntenticateLevel?)group.Level:null
                                                                                                                               :null;
        }
        public AuntenticateLevel? GetGroupLevel(string name)
        {
            return _groups.TryGetValue(name, out var group) ? (AuntenticateLevel?)group.Level : null;
        }
        public bool GroupExists(string name)
        {
            return _groups.ContainsKey(name);
        }

        public bool UpdatePassword(string name,string password)
        {
             if(_users.TryGetValue(name,out var user))
            {
                user.Password = password;

                makeSnapshot();

                return true;
            }
             else
            {
                return false;
            }
        }
        public bool ChangeGroup(string user,string newGroup)
        {
            if(_users.TryGetValue(user,out var usr)&&_groups.TryGetValue(newGroup,out var group))
            {
                _groups[usr.Group].Users.Remove(user);

                usr.Group = newGroup;

                makeSnapshot();

                group.Users.Add(user, usr);

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ChangeGroupLevel(string group, AuntenticateLevel newLevel)
        {
            if(_groups.TryGetValue(group,out var value))
            {
                value.Level = newLevel;

                makeSnapshot();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveUser(string user)
        {
            if(_users.TryGetValue(user,out var value))
            {
                _groups[value.Group].Users.Remove(user);
                _users.Remove(user);

                makeSnapshot();

                return true;
            }

            return false;
        }

        public bool RemoveGroup(string group)
        {
            if(_groups.ContainsKey(group))
            {
                foreach (var item in _groups[group].Users)
                {
                    RemoveUser(item.Key);
                }

                _groups.Remove(group);

                makeSnapshot();

                return true;
            }

            return false;
        }

        private void makeSnapshot()
        {

        }

        public UserGroup[] GetAll()
        {
            return _groups.Values.ToArray();
        }
    }
}