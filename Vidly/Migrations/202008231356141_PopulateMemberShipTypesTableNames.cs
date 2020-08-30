namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMemberShipTypesTableNames : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MemberShipTypes SET Name='Pay as You Go' where Id=1");
            Sql("UPDATE MemberShipTypes SET Name='Monthly' where Id=2");
            Sql("UPDATE MemberShipTypes SET Name='Qauterly' where Id=3");
            Sql("UPDATE MemberShipTypes SET Name='Yearly' where Id=4");
        }
        
        public override void Down()
        {
        }
    }
}
