using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeamBins.Common.ViewModels
{
    public class IssueListVM
    {
        public bool IsReadonlyView { set; get; }
        public bool IsPublicTeam { set; get; }
        public int TeamID { set; get; }
        public List<IssueVM> Bugs { set; get; }

        public IssueListVM()
        {
            Bugs = new List<IssueVM>();
        }

        public string CurrentTab { set; get; }
        public bool ProjectsExist { set; get; }
        public bool IsUserTeamMember { set; get; }
        //public bool IsDefaultProjectSet { set; get; }
    }



    public class NewIssueCommentVM
    {
        [Required]
        //[AllowHtml]
        public string CommentBody { set; get; }
        [Required]
        public int IssueID { set; get; }
    }

    public class DeleteIssueConfirmationVM : IssueVM
    {

    }



    public class IssueDetailWithStatusGroup : IssueDetailVM
    {

        public string StatusGroupName { set; get; }

    }
    public class IssueDetailVM : IssueVM
    {
        public bool IsReadOnly { set; get; }
        public bool IsStarredForUser { set; get; }
        public List<UploadDto> Images { set; get; }
        public List<UploadDto> Attachments { set; get; }
        public IEnumerable<UserDto> Members { set; get; }
        //    public List<CommentVM> Comments { set; get; }
        public bool IsEditableForCurrentUser { set; get; }
        public int TeamID { set; get; }
        public int ProjectId { set; get; }
        public string LastModifiedDate { set; get; }
        public string LastModifiedBy { set; get; }
        public IssueDetailVM()
        {
            Images = new List<UploadDto>();
            Attachments = new List<UploadDto>();
            Members = new List<UserDto>();
            //     Comments = new List<CommentVM>();
        }

        public KeyValueItem Priority { set; get; }
        public KeyValueItem Status { set; get; }
        public KeyValueItem Category { set; get; }
        public KeyValueItem Project { set; get; }
        public string StatusGroupCode { get; set; }
        public KeyValueItem StatusGroup { set; get; }



    }

    public class StatusDto
    {
        // this could be a dapper PR ( Make the mapping work without making the properties (ClassName+Id)
        public int StatusId { set; get; }
        public string StatusName { set; get; }
        public string StatusCode { set; get; }
    }

    public class StatusGroupVm
    {
        public string Name { set; get; }
        public List<IssueVM> Issues { set; get; }
    }
    public class IssuesPerStatusGroup
    {
        public string GroupCode { set; get; }
        public string GroupName { set; get; }
        public List<IssueDetailVM> Issues { set; get; }

        public int DisplayOrder { set; get; }
    }
    public class IssueVM
    {
        public int Id { set; get; }
        [Required]
        public string Title { set; get; }
        [DataType(DataType.MultilineText)]
        // [AllowHtml]
        public string Description { set; get; }
        public string PriorityName { set; get; }
        public string CategoryName { set; get; }
        public string StatusName { set; get; }
        public string StatusCode { set; get; }
        public string OpenedBy { set; get; }
        public string LastModifiedBy { set; get; }
        public string Iteration { set; get; }
        public string ProjectName { get; set; }
        public DateTime CreatedDate { set; get; }

        public DateTime? DueDate { set; get; }
        public string IssueDueDate { set; get; }
        public UserDto Author { get; set; }

        public bool Active { set; get; }
    }


    public class CreateIssue : IssueDetailVM
    {
        public int CreatedById { set; get; }
        public bool IncludeIssueInResponse { set; get; }
        public bool IsFromModalWindow { set; get; }
        public int StatusId { set; get; }
        public int PriorityId { set; get; }
        public int CategoryId { set; get; }


        public List<SelectListItem> Statuses { set; get; }
        public List<SelectListItem> Categories { set; get; }
        public List<SelectListItem> Projects { set; get; }
        public List<SelectListItem> Priorities { set; get; }

        public List<IFormFile> Files { set; get; }

        public CreateIssue()
        {
            this.Statuses = new List<SelectListItem>();
            this.Categories = new List<SelectListItem>();
            this.Projects = new List<SelectListItem>();
            this.Priorities = new List<SelectListItem>();

        }

        public CreateIssue(IssueDetailVM issueDetail)
        {
            this.Title = issueDetail.Title;
            this.Description = issueDetail.Description;
            this.ProjectId = issueDetail.Project.Id;
            this.StatusId = issueDetail.Status.Id;
            this.CategoryId = issueDetail.Category.Id;
            this.PriorityId = issueDetail.Priority.Id;


            this.Statuses = new List<SelectListItem>();
            this.Categories = new List<SelectListItem>();
            this.Projects = new List<SelectListItem>();
            this.Priorities = new List<SelectListItem>();

        }
    }
}