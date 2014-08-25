using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Perscribo.EF.Library.Models;

namespace Perscribo.EF.Library.DAL
{
    public class PerscriboInitializer : DropCreateDatabaseIfModelChanges<PerscriboContext>
    {
        protected override void Seed(PerscriboContext context)
        {
            AddMyProsperity(context);
            AddSage(context);
            AddDnb(context);
            AddOtherApplications(context);
        }

        private void AddMyProsperity(PerscriboContext db)
        {
            #region Application Submitted

            var roles = new List<Role>
            {
                new Role {
                    PositionTitle = "Senior Developer",
                    AppliedForOn = DateTime.Parse("15 December 2012 10:00"),
                    PositionType = PositionType.Permanent,
                    LowSalaryRange = 100000,
                    SalaryType = SalaryType.Salary_Package,
                    Status = RoleStatus.Company_Interview,
                    Company = new Company{
                        Name = "My Prosperity",
                        Address = new Address{
                            Street1 = "Firt Floor",
                            Street2 = "1 Oxley Road",
                            Suburb = "Hawthorn",
                            StateID = StateName.Victoria,
                            Postcode = "3122"
                        },
                        People = new List<Person>
                        {
                            new Person{FirstName = "Stephen", LastName = "Jackel"}
                        }
                    }
                }
            };
            roles[0].CompanyInterviews = new List<Interview>
            {
                new Interview
                {
                    InterviewDate = DateTime.Parse("15 December 2012 14:30"),
                    Interviewers = new List<Person>
                    {
                        roles[0].Company.People.First()
                    }
                },
                new Interview 
                {
                    InterviewDate = DateTime.Parse("17 December 2012 14:30"),
                    Interviewers = new List<Person>
                    {
                        roles[0].Company.People.First()
                    }
                }
            };

            //  lets save the role now
            roles.ForEach(r => db.Roles.Add(r));
            db.SaveChanges();

            #endregion

            #region Hired
            roles[0].Status = RoleStatus.Closed;

            var engagements = new List<Engagement>
            {
                new Engagement
                {
                    Address = roles[0].Company.Address,
                    Commencement = DateTime.Parse("7 Jan 2013"),
                    Completion = DateTime.Parse("8 Feb 2013"),
                    Company = roles[0].Company,
                    CompanyContact = roles[0].Company.People.First()
                }
            };
            engagements.ForEach(e => db.Engagements.Add(e));
            db.SaveChanges();

            #endregion
        }

        private void AddSage(PerscriboContext db)
        {
            var roles = new List<Role>
            {
                new Role
                {
                    AppliedForOn = DateTime.Parse("10 Feb 2012 14:00"),
                    Company = new Company
                    {
                        Name = "Sage Technology",
                        People = new List<Person>
                        {
                            new Person{ FirstName = "Michael", LastName = "Calagaz", PhoneNumber = "5132 2600" },
                            new Person{ FirstName = "Steven", LastName = "van der" }
                        },
                        Address = new Address
                        {
                            Street1 = "21 Hazelwood Road",
                            Suburb = "Morwell",
                            StateID = StateName.Victoria,
                            Postcode = "3840"
                        }
                    },
                    LowSalaryRange = 70000,
                    SalaryType = SalaryType.Salary_Package,
                    PositionTitle = "Senior Developer",
                    PositionType = PositionType.Permanent,
                    Status = RoleStatus.Closed
                }
            };
            roles[0].CompanyInterviews = new List<Interview>
            {
                new Interview{
                    InterviewDate = DateTime.Parse("7 Mar 2013"),
                    Interviewers = new List<Person>
                    {
                        roles[0].Company.People.First(),
                        roles[0].Company.People.Last()
                    }
                }
            };

            var engagements = new List<Engagement>
            {
                new Engagement
                {
                    Address = roles[0].Company.Address,
                    Commencement = DateTime.Parse("2 April 2013"),
                    Completion = DateTime.Parse("6 Dec 2013"),
                    Company = roles[0].Company,
                    CompanyContact = roles[0].Company.People.First()
                }
            };

            roles.ForEach(r => db.Roles.Add(r));
            engagements.ForEach(e => db.Engagements.Add(e));
            db.SaveChanges();
        }

