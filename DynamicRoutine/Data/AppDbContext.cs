﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicRoutine.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicRoutine.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Routine> Routines { get; set; }

        public DbSet<RoutineDashboard> RoutineDashboards { get; set; }

        public DbSet<RoutineStep> RoutineSteps { get; set; }
    }
}