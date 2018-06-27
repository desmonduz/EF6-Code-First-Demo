//this file is created automatically by running following command in PM:
//add-migration initial

namespace EF6CodeFirstDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weight = c.Single(nullable: false),
                        RowVersion = c.Binary(),
                        GradeId = c.Int(),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.StudentAddresses",
                c => new
                    {
                        StudentAddressId = c.Int(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Zipcode = c.Int(nullable: false),
                        State = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.StudentAddressId)
                .ForeignKey("dbo.Students", t => t.StudentAddressId)
                .Index(t => t.StudentAddressId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        GradeName = c.String(),
                        Section = c.String(),
                    })
                .PrimaryKey(t => t.GradeId);
            
            CreateTable(
                "dbo.TeacherInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(),
                        ModeOfTeaching = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Student_StudentID = c.Int(nullable: false),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_StudentID, t.Course_CourseId })
                .ForeignKey("dbo.Students", t => t.Student_StudentID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: true)
                .Index(t => t.Student_StudentID)
                .Index(t => t.Course_CourseId);
            
            CreateStoredProcedure(
                "dbo.Teacher_Insert",
                p => new
                    {
                        TeacherName = p.String(),
                        ModeOfTeaching = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[TeacherInfo]([TeacherName], [ModeOfTeaching])
                      VALUES (@TeacherName, @ModeOfTeaching)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[TeacherInfo]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[TeacherInfo] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Teacher_Update",
                p => new
                    {
                        Id = p.Int(),
                        TeacherName = p.String(),
                        ModeOfTeaching = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[TeacherInfo]
                      SET [TeacherName] = @TeacherName, [ModeOfTeaching] = @ModeOfTeaching
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Teacher_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[TeacherInfo]
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Teacher_Delete");
            DropStoredProcedure("dbo.Teacher_Update");
            DropStoredProcedure("dbo.Teacher_Insert");
            DropForeignKey("dbo.Students", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.StudentCourses", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "Student_StudentID", "dbo.Students");
            DropForeignKey("dbo.StudentAddresses", "StudentAddressId", "dbo.Students");
            DropIndex("dbo.StudentCourses", new[] { "Course_CourseId" });
            DropIndex("dbo.StudentCourses", new[] { "Student_StudentID" });
            DropIndex("dbo.StudentAddresses", new[] { "StudentAddressId" });
            DropIndex("dbo.Students", new[] { "GradeId" });
            DropTable("dbo.StudentCourses");
            DropTable("dbo.TeacherInfo");
            DropTable("dbo.Grades");
            DropTable("dbo.StudentAddresses");
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
        }
    }
}
