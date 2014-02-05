using System.Collections.Generic;
using System.Linq;
using TeamBins.DataAccess;
using TechiesWeb.TeamBins.ViewModels;
using System;
using TeamBins.Helpers.Enums;

namespace TeamBins.Services
{
    public class IssueService : IDisposable
    {
        IRepositary repo;
        public string SiteBaseURL { set; get; }
      
        public IssueService(IRepositary repositary)
        {
            repo = repositary;
        }

        public List<ActivityVM> GetTeamActivityVMs(int teamId)
        {
            List<ActivityVM> activityVMList = new List<ActivityVM>();
            try
            {
                var activityList = repo.GetTeamActivity(teamId).OrderByDescending(s => s.CreatedDate).ToList();

                foreach (var item in activityList)
                {
                    var activityVM = GetActivityVM(item);
                    activityVMList.Add(activityVM);
                }
            }
            catch (Exception ex)
            {
                
            }
            return activityVMList;
        }

        public ActivityVM GetActivityVM(Activity item)
        {
            var activityVM = new ActivityVM() { Author = item.User.FirstName, CreatedDateRelative = item.CreatedDate.ToString() };
            if (item.ActivityDesc.ToUpper() == "CREATED")
            {
                activityVM.Activity = item.ActivityDesc;
                activityVM.ObjectTite = item.NewState;
                activityVM.ObjectURL = String.Format("{0}Issues/details/{1}", SiteBaseURL, item.ObjectID);
            }
            return activityVM;
        }

        public void LoadIssueMembers(int id, IssueVM issueVm,int currentUserId=0)
        {
            var issueMemberRelations = repo.GetIssueMembers(id);

            //check the current user has starred the issue
            if (currentUserId != 0)
            {
                var isCurrentUserStarredIssue = issueMemberRelations.Where(s => s.RelationType == IssueMemberRelationType.Star.ToString() && s.MemberID == currentUserId).FirstOrDefault();
                issueVm.IsStarredForUser = isCurrentUserStarredIssue != null;
            }
            
            //Now get the members assigned to this issue
            var issueMemberList = issueMemberRelations.Where(s => s.RelationType ==IssueMemberRelationType.Member.ToString());
            foreach (var member in issueMemberList)
            {
                var vm = new MemberVM { MemberType = member.Member.JobTitle, Name = member.Member.FirstName, MemberID = member.MemberID };
                vm.AvatarHash = UserService.GetImageSource(member.Member.EmailAddress);
                issueVm.Members.Add(vm);
            }
        }
        public bool SaveIssueMember(int issueId, int userId, int createdById)
        {
            return SaveIssueMemberRelation(issueId, userId, IssueMemberRelationType.Member,createdById);
        }

        public bool StarIssue(int issueId, int userId)
        {
            return SaveIssueMemberRelation(issueId, userId, IssueMemberRelationType.Star,userId);
        }
        public bool UnStarIssue(int issueId, int userId)
        {
            return DeleteIssueMemberRelation(issueId, userId, IssueMemberRelationType.Star);
        }
        private bool DeleteIssueMemberRelation(int issueId, int userId, IssueMemberRelationType relationType)
        {
            try
            {
                var issueMember = new IssueMember { MemberID = userId, IssueID = issueId,  RelationType = relationType.ToString() };
                var result = repo.DeleteIssueMemberRelation(issueMember);
            }
            catch (Exception ex)
            {
                // to do : Log
                return false;
            }
            return true;
        }
        private bool SaveIssueMemberRelation(int issueId, int userId, IssueMemberRelationType relationType, int createdById)
        {
            try
            {
                var issueMember = new IssueMember { MemberID = userId, IssueID = issueId, CreatedByID=createdById, RelationType = relationType.ToString() };
                var result = repo.SaveIssueMemberRelation(issueMember);
            }
            catch(Exception ex)
            {
                // to do : Log
                return false;
            }
            return true;    
        }
        

        public void Dispose()
        {
            if (repo != null)
            {
                repo.Dispose();
            }
        }
    }
}
