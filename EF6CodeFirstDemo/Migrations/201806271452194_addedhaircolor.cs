namespace EF6CodeFirstDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedhaircolor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "HairColor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "HairColor");
        }
    }
}