        private void AddDnb(PerscriboContext db)
        {
            #region Application Submitted

            var roles = new List<Role>
            {
                new Role {
                    PositionTitle = "Developer",
                    AppliedForOn = DateTime.Parse("30 December 2013 10:00"),
                    PositionType = PositionType.Contract,
                    LowSalaryRange = 550,
                    SalaryType = SalaryType.Per_Day,
                    Status = RoleStatus.Applied_For
                }
            };
            roles[0].Agency = new Agency
            {
                Name = "Ethos Corporation",
                PhoneNumber = "9604 9000",
                Address = new Address
                {
                    Street1 = "Level 31",
                    Street2 = "140 William Street",
                    Suburb = "Melbourne",
                    StateID = StateName.Victoria,
                    Postcode = "3000"
                },
                Consultants = new List<Consultant>
                {
                    new Consultant
                    {
                        FirstName = "Courtney"
                    }
                }
            };
            roles[0].Consultant = roles[0].Agency.Consultants.First();

            //  lets save the role now
            roles.ForEach(r => db.Roles.Add(r));
            db.SaveChanges();

            #endregion

            #region First contact from agent, and resume sent to company

            roles[0].Consultant.LastName = "Ulshafer";
            roles[0].Consultant.Email = "courtney@ethoscorp.com.au";
            roles[0].Consultant.PhoneNumber = "0423 462 162";

            roles[0].Company = new Company
            {
                Name = "Dun & Bradstreet",
                Address = new Address
                {
                    Street1 = "Level 5",
                    Street2 = "Dun & Bradstreet House",
                    Street3 = "479 St Kilda Road",
                    Suburb = "Melbourne",
                    StateID = StateName.Victoria,
                    Postcode = "3004"
                },
            };

            roles[0].Status = RoleStatus.Resume_To_Company;

            db.SaveChanges();

            #endregion

            #region Company Interview

            roles[0].CompanyInterviews = new List<Interview>
            {
                new Interview{
                    InterviewDate = DateTime.Parse("5 Jan 2014 2:00PM"),
                    Interviewers = new List<Person>
                    {
                        new Person { FirstName = "Stephen", LastName = "Moore", Company = roles[0].Company },
                        new Person { FirstName = "Sid", LastName = "Hotta", Company = roles[0].Company}
                    }
                }
            };
            roles[0].Status = RoleStatus.Company_Interview;
            db.SaveChanges();

            #endregion

            #region Hired!!!

            roles[0].Status = RoleStatus.Closed;

            var engagements = new List<Engagement>
            {
                new Engagement
                {
                    Address = roles[0].Company.Address,
                    Agency = roles[0].Agency,
                    Commencement = DateTime.Parse("9 Jan 2014"),
                    Company = roles[0].Company,
                    Consultant = roles[0].Agency.Consultants.First(),
                    CompanyContact = roles[0].Company.People.Where(p => p.LastName == "Moore").First()
                }
            };
            engagements[0].CompanyContact.Email = "moores@dnb.com.au";
            engagements[0].CompanyContact.PhoneNumber = "9828 3201";
            engagements.ForEach(e => db.Engagements.Add(e));

            db.SaveChanges();

            #endregion

            #region Finish off the data - projects, project managers, tasks

            engagements[0].Projects = new List<Project>
            {
                new Project { 
                    Title = "P639 CRP Architecture - AU Product & Load", 
                    Engagement = engagements[0],
                    ProjectManager = new Person{
                        FirstName = "Marlene",
                        LastName = "Parker",
                        PhoneNumber = "9828 3439",
                        Email = "parkerm@dnb.com.au",
                        Company = engagements[0].Company
                    },
                    Tasks = new List<Task>
                    {
                        new Task { Title = "Development - Monitors" },
                        new Task { Title = "Development - Scoring Engine" },
                        new Task { Title = "Development - Web Channel" },
                        new Task { Title = "Induction and setup" },
                        new Task { Title = "Meetings" },
                        new Task { Title = "Moving" },
                        new Task { Title = "Team Meetings" },
                        new Task { Title = "No Task" }
                    }
                },
                new Project {
                    Title = "P908 CRS Programme of Work",
                    Engagement = engagements[0],
                    Tasks = new List<Task>
                    {
                        new Task { Title = "Web Channel" },
                        new Task { Title = "Back Office" }
                    }
                },
                new Project {
                    Title = "P920 CRS Enhancements and Fixes",
                    Engagement = engagements[0],
                    ProjectManager = new Person{
                        FirstName = "Alpa",
                        LastName = "Nangia",
                        PhoneNumber = "9828 3171",
                        Email = "nangiaa@dnb.com.au",
                        Company = engagements[0].Company
                    },
                    Tasks = new List<Task>
                    {
                        new Task { Title = "Account Maintenance Defects" }
                    }
                }
            };
            engagements[0].Projects.Where(p => p.Title == "P908 CRS Programme of Work").First().ProjectManager = engagements[0].Projects.First().ProjectManager;
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string mess = ex.Message;
            }

