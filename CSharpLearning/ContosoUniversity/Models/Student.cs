using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required]
        //[StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        //[StringLength(100, MinimumLength = 2)]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        //The ApplyFormatInEditMode setting specifies that the specified formatting should also be applied when the value is displayed in a text box for editing.
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }
        /*Navigation properties are typically defined as virtual so that they can take advantage of certain 
         * Entity Framework functionality such as lazy loading. (Lazy loading will be explained later, 
         * in the Reading Related Data tutorial later in this series.)
        */

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}