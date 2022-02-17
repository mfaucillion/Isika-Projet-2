﻿using Microsoft.AspNetCore.Mvc;
using Projet2Crowdfunding.Models;
using Projet2Crowdfunding.Service;
using Projet2Crowdfunding.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet2Crowdfunding.Controllers
{
    public class ProjectController : Controller
    {
        private BddContext bddContext;
        private AccountService accountService;
        private ProjectService projectService;

        public ProjectController()
        {
            accountService = new AccountService();
            projectService = new ProjectService();
            bddContext = new BddContext();
        }

        public IActionResult Index()
        {
            AccountViewModel viewModel = new AccountViewModel { Authentify = HttpContext.User.Identity.IsAuthenticated }; //cookies
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.Account = accountService.GetAccount(HttpContext.User.Identity.Name);
            }
            viewModel.ProjectList = projectService.GetAllProjects();

            return View(viewModel);
        }

        public IActionResult ProjectPage(int? id)

        {
            AccountViewModel viewModel = new AccountViewModel { Authentify = HttpContext.User.Identity.IsAuthenticated }; //cookies
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.Account = accountService.GetAccount(HttpContext.User.Identity.Name);
            }
            if (id.HasValue)
            {
                viewModel.Project = projectService.GetProject(id.Value);
                viewModel.ProjectOwner = this.bddContext.ProjectOwners.Find(viewModel.Project.ProjectOwnerId);

            }

            return View(viewModel);
        }


        /*      public IActionResult Index()
              {
                  AccountViewModel viewModel = new AccountViewModel { Authentify = HttpContext.User.Identity.IsAuthenticated }; //cookies
                  if (HttpContext.User.Identity.IsAuthenticated)
                  {
                      viewModel.Account = accountService.GetAccount(HttpContext.User.Identity.Name);
                      viewModel.ProjectOwner = accountService.GetProjectOwnerFromAccountId(viewModel.Account.Id);
                      viewModel.Project = accountService.GetProjectFromProjectOwnerId(viewModel.ProjectOwner.Id);
                      return View(viewModel);
                  }
                  else { 

                  }
                  return View(viewModel);

              }
              public IActionResult ProjectPage()
              {
                  AccountViewModel viewModel = new AccountViewModel { Authentify = HttpContext.User.Identity.IsAuthenticated }; //cookies
                  if (HttpContext.User.Identity.IsAuthenticated)
                  {
                      viewModel.Account = accountService.GetAccount(HttpContext.User.Identity.Name);
                      viewModel.ProjectOwner = accountService.GetProjectOwnerFromAccountId(viewModel.Account.Id);
                      viewModel.Project = accountService.GetProjectFromProjectOwnerId(viewModel.ProjectOwner.Id);
                      return View(viewModel);
                  }
                  return View(viewModel);


              }*/
        public IActionResult PaymentPage()//view de dons financiers à faire
        {
            return View("PaymentPage");
        }
        
    }
}