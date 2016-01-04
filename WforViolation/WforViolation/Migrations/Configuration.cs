namespace WforViolation.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WforViolation.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WforViolation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WforViolation.Models.ApplicationDbContext context)
        {
            context.ViolationMainTypes.AddOrUpdate(x => x.Name,
              new ViolationMainType
              {
                  Name = "Pavement Ramp Violation",
                  ViolationSubTypes = new List<ViolationSubType>()
  
  {
   new ViolationSubType(){
Name="Slope",
ViolationDetail=new ViolationDetail()
{
    PropertyName="Slope",
    PropertyType=PropertyType.Double,
    LowSeverityValue=0.083,
    MediumSeverityValue=0.09,
    HighSeverityValue=0.1,
    CatastrophicSeverityValue=0.11,
    SeverityRelationType=SeverityRelationType.Ascending
}
},
new ViolationSubType()
{
    Name="Width",
ViolationDetail=new ViolationDetail()
{
    PropertyName="Width",
    PropertyType=PropertyType.Integer,
    LowSeverityValue=915,
    MediumSeverityValue=890,
    HighSeverityValue=850,
    CatastrophicSeverityValue=830,
    SeverityRelationType=SeverityRelationType.Descending
}
},
new ViolationSubType()
{
    Name="Surface",
ViolationDetail=new ViolationDetail()
{
    PropertyName="Surface",
    PropertyType=PropertyType.Boolean
}
}
  }
              },
                            new ViolationMainType
                            {
                                Name = "Architectural Violations",
                                ViolationSubTypes = new List<ViolationSubType>()
  
  {
   new ViolationSubType(){
Name="Level differences",
ViolationDetail=new ViolationDetail()
{
    PropertyName="Level differences",
    PropertyType=PropertyType.Boolean
}
},
new ViolationSubType()
{
    Name="Water grating",
ViolationDetail=new ViolationDetail()
{
    PropertyName="Water grating",
    PropertyType=PropertyType.Boolean
}
},
new ViolationSubType()
{
    Name="Obstacle",
ViolationDetail=new ViolationDetail()
{
    PropertyName="Obstacle",
    PropertyType=PropertyType.Boolean
}
},
new ViolationSubType()
{
    Name="Surface",
ViolationDetail=new ViolationDetail()
{
    PropertyName="Surface",
    PropertyType=PropertyType.Boolean
}
},
new ViolationSubType()
{
    Name="Street drain",
ViolationDetail=new ViolationDetail()
{
    PropertyName="Street drain",
    PropertyType=PropertyType.Double,
    LowSeverityValue=13,
    MediumSeverityValue=14,
    HighSeverityValue=15,
    CatastrophicSeverityValue=16,
    SeverityRelationType=SeverityRelationType.Ascending
}
}
  }
                            },

                            new ViolationMainType
                            {
                                Name = "Car Parking Area Violations",
                                ViolationSubTypes = new List<ViolationSubType>()
  
  {
   new ViolationSubType(){
Name="Capacity for Employee at Existing BC",
ViolationDetail=new ViolationDetail()
{
 PropertyName="Capacity for Employee at Existing BC",
    PropertyType=PropertyType.Integer,
    LowSeverityValue=1,
    MediumSeverityValue=2,
    HighSeverityValue=3,
    CatastrophicSeverityValue=4,
    SeverityRelationType=SeverityRelationType.Ascending
}
},
new ViolationSubType()
{
    Name="Capacity for Visitor at Existing BC",
ViolationDetail=new ViolationDetail()
{
PropertyName="Capacity for Visitor at Existing BC",
    PropertyType=PropertyType.Double,
    LowSeverityValue=1.8,
    MediumSeverityValue=1.7,
    HighSeverityValue=1.3,
    CatastrophicSeverityValue=1,
    SeverityRelationType=SeverityRelationType.Descending
}
}

  }
                            },
  new ViolationMainType
  {
      Name = "Bus Station Violations",
      ViolationSubTypes = new List<ViolationSubType>()
  
  {
   new ViolationSubType(){
Name="Non Standard Bus",
ViolationDetail=new ViolationDetail()
{
 PropertyName="Non Standard Bus",
    PropertyType=PropertyType.Boolean
}
},
new ViolationSubType()
{
    Name="No Textured-surface",
ViolationDetail=new ViolationDetail()
{
PropertyName="No Textured-surface",
    PropertyType=PropertyType.Boolean
}
}

  }
  },
    new ViolationMainType
    {
        Name = "Street Decoration Violations",
        ViolationSubTypes = new List<ViolationSubType>()
  
  {
   new ViolationSubType(){
Name="Obstacle",
ViolationDetail=new ViolationDetail()
{
 PropertyName="Obstacle",
    PropertyType=PropertyType.Boolean
}
}

  }
    },
        new ViolationMainType
        {
            Name = "Building Reachability Violations",
            ViolationSubTypes = new List<ViolationSubType>()
  
  {
   new ViolationSubType(){
Name="No Safety Rail",
ViolationDetail=new ViolationDetail()
{
 PropertyName="No Safety Rail",
    PropertyType=PropertyType.Boolean
}
},
new ViolationSubType()
{
    Name="Safety Rail Width",
ViolationDetail=new ViolationDetail()
{
PropertyName="Safety Rail Width",
    PropertyType=PropertyType.Double,
    LowSeverityValue=1.5,
    MediumSeverityValue=1.4,
    HighSeverityValue=1.2,
    CatastrophicSeverityValue=1,
    SeverityRelationType=SeverityRelationType.Descending
}
}

  }
        },
        new ViolationMainType
        {
            Name = "Building Element Violations",
            ViolationSubTypes = new List<ViolationSubType>()
  
  {
   new ViolationSubType(){
Name="WC",
ViolationDetail=new ViolationDetail()
{
 PropertyName="WC",
    PropertyType=PropertyType.Boolean
}
},
new ViolationSubType()
{
    Name="Stoop",
ViolationDetail=new ViolationDetail()
{
PropertyName="Stoop",
 PropertyType=PropertyType.Boolean
}
}

  }
        },
        new ViolationMainType
        {
            Name = "Lightning Violations",
            ViolationSubTypes = new List<ViolationSubType>()
  
  {
   new ViolationSubType(){
Name="Bright",
ViolationDetail=new ViolationDetail()
{
 PropertyName="Bright",
    PropertyType=PropertyType.Boolean
}
},
new ViolationSubType()
{
    Name="Heterogeneous",
ViolationDetail=new ViolationDetail()
{
PropertyName="Heterogeneous",
 PropertyType=PropertyType.Boolean
}
}

  }
        },
        new ViolationMainType
        {
            Name = "Colour Violations",
            ViolationSubTypes = new List<ViolationSubType>()
  
  {
   new ViolationSubType(){
Name="Non Standard Colour",
ViolationDetail=new ViolationDetail()
{
 PropertyName="Non Standard Colour",
    PropertyType=PropertyType.Boolean
}
},


  }
        },
        new ViolationMainType
        {
            Name = "Switch or Plug Violations",
            ViolationSubTypes = new List<ViolationSubType>()
  
  {
   new ViolationSubType(){
Name="Non Standard Switch or Plug",
ViolationDetail=new ViolationDetail()
{
 PropertyName="Non Standard Switch or Plug",
    PropertyType=PropertyType.Boolean
}
}
  }
        }


            );


            RolesHelper.AddRoles(context);
        }
    }

    public class RolesHelper
    {
        public static void AddRoles(ApplicationDbContext context)
        {
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("123456");
            var store = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(store);
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!RoleManager.RoleExists("Admin"))
            {
                RoleManager.Create(new IdentityRole("Admin"));
            }
            if (!RoleManager.RoleExists("User"))
            {
                RoleManager.Create(new IdentityRole("User"));
            }
            if (!RoleManager.RoleExists("Editor"))
            {
                RoleManager.Create(new IdentityRole("Editor"));
            }
            var user = userManager.FindByName("hakan");
            if (user == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "hakan",
                    FirstName = "hakan",
                    LastName = "hasdagli",
                    Email = "hakan@gmail.com"
                };

                userManager.Create(newUser, "123456");
                userManager.AddToRole(newUser.Id, "Admin");

            }
            //if (!context.Users.Any(u => u.UserName == "founder"))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    var user = new ApplicationUser { UserName = "erhan" };

            //    manager.Create(user, "ChangeItAsap!");
            //    manager.AddToRole(user.Id, "AppAdmin");
            //}
        }
    }
}
