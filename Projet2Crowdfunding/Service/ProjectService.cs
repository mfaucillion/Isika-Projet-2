﻿using Microsoft.EntityFrameworkCore;
using Projet2Crowdfunding.Models;
using Projet2Crowdfunding.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet2Crowdfunding.Service
{
    public class ProjectService
    {

        private BddContext bddContext;
        private TimeSpan timeLeftProject;

        public ProjectService()
        {
            bddContext = new BddContext();
        }
        public List<Project> GetProjectsFromProjectOwnerId(int Id)
        {
            List<Project> ProjectList = new List<Project>();
            var projects = bddContext.Projects.Where(p => p.ProjectOwnerId == Id).ToList();
            foreach (Project project in projects)
            {
                ProjectList.Add(project);
            }
            return (ProjectList);
        }
        public List<Project> GetProjectsFromPOIdStatus(int Id, Status? status)
        {
            List<Project> ProjectList = new List<Project>();
            var projects = bddContext.Projects.Where(p => p.ProjectOwnerId == Id && p.Status == status).ToList();
            foreach (Project project in projects)
            {
                ProjectList.Add(project);
            }
            return (ProjectList);
        }


        public List<Project> GetAllProjectsStatus(Status? status)
        {
            List<Project> ProjectList = new List<Project>();
            var projects = bddContext.Projects.Where(p => p.Status == status).ToList();
            foreach (Project project in projects)
            {
                ProjectList.Add(project);
            }
            return (ProjectList);
        }


        public List<Project> GetAllProjects()
        {
            List<Project> projectList = this.bddContext.Projects.ToList();
            return projectList;
        }

        public Project GetProject(int id)
        {
            return this.bddContext.Projects.Find(id);
        }


        public TimeSpan TimeLeftCalculator(int id)
        {

            TimeSpan timeLeftProject = this.bddContext.Projects.Find(id).EndDate.Subtract(DateTime.Now);
            return timeLeftProject;

        }


        public List<Collection> GetAllCollections()
        {
            List<Collection> collectionList = this.bddContext.Collections.ToList();
            return collectionList;

        }


        public List<Step> GetStepsFromProjectId(int? idProject)
        {

            List<Step> ProjectStepsList = new List<Step>();
            var Steps = bddContext.Steps.Where(s => s.ProjectId == idProject).ToList();
            foreach (Step step in Steps)
            {
                ProjectStepsList.Add(step);
            }
            return (ProjectStepsList);


        }

        public void ModifyProjectStatus(int id, Status status)
        {
            BddContext ctx = new BddContext();
            Project project = ctx.Projects.Find(id);

            if (project != null)

                project.Status = status;
            ctx.SaveChanges();
        }
        public ProjectOwner GetProjectOwnerFromProjectId(int Id)
        {
            BddContext ctx = new BddContext();
            Project project = ctx.Projects.Find(Id);
            ProjectOwner projectOwner = this.bddContext.ProjectOwners.FirstOrDefault(p => p.Id == project.ProjectOwnerId);
            Address address = this.bddContext.Addresses.Find(projectOwner.AddressId);
            projectOwner.Address = address;
            return (projectOwner);
        }
        //public Favorite Favorite { get; set; } à rajouter dans accoutnviewmodel
        //compter les coups de coeur sur un projets
        //******** public void setProjectHeartCounter()
        public int CountProjectFavoriteSum(int? idProject)
        {

            /////////////////////////////////////
            Project project = this.bddContext.Projects.Find(idProject);

            List<Favorite> MyFavoritesList = new List<Favorite>();
            var favorites = bddContext.Favorites.Where(f => f.ProjectId == idProject).ToList();

            return favorites.Count;

        }
        public Project AddAFavoriteForAParticipantOnAProject(int idProject, int idParticipant)
        {//recuperer le participant depuis son account
         //puis stocker le favorite 
         //puis afficher le nombre de favorite dans le heartcounter
            BddContext ctx = new BddContext();
            Project project = ctx.Projects.FirstOrDefault(p => p.Id == idProject);
            project.HeartCounter++;
            Favorite favorite = new Favorite();
            favorite.ProjectId = idProject;
            favorite.ParticipantId = idParticipant;

            ctx.SaveChanges();
            //AccountService.GetParticipantFromAccountId(Id);
            return (project);

        }


        public Project SuppressAFavoriteForAParticipantOnAProject(int idProject, int idParticipant)
        {
            BddContext ctx = new BddContext();
            Project project = ctx.Projects.FirstOrDefault(p => p.Id == idProject);
            project.HeartCounter--;
            Favorite favorite = ctx.Favorites.FirstOrDefault(f => f.ProjectId == idProject);
            ctx.Favorites.Remove(favorite);
            ctx.SaveChanges();
            return (project);


        }
        //public void
        //  HeartCounting()
        //// {
        //    Project.HeartCounter++;
        // }
        public Boolean IfExistFavorite(int idProject, int idParticipant)
        {
            BddContext ctx = new BddContext();
            // Project project = ctx.Projects.Find(idProject);
            // Participant participant = ctx.Participants.Find(id);
            Favorite favorite = ctx.Favorites.FirstOrDefault(f => f.ProjectId == idProject && f.ParticipantId == idParticipant);

            return favorite != null;
        }
    }

    }
