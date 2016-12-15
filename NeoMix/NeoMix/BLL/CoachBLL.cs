using NeoMix.DAL;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.BLL
{
    public class CoachBLL
    {
        private CoachDAL _coachDAL = new CoachDAL();

        public List<Coach> CoachList()
        {
            return _coachDAL.CoachList();
        }

        public bool CoachCreate(Coach coach)
        {
            return _coachDAL.CoachCreate(coach);
        }

        public Coach CoachSelect(int id_Coach)
        {
            return _coachDAL.CoachSelect(id_Coach);
        }

        public List<Coach> CoachSelectHome(int views)
        {
            return _coachDAL.CoachSelectHome(views);
        }

        public bool CoachUpdate(Coach coach)
        {
            return _coachDAL.CoachUpdate(coach);
        }
    }    
}