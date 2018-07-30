namespace ContosoUniversity.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class InitialCreate : DbMigration
  {
    public override void Up()
    
    {
     //Clean all objects
       Down();

      CreateTable(
       "dbo.Course",
       c => new
       {
         CourseID = c.Int(nullable: false),
         Title = c.String(maxLength:255),
         Credits = c.Int(nullable: false),
       })
       .PrimaryKey(t => t.CourseID);

      AlterColumn("dbo.Course", "Title", c => c.String(maxLength: 200));

      CreateTable(
          "dbo.Enrollment",
          c => new
          {
            EnrollmentID = c.Int(nullable: false, identity: true),
            CourseID = c.Int(nullable: false),
            StudentID = c.Int(nullable: false),
            Grade = c.Int(),
          })
          .PrimaryKey(t => t.EnrollmentID)
          .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
          .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
          .Index(t => t.CourseID)
          .Index(t => t.StudentID);

      CreateTable(
          "dbo.Student",
          c => new
          {
            ID = c.Int(nullable: false, identity: true),
            LastName = c.String(maxLength:50),
            FirstMidName = c.String(maxLength: 50),
            EnrollmentDate = c.DateTime(nullable: false),
            EmailAddress = c.String(maxLength: 50),
          })
          .PrimaryKey(t => t.ID);

      RenameColumn(table: "dbo.Student", name: "FirstMidName", newName: "FirstName");

      AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false, maxLength: 45));
      AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false, maxLength: 45));
      AlterColumn("dbo.Student", "EmailAddress", c => c.String(nullable: false, maxLength: 40));

    }

    public override void Down()
    {
      DropForeignKey("dbo.Enrollment", "StudentID", "dbo.Student");
      DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
      DropIndex("dbo.Enrollment", new[] { "StudentID" });
      DropIndex("dbo.Enrollment", new[] { "CourseID" });
      DropTable("dbo.Student");
      DropTable("dbo.Enrollment");
      DropTable("dbo.Course");
    }
  }
}
