﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace petslogger.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbPetsloggerEntities : DbContext
    {
        public dbPetsloggerEntities()
            : base("name=dbPetsloggerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tb_comment> tb_comment { get; set; }
        public virtual DbSet<tb_friend> tb_friend { get; set; }
        public virtual DbSet<tb_like> tb_like { get; set; }
        public virtual DbSet<tb_messages> tb_messages { get; set; }
        public virtual DbSet<tb_notification> tb_notification { get; set; }
        public virtual DbSet<tb_post> tb_post { get; set; }
        public virtual DbSet<tb_user> tb_user { get; set; }
    }
}
