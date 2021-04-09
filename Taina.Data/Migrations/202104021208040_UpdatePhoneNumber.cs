namespace Taina.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePhoneNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "PhoneNumber", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "PhoneNumber", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
