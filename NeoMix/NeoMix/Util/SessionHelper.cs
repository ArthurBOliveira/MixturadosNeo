using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.Util
{
    public class SessionHelper
    {
        private const string SessionCurrentAdmin = "CurrentAdmin";
        private const string SessionTemp = "Temp";

        private System.Web.SessionState.HttpSessionState _context;

        public SessionHelper(System.Web.SessionState.HttpSessionState stateBase)
        {
            _context = stateBase;
        }

        public string SessionId
        {
            get
            {
                return _context.SessionID;
            }
        }

        public Admin CurrentPlayer
        {
            get
            {
                if (_context[SessionCurrentAdmin] == null)
                    _context[SessionCurrentAdmin] = new Admin();
                return (Admin)_context[SessionCurrentAdmin];
            }
            set
            {
                _context[SessionCurrentAdmin] = value;
            }
        }

        public bool Temp
        {
            get
            {
                if (_context[SessionTemp] == null)
                    _context[SessionTemp] = false;
                return (bool)_context[SessionTemp];
            }
            set
            {
                _context[SessionTemp] = value;
            }
        }
    }
}