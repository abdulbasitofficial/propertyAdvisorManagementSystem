﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Property
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class propertyEntities : DbContext
    {
        public propertyEntities()
            : base("name=propertyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<aacount> aacounts { get; set; }
        public virtual DbSet<company> companies { get; set; }
        public virtual DbSet<offer> offers { get; set; }
        public virtual DbSet<property> properties { get; set; }
        public virtual DbSet<rating> ratings { get; set; }
        public virtual DbSet<refer> refers { get; set; }
        public virtual DbSet<referproperty> referproperties { get; set; }
    }
}
