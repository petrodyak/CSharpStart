using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
  public class Student
  {
    public int ID { get; set; }

    [Required]
    [StringLength(45, MinimumLength = 2, ErrorMessage = "Last Name cannot be less 2 and longer than 45 characters.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
    [Column("FirstName")] //Set explicit column name to FirstName in DataBase
    [Display(Name = "First Name")]
    [StringLength(45, ErrorMessage = "First name cannot be longer than 45 characters.")]
    public string FirstMidName { get; set; }

    [Required] //The Required attribute makes the name properties required fields
    [DataType(DataType.Date)]
    //The ApplyFormatInEditMode setting specifies that the specified formatting should 
    //also be applied when the value is displayed in a text box for editing.
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    //he Display attribute specifies that the caption for the text boxes should be
    [Display(Name = "Enrollment Date")]
    public DateTime EnrollmentDate { get; set; }
    /*Navigation properties are typically defined as virtual so that they can take advantage of certain 
     * Entity Framework functionality such as lazy loading. (Lazy loading will be explained later, 
     * in the Reading Related Data tutorial later in this series.)
    */

    [DataType(DataType.EmailAddress)]
    [StringLength(45)]
    public string EmailAddress { get; set; }


    [Display(Name = "Full Name")]
    public string FullName
    {
      get
      {
        return LastName + " " + FirstMidName;
      }
    }


    public virtual ICollection<Enrollment> Enrollments { get; set; }
  }
}