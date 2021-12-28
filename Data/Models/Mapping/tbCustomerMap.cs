using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Mapping
{
    public class tbCustomerMap : EntityTypeConfiguration<tbCustomer>
    {
        public tbCustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.PersonID);


            // Table & Column Mappings
            this.ToTable("tbCustomer");
            this.Property(t => t.PersonID).HasColumnName("PersonID");

            this.Property(t => t.Name).HasColumnName("Name");
           
            this.Property(t => t.Email).HasColumnName("Email");
           
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.Phone).HasColumnName("Phone");

            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Postcode).HasColumnName("Postcode");

            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.Address).HasColumnName("Address");

            this.Property(t => t.Accesstime).HasColumnName("Accesstime");


        }
    }
}