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
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name cannot be less 2 and longer than 50 characters.")]
    public string LastName { get; set; }

    [Required]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
    public string FirstMidName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    //The ApplyFormatInEditMode setting specifies that the specified formatting should 
    //also be applied when the value is displayed in a text box for editing.
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