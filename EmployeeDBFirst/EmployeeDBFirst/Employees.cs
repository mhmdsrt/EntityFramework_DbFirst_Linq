//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeDBFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employees
    {
        public short EmployeeID { get; set; }
        public Nullable<short> DepartmentID { get; set; }
        public Nullable<short> AddressID { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public Nullable<int> Salary { get; set; }
        public string Gender { get; set; }
    
        public virtual Addresses Addresses { get; set; }
        public virtual Departments Departments { get; set; }
    }
}
