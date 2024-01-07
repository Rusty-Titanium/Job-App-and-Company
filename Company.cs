using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_App_and_Company
{
    public class Company
    {
        private String name, notes;
        private Trust canTrust;
        private bool onGovWebsite;
        private List<JobApplication> applications; // will probably change from object to custom class that I make later.

        public Company ()
        {
            applications = new List<JobApplication>();
        }

        public Company(String name, String notes, Trust canTrust, bool onGovWebsite)
        {
            this.name = name;
            this.notes = notes;
            this.canTrust = canTrust;
            this.onGovWebsite = onGovWebsite;
            applications = new List<JobApplication>();
        }

        public String Name
        {
            get { return name; }

            set { name = value; }
        }

        public Trust Trust
        {
            get { return canTrust; }

            set { canTrust = value; }
        }

        public bool OnGovWebsite
        {
            get { return onGovWebsite; }

            set { onGovWebsite = value; }
        }

        public List<JobApplication> Applications
        {
            get { return applications; }

            set {  applications = value; }
        }

        public String Notes
        {
            get { return notes; }

            set { notes = value; }
        }


    }
}