            #endregion
        }

        private void AddOtherApplications(PerscriboContext db)
        {
            var roles = new List<Role>
            {
                new Role {
                    PositionTitle = "Senior .Net Developer (Front end focus)", 
                    AppliedForOn = DateTime.Parse("2 Aug 2014 2:00PM"),
                    PositionType = PositionType.Fixed_Term, 
                    LowSalaryRange = 120000, 
                    SalaryType = SalaryType.Salary_Package, 
                    Status = RoleStatus.Resume_To_Company,
                    Agency = new Agency 
                    { 
                        Name = "RDBMS Resource Solutions", 
                        PhoneNumber = "1300 552 731",
                        Address = new Address
                        {
                            Street1 = "Level 27", 
                            Street2 = "459 Collins Street", 
                            Suburb = "Melbourne", 
                            StateID = StateName.Victoria, 
                            Postcode = "3000"
                        },
                        Consultants = new List<Consultant>
                        {
                            new Consultant
                            {
                                FirstName = "Jessica", 
                                LastName = "Burns", 
                                PhoneNumber = "8602 6407", 
                                Email = "Jessica.Burns@rdbms.com.au"
                            }
                        }
                    }, 
                    Company = new Company{Name = "Lonsec" }
                },
                new Role 
                { 
                    PositionTitle = "Senior Developer-C#-Asp.net", 
                    AppliedForOn = DateTime.Parse("8 Aug 2014 17:54"), 
                    PositionType = PositionType.Permanent, 
                    LowSalaryRange = 100000,
                    HighSalaryRange = 120000,
                    SalaryType = SalaryType.Salary,
                    ReferenceNumber = "12966/AB", 
                    Status = RoleStatus.Applied_For,
                    Agency = new Agency{
                        Name = "eCareer", 
                        PhoneNumber = "8641 6888", 
                        Address = new Address{ 
                            Street1 = "Level 9", 
                            Street2 = "423 Bourke Street", 
                            Suburb = "Melbourne", 
                            StateID = StateName.Victoria, 
                            Postcode = "3000"
                        },
                        Consultants = new List<Consultant>
                        {
                            new Consultant{FirstName = "Andy", LastName = "Beale", PhoneNumber = "8641 6804"} 
                        }
                    }
                },
                new Role 
                { 
                    PositionTitle = "Senior Software Engineer", 
                    AppliedForOn = DateTime.Parse("8 Aug 2014 16:55"), 
                    PositionType = PositionType.Permanent, 
                    LowSalaryRange = 140000,
                    SalaryType = SalaryType.Salary,
                    Status = RoleStatus.Applied_For,
                    Agency = new Agency{
                        Name = "Command Group", 
                        PhoneNumber = "9621 3399", 
                        Address = new Address{ 
                            Street1 = "Level 5", 
                            Street2 = "461 Bourke Street", 
                            Suburb = "Melbourne", 
                            StateID = StateName.Victoria, 
                            Postcode = "3000"
                        },
                        Consultants = new List<Consultant>
                        {
                            new Consultant{FirstName = "Tamba", LastName = "Sumana", PhoneNumber = "9621 3399"} 
                        }
                    }
                }
            };
            //  need to link consultant to role
            for (int i = 0; i < 3; i++)
            {
                roles[i].Consultant = roles[i].Agency.Consultants.First();
            }

            roles.ForEach(r => db.Roles.Add(r));
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string temp = ex.Message;
            }
        }
    }
}
