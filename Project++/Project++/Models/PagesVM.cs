using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Project__.Models
{
    public class UsersVM
    {
        public User Users { get; set; }
        public Chat Chat { get; set; }
        public Event Events { get; set; }
        public Projects Group { get; set; }
        public GroupMember GroupMembers { get; set; }
        public Log Log { get; set; }
        public Assignment Taks { get; set; }
        
    }

    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DefaultGroupID { get; set; }
    }

    public class Chat
    {
        [Key]
        public int GroupID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }

    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public int GroupID { get; set; }
        public string EventName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string EventDescription { get; set; }
        public int CreatedByID { get; set; }
    }

    public class Projects
    {
        [Key]
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public int TeamLeaderID { get; set; }
        public string Description { get; set; }
    }

    public class GroupMember
    {
        [Key]
        public int GroupID { get; set; }
        public int UserID { get; set; }
        public string UserTitle { get; set; }
    }

    public class Log
    {
        [Key]
        public int LogID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ActionType { get; set; }
        public string UserName { get; set; }
        public int GroupID { get; set; }
    }

    public class Assignment
    {
        [Key]
        public int GroupID { get; set; }
        public string TaskName { get; set; }
        public int AssignedUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class PlusPlusContext : DbContext
    {
        public PlusPlusContext() : base("name=PlusPlus") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Projects> Project { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Assignment> Tasks { get; set; }
       
        
    }
}