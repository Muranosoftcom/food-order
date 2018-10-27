using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IRepository _repo;

        public OrderController(IRepository repo)
        {
            _repo = repo;
        }
    }
}