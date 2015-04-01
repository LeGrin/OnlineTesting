using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMPR_testing_Lib.Domain;

namespace SMPRShedulerService
{
    public class SessionList
    {
        private ISet<KeyValuePair<int, DateTime> > _allSessions;
        
        public bool Contains(KeyValuePair<int, DateTime> session) 
        {
            return _allSessions.Contains(session);
        }
        
        public void AddSession(KeyValuePair<int, DateTime> session)
        {
            _allSessions.Add(session);
        }

        public IList<KeyValuePair<int, DateTime>> GetAllSessions()
        {
            return _allSessions.ToList();
        }

        public void Remove(KeyValuePair<int, DateTime> session)
        {
            if (_allSessions.Contains(session))
            {
                _allSessions.Remove(session);
            }
        }
    }
}
